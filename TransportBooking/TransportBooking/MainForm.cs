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

    /// <summary>
    /// Zakładka "Test połączenia z bazą danych"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    //Ładowanie klientów
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


    /// <summary>
    /// Zakładka "Klienci"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //Przycisk łądujący klientów z bazdy danych
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

    //Przycisk dodający nowego klienta
    private void btnAddClient_Click(object sender, EventArgs e)
    {
        try
        {
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();

            string? email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
            string? phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();

            string? city = string.IsNullOrWhiteSpace(txtCity.Text) ? null : txtCity.Text.Trim();
            string? address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim();
            string? postalCode = string.IsNullOrWhiteSpace(txtPostalCode.Text) ? null : txtPostalCode.Text.Trim();

            if (firstName.Length < 3)
            {
                MessageBox.Show("Imię musi mieć co najmniej 3 znaki.");
                return;
            }

            if (lastName.Length < 3)
            {
                MessageBox.Show("Nazwisko musi mieć co najmniej 3 znaki.");
                return;
            }

            if (email is not null)
            {
                int at = email.IndexOf('@');
                int dot = email.LastIndexOf('.');
                if (at <= 0 || dot <= at + 1 || dot == email.Length - 1)
                {
                    MessageBox.Show("Podaj poprawny adres e-mail.");
                    return;
                }
            }

            if (phone is not null)
            {
                string cleaned = phone.Replace(" ", "").Replace("-", "");
                if (cleaned.StartsWith("+")) cleaned = cleaned[1..];

                if (cleaned.Length != 9 || !cleaned.All(char.IsDigit))
                {
                    MessageBox.Show("Podaj poprawny numer telefonu (9 cyfr).");
                    return;
                }
            }

            if (postalCode is not null)
            {
                string pc = postalCode.Replace(" ", "");
                bool ok =
                    (pc.Length == 6 && pc[2] == '-' && pc.Take(2).All(char.IsDigit) && pc.Skip(3).All(char.IsDigit)) ||
                    (pc.Length == 5 && pc.All(char.IsDigit));

                if (!ok)
                {
                    MessageBox.Show("Kod pocztowy jest niepoprawny. Użyj formatu 00-000 lub 00000.");
                    return;
                }
            }

            if (email is not null && email.Length > 120) { MessageBox.Show("Email max 120 znaków."); return; }
            if (phone is not null && phone.Length > 20) { MessageBox.Show("Telefon max 20 znaków."); return; }
            if (address is not null && address.Length > 120) { MessageBox.Show("Adres max 120 znaków."); return; }
            if (city is not null && city.Length > 60) { MessageBox.Show("Miasto max 60 znaków."); return; }
            if (postalCode is not null && postalCode.Length > 10) { MessageBox.Show("Kod pocztowy max 10 znaków."); return; }

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

            if (phone is not null)
            {
                bool exists = db.Clients.Any(c => c.Phone == phone);
                if (exists)
                {
                    MessageBox.Show("Klient z takim numerem telefonu już istnieje.");
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

            // czyść pola
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtCity.Clear();
            txtAddress.Clear();
            txtPostalCode.Clear();

            // odśwież listę
            btnLoadClients_Click(sender, e);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    //Przycisk wyszukiwujący klienta w bazie danych
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

    //Przycisk usuwający klienta zaznaczonego
    private void btnDeleteClient_Click(object sender, EventArgs e)
    {
        try
        {
            if (dgvClients.CurrentRow == null)
            {
                MessageBox.Show("Zaznacz klienta w tabeli.");
                return;
            }

            // Pobieramy ID z zaznaczonego wiersza (kolumna ClientId)
            var idObj = dgvClients.CurrentRow.Cells["ClientId"].Value;
            if (idObj == null || !long.TryParse(idObj.ToString(), out long clientId))
            {
                MessageBox.Show("Nie udało się odczytać ID klienta.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Czy na pewno usunąć klienta ID: {clientId}?",
                "Potwierdzenie usunięcia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            using var db = new AppDbContext();

            // Uwaga: jeśli klient ma rezerwacje -> FK może zablokować usunięcie (i dobrze)
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
            LoadClients(txtSearchClient.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

}
