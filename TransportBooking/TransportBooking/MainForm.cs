namespace TransportBooking;

using TransportBooking.Data.Context;
using Microsoft.EntityFrameworkCore;
using TransportBooking.Domain.Entities;




public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    //Testowanie połącznia z bazą danych
    private void btnTestDb_Click(object sender, EventArgs e)
    {
        try
        {
            using var db = new AppDbContext();
            var ok = db.Database.CanConnect();

            if (db_connect_info != null)
            {
                db_connect_info.Text = ok
                    ? "Wynik połączenia: Połączono"
                    : "Wynik połączenia: Brak połączenia";
            }
            else
            {
                MessageBox.Show("Label db_connect_info jest null");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }



    /* =========================================================
   SEKCJA: KLIENCI
   ---------------------------------------------------------
   Sekcja odpowiada za kompleksowe zarządzanie danymi klientów
   w systemie rezerwacji transportu. Umożliwia:
   - wyświetlanie listy klientów z bazy danych,
   - wyszukiwanie klientów po podstawowych danych,
   - dodawanie nowych klientów z pełną walidacją danych,
   - edycję danych istniejących klientów,
   - bezpieczne usuwanie klientów (z blokadą, jeśli istnieją
     powiązane rezerwacje).
   
   Dane prezentowane są w kontrolce DataGridView, a operacje
   CRUD realizowane są z wykorzystaniem Entity Framework Core
   oraz bazy danych PostgreSQL.
   ========================================================= */

    // Ładuje listę klientów z bazy danych (opcjonalnie z filtrem wyszukiwania)
    private void LoadClients(string? filter = null)
    {
        using var db = new AppDbContext();

        var q = db.Clients.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            filter = filter.Trim().ToLower();

            q = q.Where(c =>
                (c.FirstName != null && c.FirstName.ToLower().Contains(filter)) ||
                (c.LastName != null && c.LastName.ToLower().Contains(filter)) ||
                (c.Email != null && c.Email.ToLower().Contains(filter)) ||
                (c.Phone != null && c.Phone.ToLower().Contains(filter)) ||
                (c.City != null && c.City.ToLower().Contains(filter))
            );
        }

        dgvClients.DataSource = q
            .OrderByDescending(c => c.ClientId)
            .ToList();
    }

    // Przechowuje ID aktualnie zaznaczonego klienta w tabeli
    private long? _selectedClientId = null;

    // Obsługuje przycisk wczytujący wszystkich klientów z bazy
    private void btnLoadClients_Click(object sender, EventArgs e)
    {
        try
        {
            LoadClients();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    // Dodaje nowego klienta do bazy danych po poprawnej walidacji danych
    private void btnAddClient_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateClientInputs(out var firstName, out var lastName,
                    out var email, out var phone, out var city, out var address, out var postalCode))
                return;

            using var db = new AppDbContext();

            if (email is not null)
            {
                bool exists = db.Clients.Any(c => c.Email == email);
                if (exists)
                {
                    MessageBox.Show("Klient z takim adresem e-mail już istnieje.");
                    return;
                }
            }

            var client = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                City = city,
                Address = address,
                PostalCode = postalCode,
                CreatedAt = DateTime.UtcNow
            };

            db.Clients.Add(client);
            db.SaveChanges();

            MessageBox.Show($"Dodano klienta ✅ ID: {client.ClientId}");
            btnClearClientForm_Click(sender, e);
            LoadClients(txtSearchClient.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    // Wyszukuje klientów na podstawie wpisanego tekstu (imię, nazwisko, email itp.)
    private void btnSearchClient_Click(object sender, EventArgs e)
    {
        try
        {
            LoadClients(txtSearchClient.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    // Usuwa zaznaczonego klienta z bazy danych (jeśli nie ma rezerwacji)
    private void btnDeleteClient_Click(object sender, EventArgs e)
    {
        try
        {
            if (dgvClients.CurrentRow == null)
            {
                MessageBox.Show("Zaznacz klienta w tabeli.");
                return;
            }

            var idObj = dgvClients.CurrentRow.Cells["ClientId"].Value;
            if (idObj == null || !long.TryParse(idObj.ToString(), out long clientId))
            {
                MessageBox.Show("Nie udało się odczytać ID klienta.");
                return;
            }

            using var db = new AppDbContext();

            // BLOKADA: klient ma rezerwacje
            bool hasReservations = db.Reservations.Any(r => r.ClientId == clientId);
            if (hasReservations)
            {
                MessageBox.Show("Nie można usunąć klienta — ma przypisane rezerwacje.\nUsuń/zmień rezerwacje i spróbuj ponownie.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Czy na pewno usunąć klienta ID: {clientId}?",
                "Potwierdzenie usunięcia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            var client = db.Clients.FirstOrDefault(c => c.ClientId == clientId);
            if (client == null)
            {
                MessageBox.Show("Klient nie istnieje (może już został usunięty).");
                LoadClients(txtSearchClient.Text);
                return;
            }

            db.Clients.Remove(client);
            db.SaveChanges();

            MessageBox.Show("Usunięto klienta ✅");
            btnClearClientForm_Click(sender, e);
            LoadClients(txtSearchClient.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    // Aktualizuje dane zaznaczonego klienta w bazie danych
    private void btnUpdateClient_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedClientId is null)
            {
                MessageBox.Show("Najpierw zaznacz klienta w tabeli.");
                return;
            }

            if (!ValidateClientInputs(out var firstName, out var lastName,
                    out var email, out var phone, out var city, out var address, out var postalCode))
                return;

            using var db = new AppDbContext();

            // duplikat email, ale z wykluczeniem aktualnie edytowanego klienta
            if (email is not null)
            {
                bool exists = db.Clients.Any(c => c.Email == email && c.ClientId != _selectedClientId.Value);
                if (exists)
                {
                    MessageBox.Show("Inny klient ma już taki adres e-mail.");
                    return;
                }
            }

            var client = db.Clients.FirstOrDefault(c => c.ClientId == _selectedClientId.Value);
            if (client == null)
            {
                MessageBox.Show("Klient nie istnieje (mógł zostać usunięty).");
                LoadClients(txtSearchClient.Text);
                return;
            }

            client.FirstName = firstName;
            client.LastName = lastName;
            client.Email = email;
            client.Phone = phone;
            client.City = city;
            client.Address = address;
            client.PostalCode = postalCode;

            db.SaveChanges();

            MessageBox.Show("Zapisano zmiany ✅");
            LoadClients(txtSearchClient.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    // Czyści formularz klienta oraz resetuje zaznaczenie w tabeli
    private void btnClearClientForm_Click(object sender, EventArgs e)
    {
        _selectedClientId = null;

        txtFirstName.Clear();
        txtLastName.Clear();
        txtEmail.Clear();
        txtPhone.Clear();
        txtAddress.Clear();
        txtCity.Clear();
        txtPostalCode.Clear();

        dgvClients.ClearSelection();
    }

    // Obsługuje kliknięcie w tabeli klientów i uzupełnia formularz danymi klienta
    private void dgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (dgvClients.CurrentRow == null) return;

            _selectedClientId = Convert.ToInt64(dgvClients.CurrentRow.Cells["ClientId"].Value);

            txtFirstName.Text = dgvClients.CurrentRow.Cells["FirstName"].Value?.ToString() ?? "";
            txtLastName.Text = dgvClients.CurrentRow.Cells["LastName"].Value?.ToString() ?? "";
            txtEmail.Text = dgvClients.CurrentRow.Cells["Email"].Value?.ToString() ?? "";
            txtPhone.Text = dgvClients.CurrentRow.Cells["Phone"].Value?.ToString() ?? "";
            txtAddress.Text = dgvClients.CurrentRow.Cells["Address"].Value?.ToString() ?? "";
            txtCity.Text = dgvClients.CurrentRow.Cells["City"].Value?.ToString() ?? "";
            txtPostalCode.Text = dgvClients.CurrentRow.Cells["PostalCode"].Value?.ToString() ?? "";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    // Waliduje dane klienta (wspólna metoda dla dodawania i edycji)
    private bool ValidateClientInputs(out string firstName, out string lastName,
    out string? email, out string? phone, out string? city, out string? address, out string? postalCode)
    {
        firstName = txtFirstName.Text.Trim();
        lastName = txtLastName.Text.Trim();
        email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
        phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();
        city = string.IsNullOrWhiteSpace(txtCity.Text) ? null : txtCity.Text.Trim();
        address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim();
        postalCode = string.IsNullOrWhiteSpace(txtPostalCode.Text) ? null : txtPostalCode.Text.Trim();

        if (firstName.Length < 2) { MessageBox.Show("Imię musi mieć co najmniej 2 znaki."); return false; }
        if (lastName.Length < 2) { MessageBox.Show("Nazwisko musi mieć co najmniej 2 znaki."); return false; }

        // email jeśli podany
        if (email is not null)
        {
            int at = email.IndexOf('@');
            int dot = email.LastIndexOf('.');
            if (at <= 0 || dot <= at + 1 || dot == email.Length - 1)
            {
                MessageBox.Show("Podaj poprawny adres e-mail.");
                return false;
            }
            if (email.Length > 120) { MessageBox.Show("Email max 120 znaków."); return false; }
        }

        // telefon jeśli podany
        if (phone is not null)
        {
            string cleaned = phone.Replace(" ", "").Replace("-", "");
            if (cleaned.StartsWith("+")) cleaned = cleaned[1..];

            if (cleaned.Length < 7 || cleaned.Length > 15 || !cleaned.All(char.IsDigit))
            {
                MessageBox.Show("Podaj poprawny numer telefonu (7–15 cyfr).");
                return false;
            }
            if (phone.Length > 20) { MessageBox.Show("Telefon max 20 znaków."); return false; }
        }

        // kod pocztowy jeśli podany
        if (postalCode is not null)
        {
            string pc = postalCode.Replace(" ", "");
            bool ok =
                (pc.Length == 6 && pc[2] == '-' && pc.Take(2).All(char.IsDigit) && pc.Skip(3).All(char.IsDigit)) ||
                (pc.Length == 5 && pc.All(char.IsDigit));

            if (!ok)
            {
                MessageBox.Show("Kod pocztowy jest niepoprawny. Użyj formatu 00-000 lub 00000.");
                return false;
            }
            if (postalCode.Length > 10) { MessageBox.Show("Kod pocztowy max 10 znaków."); return false; }
        }

        if (address is not null && address.Length > 120) { MessageBox.Show("Adres max 120 znaków."); return false; }
        if (city is not null && city.Length > 60) { MessageBox.Show("Miasto max 60 znaków."); return false; }

        return true;
    }


}
