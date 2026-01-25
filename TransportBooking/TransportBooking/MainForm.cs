namespace TransportBooking;

using TransportBooking.Data.Context;
using Microsoft.EntityFrameworkCore;
using TransportBooking.Domain.Entities;

using System.Text;


public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        cmbServiceType.Items.Clear();
        cmbServiceType.Items.AddRange(new object[] { "PERSON", "PACKAGE" });
        cmbServiceType.SelectedIndex = 0;



        cmbResStatus.Items.Clear();
        cmbResStatus.Items.AddRange(new object[] { "CONFIRMED", "NEW", "CANCELLED", "DONE" });
        cmbResStatus.SelectedIndex = 0;

        dtpResCreatedAt.Value = DateTime.Now;

        lblDbStatus.Text = "Status: —";
        if (txtDbDetails != null) txtDbDetails.Text = "Kliknij „Test połączenia z bazą”.";

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






    /* =========================================================
   SEKCJA: POJAZDY
   ---------------------------------------------------------
   Sekcja odpowiedzialna za zarządzanie pojazdami wykorzystywanymi
   w systemie rezerwacji transportu. Umożliwia:
   - wyświetlanie listy pojazdów z bazy danych,
   - wyszukiwanie pojazdów po numerze rejestracyjnym lub modelu,
   - dodawanie nowych pojazdów z walidacją danych,
   - edycję danych istniejących pojazdów,
   - bezpieczne usuwanie pojazdów (z blokadą, jeśli pojazd
     jest przypisany do tras).
   
   Dane prezentowane są w kontrolce DataGridView, a operacje
   CRUD realizowane są z wykorzystaniem Entity Framework Core
   oraz bazy danych PostgreSQL.
   ========================================================= */
    // Przechowuje ID aktualnie zaznaczonego pojazdu w tabeli
    private long? _selectedVehicleId = null;



    // Ładuje listę pojazdów z bazy danych (opcjonalnie z filtrem wyszukiwania)
    private void LoadVehicles(string? filter = null)
    {
        using var db = new AppDbContext();

        var q = db.Vehicles.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            filter = filter.Trim().ToLower();

            q = q.Where(v =>
                v.PlateNumber.ToLower().Contains(filter) ||
                v.Model.ToLower().Contains(filter)
            );
        }

        dgvVehicles.DataSource = q
            .OrderByDescending(v => v.VehicleId)
            .ToList();
    }



    // Obsługuje przycisk wczytujący wszystkie pojazdy z bazy danych
    private void btnLoadVehicles_Click_1(object sender, EventArgs e)
    {
        MessageBox.Show("Klik działa");


        try
        {
            LoadVehicles();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Wyszukuje pojazdy na podstawie numeru rejestracyjnego lub modelu
    private void btnSearchVehicle_Click(object sender, EventArgs e)
    {
        try
        {
            LoadVehicles(txtSearchVehicle.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Obsługuje kliknięcie w tabeli pojazdów i uzupełnia formularz danymi pojazdu
    private void dgvVehicles_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (dgvVehicles.CurrentRow == null) return;

        _selectedVehicleId = Convert.ToInt64(dgvVehicles.CurrentRow.Cells["VehicleId"].Value);

        txtPlateNumber.Text = dgvVehicles.CurrentRow.Cells["PlateNumber"].Value?.ToString() ?? "";
        txtVehicleModel.Text = dgvVehicles.CurrentRow.Cells["Model"].Value?.ToString() ?? "";
        txtSeats.Text = dgvVehicles.CurrentRow.Cells["Seats"].Value?.ToString() ?? "";
        chkVehicleActive.Checked = Convert.ToBoolean(dgvVehicles.CurrentRow.Cells["Active"].Value);
    }



    // Waliduje dane pojazdu (numer rejestracyjny, model, liczba miejsc, status)
    private bool ValidateVehicleInputs(out string plate, out string model, out int seats, out bool active)
    {
        plate = txtPlateNumber.Text.Trim();
        model = txtVehicleModel.Text.Trim();
        active = chkVehicleActive.Checked;

        if (plate.Length < 3)
        {
            MessageBox.Show("Numer rejestracyjny jest za krótki.");
            seats = 0;
            return false;
        }

        if (model.Length < 2)
        {
            MessageBox.Show("Model pojazdu jest za krótki.");
            seats = 0;
            return false;
        }

        if (!int.TryParse(txtSeats.Text.Trim(), out seats) || seats <= 0)
        {
            MessageBox.Show("Liczba miejsc musi być liczbą większą od 0.");
            return false;
        }

        return true;
    }



    // Dodaje nowy pojazd do bazy danych po poprawnej walidacji danych
    private void btnAddVehicle_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateVehicleInputs(out var plate, out var model, out var seats, out var active))
                return;

            using var db = new AppDbContext();

            bool exists = db.Vehicles.Any(v => v.PlateNumber == plate);
            if (exists)
            {
                MessageBox.Show("Pojazd z takim numerem rejestracyjnym już istnieje.");
                return;
            }

            var vehicle = new Vehicle
            {
                PlateNumber = plate,
                Model = model,
                Seats = seats,
                Active = active
            };

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            MessageBox.Show("Dodano pojazd ✅");
            btnClearVehicleForm_Click(sender, e);
            LoadVehicles(txtSearchVehicle.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Aktualizuje dane zaznaczonego pojazdu w bazie danych
    private void btnUpdateVehicle_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedVehicleId is null)
            {
                MessageBox.Show("Zaznacz pojazd w tabeli.");
                return;
            }

            if (!ValidateVehicleInputs(out var plate, out var model, out var seats, out var active))
                return;

            using var db = new AppDbContext();

            bool exists = db.Vehicles.Any(v =>
                v.PlateNumber == plate && v.VehicleId != _selectedVehicleId.Value);

            if (exists)
            {
                MessageBox.Show("Inny pojazd ma już taki numer rejestracyjny.");
                return;
            }

            var vehicle = db.Vehicles.FirstOrDefault(v => v.VehicleId == _selectedVehicleId.Value);
            if (vehicle == null)
            {
                MessageBox.Show("Pojazd nie istnieje.");
                LoadVehicles();
                return;
            }

            vehicle.PlateNumber = plate;
            vehicle.Model = model;
            vehicle.Seats = seats;
            vehicle.Active = active;

            db.SaveChanges();

            MessageBox.Show("Zapisano zmiany ✅");
            LoadVehicles(txtSearchVehicle.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Usuwa zaznaczony pojazd z bazy danych (jeśli nie jest przypisany do tras)
    private void btnDeleteVehicle_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedVehicleId is null)
            {
                MessageBox.Show("Zaznacz pojazd w tabeli.");
                return;
            }

            using var db = new AppDbContext();

            bool inRoutes = db.Routes.Any(r => r.VehicleId == _selectedVehicleId.Value);
            if (inRoutes)
            {
                MessageBox.Show("Nie można usunąć pojazdu — jest przypisany do tras.");
                return;
            }

            var confirm = MessageBox.Show(
                "Czy na pewno usunąć pojazd?",
                "Potwierdzenie",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            var vehicle = db.Vehicles.First(v => v.VehicleId == _selectedVehicleId.Value);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            MessageBox.Show("Usunięto pojazd ✅");
            btnClearVehicleForm_Click(sender, e);
            LoadVehicles();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Czyści formularz pojazdu oraz resetuje zaznaczenie w tabeli
    private void btnClearVehicleForm_Click(object sender, EventArgs e)
    {
        _selectedVehicleId = null;

        txtPlateNumber.Clear();
        txtVehicleModel.Clear();
        txtSeats.Clear();
        chkVehicleActive.Checked = false;

        dgvVehicles.ClearSelection();
    }



    /* =========================================================
   SEKCJA: TRASY / KURSY
   ---------------------------------------------------------
   Sekcja odpowiedzialna za zarządzanie trasami (kursami)
   realizowanymi przez firmę transportową. Umożliwia:
   - wyświetlanie listy tras zapisanych w bazie danych,
   - wyszukiwanie tras po miastach początkowych i docelowych,
   - dodawanie nowych tras z przypisaniem pojazdu,
   - edycję danych istniejących tras,
   - bezpieczne usuwanie tras (z blokadą, jeśli istnieją
     powiązane rezerwacje).
   
   Trasa zawiera informacje o pojeździe, miastach,
   dacie i godzinie wyjazdu oraz cenie za osobę.
   Dane przechowywane są w bazie PostgreSQL, a operacje
   CRUD realizowane są z wykorzystaniem Entity Framework Core.
   ========================================================= */



    // Przechowuje ID aktualnie zaznaczonej trasy w tabeli
    private long? _selectedRouteId = null;


    // Wczytuje listę pojazdów do ComboBox (wybór pojazdu dla trasy)
    private void LoadVehiclesToRouteCombo()
    {
        using var db = new AppDbContext();

        var vehicles = db.Vehicles
            .AsNoTracking()
            .OrderByDescending(v => v.VehicleId)
            .Select(v => new
            {
                v.VehicleId,
                Display = $"{v.PlateNumber} | {v.Model} | miejsca: {v.Seats}"
            })
            .ToList();

        cmbRouteVehicle.DataSource = vehicles;
        cmbRouteVehicle.DisplayMember = "Display";
        cmbRouteVehicle.ValueMember = "VehicleId";
    }



    // Ładuje listę tras z bazy danych (opcjonalnie z filtrem wyszukiwania)
    private void LoadRoutes(string? filter = null)
    {
        using var db = new AppDbContext();

        var q = db.Routes.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            filter = filter.Trim().ToLower();

            q = q.Where(r =>
                r.StartCity.ToLower().Contains(filter) ||
                r.EndCity.ToLower().Contains(filter)
            );
        }

        dgvRoutes.DataSource = q
            .OrderByDescending(r => r.RouteId)
            .ToList();
    }



    // Obsługuje przycisk wczytujący wszystkie trasy oraz pojazdy do ComboBox
    private void btnLoadRoutes_Click(object sender, EventArgs e)
    {
        try
        {
            LoadVehiclesToRouteCombo();
            LoadRoutes();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Wyszukuje trasy na podstawie miasta początkowego lub docelowego
    private void btnSearchRoute_Click(object sender, EventArgs e)
    {
        try
        {
            LoadRoutes(txtSearchRoute.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Obsługuje kliknięcie w tabeli tras i uzupełnia formularz danymi trasy
    private void dgvRoutes_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (dgvRoutes.CurrentRow == null) return;

            _selectedRouteId = Convert.ToInt64(dgvRoutes.CurrentRow.Cells["RouteId"].Value);

            txtStartCity.Text = dgvRoutes.CurrentRow.Cells["StartCity"].Value?.ToString() ?? "";
            txtEndCity.Text = dgvRoutes.CurrentRow.Cells["EndCity"].Value?.ToString() ?? "";
            txtPricePerson.Text = dgvRoutes.CurrentRow.Cells["PricePerson"].Value?.ToString() ?? "";

            if (dgvRoutes.CurrentRow.Cells["DepartureTime"].Value is DateTime dt)
                dtpDepartureTime.Value = dt;

            // ustaw pojazd w ComboBox
            var vehicleIdObj = dgvRoutes.CurrentRow.Cells["VehicleId"].Value;
            if (vehicleIdObj != null)
                cmbRouteVehicle.SelectedValue = Convert.ToInt64(vehicleIdObj);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Waliduje dane trasy (pojazd, miasta, data wyjazdu, cena)
    private bool ValidateRouteInputs(out long vehicleId, out string start, out string end, out DateTime departure, out decimal pricePerson)
    {
        // ustaw wartości domyślne dla OUT (żeby nie było CS0177)
        vehicleId = 0;
        start = txtStartCity.Text.Trim();
        end = txtEndCity.Text.Trim();
        departure = dtpDepartureTime.Value;
        pricePerson = 0;

        // pojazd
        if (cmbRouteVehicle.SelectedValue == null || !long.TryParse(cmbRouteVehicle.SelectedValue.ToString(), out vehicleId))
        {
            MessageBox.Show("Wybierz pojazd dla trasy.");
            return false;
        }

        // miasta
        if (start.Length < 2)
        {
            MessageBox.Show("Miasto startowe jest za krótkie.");
            return false;
        }

        if (end.Length < 2)
        {
            MessageBox.Show("Miasto docelowe jest za krótkie.");
            return false;
        }

        // cena
        var raw = txtPricePerson.Text.Trim().Replace(',', '.');
        if (!decimal.TryParse(raw, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out pricePerson) || pricePerson < 0)
        {
            MessageBox.Show("Cena za osobę musi być liczbą >= 0 (np. 120.50).");
            return false;
        }

        return true;
    }



    // Dodaje nową trasę do bazy danych po poprawnej walidacji danych
    private void btnAddRoute_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateRouteInputs(out var vehicleId, out var start, out var end, out var departure, out var price))
                return;

            using var db = new AppDbContext();

            var route = new Route
            {
                VehicleId = vehicleId,
                StartCity = start,
                EndCity = end,
                DepartureTime = DateTime.SpecifyKind(departure, DateTimeKind.Local).ToUniversalTime(),
                PricePerson = price
            };


            db.Routes.Add(route);
            db.SaveChanges();

            MessageBox.Show("Dodano trasę ✅");
            btnClearRouteForm_Click(sender, e);
            LoadRoutes(txtSearchRoute.Text);
        }
        catch (Exception ex)
        {
            var details = ex.InnerException?.Message ?? ex.Message;
            MessageBox.Show("Błąd: " + details);
        }

    }



    // Aktualizuje dane zaznaczonej trasy w bazie danych
    private void btnUpdateRoute_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedRouteId is null)
            {
                MessageBox.Show("Zaznacz trasę w tabeli.");
                return;
            }

            if (!ValidateRouteInputs(out var vehicleId, out var start, out var end, out var departure, out var price))
                return;

            using var db = new AppDbContext();

            var route = db.Routes.FirstOrDefault(r => r.RouteId == _selectedRouteId.Value);
            if (route == null)
            {
                MessageBox.Show("Trasa nie istnieje.");
                LoadRoutes();
                return;
            }

            route.VehicleId = vehicleId;
            route.StartCity = start;
            route.EndCity = end;
            route.DepartureTime = DateTime.SpecifyKind(departure, DateTimeKind.Local).ToUniversalTime();
            route.PricePerson = price;

            db.SaveChanges();

            MessageBox.Show("Zapisano zmiany ✅");
            LoadRoutes(txtSearchRoute.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Usuwa zaznaczoną trasę z bazy danych (jeśli nie istnieją rezerwacje)
    private void btnDeleteRoute_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedRouteId is null)
            {
                MessageBox.Show("Zaznacz trasę w tabeli.");
                return;
            }

            using var db = new AppDbContext();

            bool hasReservations = db.Reservations.Any(r => r.RouteId == _selectedRouteId.Value);
            if (hasReservations)
            {
                MessageBox.Show("Nie można usunąć trasy — istnieją rezerwacje na tę trasę.");
                return;
            }

            var confirm = MessageBox.Show(
                "Czy na pewno usunąć trasę?",
                "Potwierdzenie",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            var route = db.Routes.First(r => r.RouteId == _selectedRouteId.Value);
            db.Routes.Remove(route);
            db.SaveChanges();

            MessageBox.Show("Usunięto trasę ✅");
            btnClearRouteForm_Click(sender, e);
            LoadRoutes();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Czyści formularz trasy oraz resetuje zaznaczenie w tabeli
    private void btnClearRouteForm_Click(object sender, EventArgs e)
    {
        _selectedRouteId = null;

        txtStartCity.Clear();
        txtEndCity.Clear();
        txtPricePerson.Clear();
        dtpDepartureTime.Value = DateTime.Now;

        dgvRoutes.ClearSelection();
    }



    /* =========================================================
   SEKCJA: REZERWACJE
   ---------------------------------------------------------
   Sekcja odpowiedzialna za obsługę rezerwacji przewozu osób
   oraz paczek w systemie transportowym. Umożliwia:
   - wyświetlanie listy rezerwacji zapisanych w bazie danych,
   - wyszukiwanie rezerwacji po typie usługi i statusie,
   - dodawanie nowych rezerwacji z przypisaniem klienta
     oraz trasy,
   - edycję danych istniejących rezerwacji,
   - bezpieczne usuwanie rezerwacji (z blokadą, jeśli
     istnieją powiązane płatności lub paczki).
   
   Rezerwacja zawiera informacje o kliencie, trasie,
   typie usługi, statusie oraz dacie utworzenia.
   Dane przechowywane są w bazie PostgreSQL, a operacje
   CRUD realizowane są z wykorzystaniem Entity Framework Core.
   ========================================================= */



    // Przechowuje ID aktualnie zaznaczonej rezerwacji w tabeli
    private long? _selectedReservationId = null;



    // Wczytuje listę klientów do ComboBox (wybór klienta dla rezerwacji)
    private void LoadClientsToReservationCombo()
    {
        using var db = new AppDbContext();

        var clients = db.Clients
            .AsNoTracking()
            .OrderByDescending(c => c.ClientId)
            .Select(c => new
            {
                c.ClientId,
                Display = $"{c.FirstName} {c.LastName} | {c.Email}"
            })
            .ToList();

        cmbResClient.DataSource = clients;
        cmbResClient.DisplayMember = "Display";
        cmbResClient.ValueMember = "ClientId";
    }



    // Wczytuje listę tras do ComboBox (wybór trasy dla rezerwacji)
    private void LoadRoutesToReservationCombo()
    {
        using var db = new AppDbContext();

        var routes = db.Routes
            .AsNoTracking()
            .OrderByDescending(r => r.RouteId)
            .Select(r => new
            {
                r.RouteId,
                Display = $"{r.StartCity} -> {r.EndCity} | {r.DepartureTime} | {r.PricePerson} zł"
            })
            .ToList();

        cmbResRoute.DataSource = routes;
        cmbResRoute.DisplayMember = "Display";
        cmbResRoute.ValueMember = "RouteId";
    }



    // Ładuje listę rezerwacji z bazy danych (opcjonalnie z filtrem wyszukiwania)
    private void LoadReservations(string? filter = null)
    {
        using var db = new AppDbContext();

        var q = db.Reservations.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            filter = filter.Trim().ToLower();

            q = q.Where(r =>
                r.ServiceType.ToLower().Contains(filter) ||
                r.Status.ToLower().Contains(filter)
            );
        }

        dgvReservations.DataSource = q
            .OrderByDescending(r => r.ReservationId)
            .ToList();
    }



    // Obsługuje przycisk wczytujący wszystkie rezerwacje oraz dane do ComboBoxów
    private void btnLoadReservations_Click(object sender, EventArgs e)
    {
        try
        {
            LoadClientsToReservationCombo();
            LoadRoutesToReservationCombo();
            LoadReservations();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Wyszukuje rezerwacje na podstawie typu usługi lub statusu
    private void btnSearchReservation_Click(object sender, EventArgs e)
    {
        try
        {
            LoadReservations(txtSearchReservation.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Obsługuje kliknięcie w tabeli rezerwacji i uzupełnia formularz danymi rezerwacji
    private void dgvReservations_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (dgvReservations.CurrentRow == null) return;

            _selectedReservationId = Convert.ToInt64(dgvReservations.CurrentRow.Cells["ReservationId"].Value);

            // ustaw klienta i trasę
            var clientIdObj = dgvReservations.CurrentRow.Cells["ClientId"].Value;
            var routeIdObj = dgvReservations.CurrentRow.Cells["RouteId"].Value;

            if (clientIdObj != null) cmbResClient.SelectedValue = Convert.ToInt64(clientIdObj);
            if (routeIdObj != null) cmbResRoute.SelectedValue = Convert.ToInt64(routeIdObj);

            // service type / status
            cmbServiceType.SelectedItem = dgvReservations.CurrentRow.Cells["ServiceType"].Value?.ToString() ?? "osoby";
            cmbResStatus.SelectedItem = dgvReservations.CurrentRow.Cells["Status"].Value?.ToString() ?? "new";

            // created_at (UTC->Local jeśli zapisujesz UTC)
            if (dgvReservations.CurrentRow.Cells["CreatedAt"].Value is DateTime dt)
                dtpResCreatedAt.Value = dt.Kind == DateTimeKind.Utc ? dt.ToLocalTime() : dt;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



    // Waliduje dane rezerwacji (klient, trasa, typ usługi, status, data)
    private bool ValidateReservationInputs(out long clientId, out long routeId, out string serviceType, out string status, out DateTime createdAtUtc)
    {
        clientId = 0;
        routeId = 0;
        serviceType = cmbServiceType.SelectedItem?.ToString() ?? "osoby";
        status = cmbResStatus.SelectedItem?.ToString() ?? "new";

        if (cmbResClient.SelectedValue == null || !long.TryParse(cmbResClient.SelectedValue.ToString(), out clientId))
        {
            MessageBox.Show("Wybierz klienta.");
            createdAtUtc = DateTime.UtcNow;
            return false;
        }

        if (cmbResRoute.SelectedValue == null || !long.TryParse(cmbResRoute.SelectedValue.ToString(), out routeId))
        {
            MessageBox.Show("Wybierz trasę.");
            createdAtUtc = DateTime.UtcNow;
            return false;
        }

        if (serviceType != "PERSON" && serviceType != "PACKAGE")
        {
            MessageBox.Show("Niepoprawny typ usługi.");
            createdAtUtc = DateTime.UtcNow;
            return false;
        }



        if (status.Length < 2)
        {
            MessageBox.Show("Status jest niepoprawny.");
            createdAtUtc = DateTime.UtcNow;
            return false;
        }

        // zapisuj w UTC (bezpiecznie dla Postgres timestamptz)
        createdAtUtc = DateTime.SpecifyKind(dtpResCreatedAt.Value, DateTimeKind.Local).ToUniversalTime();
        return true;
    }



    // Dodaje nową rezerwację do bazy danych po poprawnej walidacji danych
    private void btnAddReservation_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateReservationInputs(out var clientId, out var routeId, out var serviceType, out var status, out var createdAtUtc))
                return;

            using var db = new AppDbContext();

            var res = new Reservation
            {
                ClientId = clientId,
                RouteId = routeId,
                ServiceType = serviceType,
                Status = status,
                CreatedAt = createdAtUtc
            };

            db.Reservations.Add(res);
            db.SaveChanges();

            MessageBox.Show("Dodano rezerwację ✅");
            btnClearReservationForm_Click(sender, e);
            LoadReservations(txtSearchReservation.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + (ex.InnerException?.Message ?? ex.Message));
        }
    }



    // Aktualizuje dane zaznaczonej rezerwacji w bazie danych
    private void btnUpdateReservation_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedReservationId is null)
            {
                MessageBox.Show("Zaznacz rezerwację w tabeli.");
                return;
            }

            if (!ValidateReservationInputs(out var clientId, out var routeId, out var serviceType, out var status, out var createdAtUtc))
                return;

            using var db = new AppDbContext();

            var res = db.Reservations.FirstOrDefault(r => r.ReservationId == _selectedReservationId.Value);
            if (res == null)
            {
                MessageBox.Show("Rezerwacja nie istnieje.");
                LoadReservations();
                return;
            }

            res.ClientId = clientId;
            res.RouteId = routeId;
            res.ServiceType = serviceType;
            res.Status = status;
            res.CreatedAt = createdAtUtc;

            db.SaveChanges();

            MessageBox.Show("Zapisano zmiany ✅");
            LoadReservations(txtSearchReservation.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + (ex.InnerException?.Message ?? ex.Message));
        }
    }



    // Usuwa zaznaczoną rezerwację z bazy danych (jeśli brak powiązań)
    private void btnDeleteReservation_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedReservationId is null)
            {
                MessageBox.Show("Zaznacz rezerwację w tabeli.");
                return;
            }

            using var db = new AppDbContext();

            bool hasPayments = db.Payments.Any(p => p.ReservationId == _selectedReservationId.Value);
            bool hasPackages = db.Packages.Any(p => p.ReservationId == _selectedReservationId.Value);

            if (hasPayments || hasPackages)
            {
                MessageBox.Show("Nie można usunąć rezerwacji — istnieją powiązane płatności lub paczki.");
                return;
            }

            var confirm = MessageBox.Show("Czy na pewno usunąć rezerwację?", "Potwierdzenie",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            var res = db.Reservations.First(r => r.ReservationId == _selectedReservationId.Value);
            db.Reservations.Remove(res);
            db.SaveChanges();

            MessageBox.Show("Usunięto rezerwację ✅");
            btnClearReservationForm_Click(sender, e);
            LoadReservations();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + (ex.InnerException?.Message ?? ex.Message));
        }
    }



    // Czyści formularz rezerwacji oraz resetuje zaznaczenie w tabeli
    private void btnClearReservationForm_Click(object sender, EventArgs e)
    {
        _selectedReservationId = null;

        txtSearchReservation.Clear();
        dtpResCreatedAt.Value = DateTime.Now;

        dgvReservations.ClearSelection();
    }




    /* =========================================================
   SEKCJA: RAPORT CSV
   ---------------------------------------------------------
   Sekcja odpowiedzialna za generowanie raportów miesięcznych
   w formacie CSV na podstawie danych zapisanych w bazie
   danych systemu rezerwacji transportu.
   
   Raport obejmuje rezerwacje z wybranego miesiąca i zawiera
   m.in. informacje o:
   - rezerwacji (ID, data utworzenia, typ usługi, status),
   - kliencie (ID, imię i nazwisko, e-mail),
   - trasie (miasta, data wyjazdu, cena za osobę).
   
   Dane eksportowane są do pliku CSV z kodowaniem UTF-8,
   kompatybilnego z arkuszami kalkulacyjnymi (np. Excel).
   ========================================================= */


    // Obsługuje eksport rezerwacji z wybranego miesiąca do pliku CSV
    private void btnExportCsv_Click(object sender, EventArgs e)
    {
        try
        {
            var localMonth = new DateTime(dtpReportMonth.Value.Year, dtpReportMonth.Value.Month, 1);

            var startUtc = DateTime.SpecifyKind(localMonth, DateTimeKind.Local).ToUniversalTime();
            var endUtc = DateTime.SpecifyKind(localMonth.AddMonths(1), DateTimeKind.Local).ToUniversalTime();

            using var db = new AppDbContext();

            // Pobieramy dane JOINem (bez nawigacji EF)
            var data = (
                from r in db.Reservations.AsNoTracking()
                join c in db.Clients.AsNoTracking() on r.ClientId equals c.ClientId
                join ro in db.Routes.AsNoTracking() on r.RouteId equals ro.RouteId
                where r.CreatedAt >= startUtc && r.CreatedAt < endUtc
                orderby r.CreatedAt
                select new
                {
                    r.ReservationId,
                    r.CreatedAt,
                    r.ServiceType,
                    r.Status,

                    r.ClientId,
                    ClientName = c.FirstName + " " + c.LastName,
                    ClientEmail = c.Email,

                    r.RouteId,
                    ro.StartCity,
                    ro.EndCity,
                    ro.DepartureTime,
                    ro.PricePerson
                }
            ).ToList();

            using var sfd = new SaveFileDialog
            {
                FileName = $"raport_rezerwacje_{localMonth:yyyy_MM}.csv",
                Filter = "CSV (*.csv)|*.csv",
                Title = "Zapisz raport CSV"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var sb = new StringBuilder();

            sb.AppendLine("ReservationId;CreatedAt;ServiceType;Status;ClientId;ClientName;ClientEmail;RouteId;StartCity;EndCity;DepartureTime;PricePerson");

            foreach (var x in data)
            {
                var createdLocal = x.CreatedAt.Kind == DateTimeKind.Utc ? x.CreatedAt.ToLocalTime() : x.CreatedAt;

                var depLocal = x.DepartureTime;
                if (depLocal.Kind == DateTimeKind.Utc)
                    depLocal = depLocal.ToLocalTime();

                sb.AppendLine(string.Join(";",
                    Csv(x.ReservationId),
                    Csv(createdLocal.ToString("yyyy-MM-dd HH:mm")),
                    Csv(x.ServiceType),
                    Csv(x.Status),
                    Csv(x.ClientId),
                    Csv(x.ClientName),
                    Csv(x.ClientEmail),
                    Csv(x.RouteId),
                    Csv(x.StartCity),
                    Csv(x.EndCity),
                    Csv(depLocal.ToString("yyyy-MM-dd HH:mm")),
                    Csv(x.PricePerson.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
                ));
            }

            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));

            MessageBox.Show($"Zapisano CSV ✅\nRekordów: {data.Count}");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + (ex.InnerException?.Message ?? ex.Message));
        }
    }



    // Funkcja pomocnicza formatująca dane do bezpiecznego zapisu CSV
    // (obsługa separatorów, znaków nowej linii i cudzysłowów)
    private static string Csv(object? value)
    {
        var s = value?.ToString() ?? "";
        s = s.Replace("\"", "\"\"");
        if (s.Contains(';') || s.Contains('\n') || s.Contains('\r') || s.Contains('"'))
            return $"\"{s}\"";
        return s;
    }




    /* =========================================================
   SEKCJA: POŁĄCZENIE Z BAZĄ DANYCH
   ---------------------------------------------------------
   Sekcja odpowiedzialna za sprawdzenie poprawności połączenia
   aplikacji z bazą danych PostgreSQL.
   
   Umożliwia szybkie przetestowanie, czy aplikacja ma dostęp
   do bazy danych oraz czy konfiguracja połączenia została
   wykonana poprawnie.
   
   Test realizowany jest z wykorzystaniem metody CanConnect()
   z Entity Framework Core, a wynik prezentowany jest w formie
   czytelnego komunikatu dla użytkownika.
   ========================================================= */



    // Testuje połączenie z bazą danych i wyświetla status oraz szczegóły błędu
    private void btnTestDb_Click(object sender, EventArgs e)
    {
        try
        {
            lblDbStatus.Text = "Status: sprawdzam...";
            if (txtDbDetails != null) txtDbDetails.Clear();

            using var db = new AppDbContext();

            // Najprostszy test: czy aplikacja potrafi połączyć się z bazą
            bool ok = db.Database.CanConnect();

            if (ok)
            {
                lblDbStatus.Text = "Status: Połączenie z bazą OK";
                if (txtDbDetails != null)
                    txtDbDetails.Text = $"Data: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\r\nWynik: CanConnect() = true";
            }
            else
            {
                lblDbStatus.Text = "Status: Brak połączenia z bazą";
                if (txtDbDetails != null)
                    txtDbDetails.Text = $"Data: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\r\nWynik: CanConnect() = false";
            }
        }
        catch (Exception ex)
        {
            lblDbStatus.Text = "Status: Błąd połączenia ❌";

            var details = ex.InnerException?.Message ?? ex.Message;

            if (txtDbDetails != null)
            {
                txtDbDetails.Text =
                    $"Data: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\r\n" +
                    $"Błąd: {details}\r\n\r\n" +
                    $"Typ wyjątku: {ex.GetType().Name}";
            }

            MessageBox.Show("Błąd połączenia z bazą: " + details);
        }
    }

}

