namespace TransportBooking;

using TransportBooking.Data.Context;
using Microsoft.EntityFrameworkCore;
using TransportBooking.Domain.Entities;

using System.Text;


public partial class MainForm : Form
{
    /// <summary>
    /// PROJEKT PROGRAMOWANIE OBIEKTOWE C#
    /// Temat: System rezerwacji transportu i paczek
    ///
    /// Aplikacja desktopowa (Windows Forms) współpracująca z bazą danych PostgreSQL.
    /// Umożliwia zarządzanie klientami, pojazdami, trasami i rezerwacjami (CRUD),
    /// posiada walidację danych, obsługę wyjątków oraz eksport raportu miesięcznego do CSV.
    /// 
    /// Wersja: v1.1
    /// Data: 02.02.2026
    /// 
    /// </summary>
    /// <remarks>
    /// Autor: Maksymilian Tołpa
    /// Nr albumu: 71368
    /// Prowadzący: mgr inż. Ewa Żesławska
    /// Rok: 2026
    ///
    /// Technologie: C#, .NET 8, Windows Forms, PostgreSQL, Entity Framework Core (Npgsql)
    /// </remarks>

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
    private void LoadClients(string filter)
    {
        AppDbContext db = new AppDbContext();

        List<Client> allClients = db.Clients.ToList();
        List<Client> result = new List<Client>();

        if (filter != null && filter.Trim() != "")
        {
            string search = filter.Trim().ToLower();

            for (int i = 0; i < allClients.Count; i++)
            {
                Client c = allClients[i];

                bool match = false;

                if (c.FirstName != null && c.FirstName.ToLower().Contains(search))
                    match = true;
                else if (c.LastName != null && c.LastName.ToLower().Contains(search))
                    match = true;
                else if (c.Email != null && c.Email.ToLower().Contains(search))
                    match = true;
                else if (c.Phone != null && c.Phone.ToLower().Contains(search))
                    match = true;
                else if (c.City != null && c.City.ToLower().Contains(search))
                    match = true;

                if (match)
                    result.Add(c);
            }
        }
        else
        {
            result = allClients;
        }

            // sortowanie malejąco po ClientId
            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = i + 1; j < result.Count; j++)
                {
                    if (result[i].ClientId < result[j].ClientId)
                    {
                        Client temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }

            dgvClients.DataSource = result;
        }

    // Przechowuje ID aktualnie zaznaczonego klienta w tabeli
    private long? _selectedClientId = null;

    // Obsługuje przycisk wczytujący wszystkich klientów z bazy
    private void btnLoadClients_Click(object sender, EventArgs e)
    {
        try
        {
            LoadClients("");
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
            // 1) Walidacja danych wejściowych
            string firstName, lastName, email, phone, city, address, postalCode;

            bool ok = ValidateClientInputs(out firstName, out lastName, out email, out phone, out city, out address, out postalCode);
            if (!ok)
                return;

            // 2) Połączenie z bazą
            AppDbContext db = new AppDbContext();

            // 3) Sprawdzenie czy email już istnieje
            if (email != null && email.Trim() != "")
            {
                List<Client> clients = db.Clients.ToList();

                bool exists = false;
                for (int i = 0; i < clients.Count; i++)
                {
                    Client c = clients[i];

                    if (c.Email != null && c.Email == email)
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    MessageBox.Show("Klient z takim adresem e-mail już istnieje.");
                    return;
                }
            }

            // 4) Tworzenie obiektu klienta (krok po kroku)
            Client client = new Client();
            client.FirstName = firstName;
            client.LastName = lastName;
            client.Email = email;
            client.Phone = phone;
            client.City = city;
            client.Address = address;
            client.PostalCode = postalCode;
            client.CreatedAt = DateTime.Now;

            // 5) Zapis do bazy
            db.Clients.Add(client);
            db.SaveChanges();

            // 6) Info + czyszczenie formularza + odświeżenie listy
            MessageBox.Show("Dodano klienta  ID: " + client.ClientId);
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

            object idObj = dgvClients.CurrentRow.Cells["ClientId"].Value;
            long clientId;
            if (idObj == null || !long.TryParse(idObj.ToString(), out clientId))
            {
                MessageBox.Show("Nie udało się odczytać ID klienta.");
                return;
            }

            AppDbContext db = new AppDbContext();

            // BLOKADA: klient ma rezerwacje
            List<Reservation> reservations = db.Reservations.ToList();
            for (int i = 0; i < reservations.Count; i++)
            {
                if (reservations[i].ClientId == clientId)
                {
                    MessageBox.Show("Nie można usunąć klienta — ma przypisane rezerwacje.\nUsuń/zmień rezerwacje i spróbuj ponownie.");
                    return;
                }
            }

            DialogResult confirm = MessageBox.Show(
                "Czy na pewno usunąć klienta ID: " + clientId + "?",
                "Potwierdzenie usunięcia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            // Szukanie klienta
            List<Client> clients = db.Clients.ToList();
            Client client = null;

            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].ClientId == clientId)
                {
                    client = clients[i];
                    break;
                }
            }

            if (client == null)
            {
                MessageBox.Show("Klient nie istnieje (może już został usunięty).");
                LoadClients(txtSearchClient.Text);
                return;
            }

            db.Clients.Remove(client);
            db.SaveChanges();

            MessageBox.Show("Usunięto klienta ");
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
            if (_selectedClientId == null)
            {
                MessageBox.Show("Najpierw zaznacz klienta w tabeli.");
                return;
            }

            // walidacja
            string firstName, lastName, email, phone, city, address, postalCode;
            if (!ValidateClientInputs(out firstName, out lastName, out email, out phone, out city, out address, out postalCode))
                return;

            long clientId = _selectedClientId.Value;

            AppDbContext db = new AppDbContext();

            // sprawdzenie duplikatu email
            if (email != null && email.Trim() != "")
            {
                List<Client> all = db.Clients.ToList();

                for (int i = 0; i < all.Count; i++)
                {
                    Client c = all[i];

                    if (c.Email != null && c.Email == email && c.ClientId != clientId)
                    {
                        MessageBox.Show("Inny klient ma już taki adres e-mail.");
                        return;
                    }
                }
            }

            // znalezienie klienta
            List<Client> clients = db.Clients.ToList();
            Client client = null;

            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].ClientId == clientId)
                {
                    client = clients[i];
                    break;
                }
            }

            if (client == null)
            {
                MessageBox.Show("Klient nie istnieje (mógł zostać usunięty).");
                LoadClients(txtSearchClient.Text);
                return;
            }

            // aktualizacja pól
            client.FirstName = firstName;
            client.LastName = lastName;
            client.Email = email;
            client.Phone = phone;
            client.City = city;
            client.Address = address;
            client.PostalCode = postalCode;

            db.SaveChanges();

            MessageBox.Show("Zapisano zmiany ");
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
            if (dgvClients.CurrentRow == null)
                return;

            object idObj = dgvClients.CurrentRow.Cells["ClientId"].Value;
            if (idObj == null)
                return;

            _selectedClientId = Convert.ToInt64(idObj);

            txtFirstName.Text = GetCellValue("FirstName");
            txtLastName.Text = GetCellValue("LastName");
            txtEmail.Text = GetCellValue("Email");
            txtPhone.Text = GetCellValue("Phone");
            txtAddress.Text = GetCellValue("Address");
            txtCity.Text = GetCellValue("City");
            txtPostalCode.Text = GetCellValue("PostalCode");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    private string GetCellValue(string columnName)
    {
        object value = dgvClients.CurrentRow.Cells[columnName].Value;
        if (value == null)
            return "";

        return value.ToString();
    }

    // Waliduje dane klienta (wspólna metoda dla dodawania i edycji)
    private bool ValidateClientInputs(out string firstName, out string lastName,
        out string email, out string phone, out string city, out string address, out string postalCode)
    {
        firstName = txtFirstName.Text.Trim();
        lastName = txtLastName.Text.Trim();
        email = txtEmail.Text.Trim();
        phone = txtPhone.Text.Trim();
        city = txtCity.Text.Trim();
        address = txtAddress.Text.Trim();
        postalCode = txtPostalCode.Text.Trim();

        if (firstName.Length < 2) { MessageBox.Show("Imię musi mieć co najmniej 2 znaki."); return false; }
        if (lastName.Length < 2) { MessageBox.Show("Nazwisko musi mieć co najmniej 2 znaki."); return false; }

        // Email (jeśli podany)
        if (email != "")
        {
            int at = email.IndexOf('@');
            int dot = email.LastIndexOf('.');
            if (at <= 0 || dot <= at + 1 || dot >= email.Length - 1)
            {
                MessageBox.Show("Podaj poprawny adres e-mail.");
                return false;
            }
            if (email.Length > 120) { MessageBox.Show("Email max 120 znaków."); return false; }
        }

        // Telefon (jeśli podany) 7–15 cyfr po usunięciu spacji, myślników, plusa
        if (phone != "")
        {
            string cleaned = phone.Replace(" ", "").Replace("-", "");
            if (cleaned.StartsWith("+")) cleaned = cleaned.Substring(1);

            if (cleaned.Length < 7 || cleaned.Length > 15 || !IsAllDigits(cleaned))
            {
                MessageBox.Show("Podaj poprawny numer telefonu (7-15 cyfr).");
                return false;
            }
            if (phone.Length > 20) { MessageBox.Show("Telefon max 20 znaków."); return false; }
        }

        // Kod pocztowy (jeśli podany) 00-000 lub 00000
        if (postalCode != "")
        {
            string pc = postalCode.Replace(" ", "");

            bool ok = false;
            if (pc.Length == 6 && pc[2] == '-')
            {
                ok = IsAllDigits(pc.Substring(0, 2)) && IsAllDigits(pc.Substring(3, 3));
            }
            else if (pc.Length == 5)
            {
                ok = IsAllDigits(pc);
            }

            if (!ok)
            {
                MessageBox.Show("Kod pocztowy jest niepoprawny. Użyj formatu 00-000 lub 00000.");
                return false;
            }
            if (postalCode.Length > 10) { MessageBox.Show("Kod pocztowy max 10 znaków."); return false; }
        }

        if (address != "" && address.Length > 120) { MessageBox.Show("Adres max 120 znaków."); return false; }
        if (city != "" && city.Length > 60) { MessageBox.Show("Miasto max 60 znaków."); return false; }

        return true;
    }
    // Sprawdza czy cały napis składa się z cyfr
    private bool IsAllDigits(string text)
    {
        if (text == null || text == "") return false;

        for (int i = 0; i < text.Length; i++)
        {
            if (!char.IsDigit(text[i]))
                return false;
        }
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
    private void LoadVehicles(string filter)
    {
        AppDbContext db = new AppDbContext();

        List<Vehicle> allVehicles = db.Vehicles.ToList();
        List<Vehicle> result = new List<Vehicle>();

        if (filter != null) filter = filter.Trim().ToLower();

        // filtrowanie (jeśli coś wpisano)
        if (filter != null && filter != "")
        {
            for (int i = 0; i < allVehicles.Count; i++)
            {
                Vehicle v = allVehicles[i];

                string plate = (v.PlateNumber == null) ? "" : v.PlateNumber.ToLower();
                string model = (v.Model == null) ? "" : v.Model.ToLower();

                if (plate.Contains(filter) || model.Contains(filter))
                    result.Add(v);
            }
        }
        else
        {
            result = allVehicles;
        }

        // sortowanie malejąco po VehicleId
        for (int i = 0; i < result.Count - 1; i++)
        {
            for (int j = i + 1; j < result.Count; j++)
            {
                if (result[i].VehicleId < result[j].VehicleId)
                {
                    Vehicle temp = result[i];
                    result[i] = result[j];
                    result[j] = temp;
                }
            }
        }

        dgvVehicles.DataSource = result;
    }

    // Obsługuje przycisk wczytujący wszystkie pojazdy z bazy danych
    private void btnLoadVehicles_Click_1(object sender, EventArgs e)
    {
        try
        {
            LoadVehicles("");
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

        object idObj = dgvVehicles.CurrentRow.Cells["VehicleId"].Value;
        if (idObj == null) return;

        _selectedVehicleId = Convert.ToInt64(idObj);

        txtPlateNumber.Text = GetVehicleCellText("PlateNumber");
        txtVehicleModel.Text = GetVehicleCellText("Model");
        txtSeats.Text = GetVehicleCellText("Seats");

        object activeObj = dgvVehicles.CurrentRow.Cells["Active"].Value;
        chkVehicleActive.Checked = (activeObj != null && Convert.ToBoolean(activeObj));
    }

    // Proste pobranie tekstu z komórki
    private string GetVehicleCellText(string columnName)
    {
        object value = dgvVehicles.CurrentRow.Cells[columnName].Value;
        if (value == null) return "";
        return value.ToString();
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
            string plate, model;
            int seats;
            bool active;

            if (!ValidateVehicleInputs(out plate, out model, out seats, out active))
                return;

            AppDbContext db = new AppDbContext();

            // sprawdzenie czy pojazd już istnieje
            List<Vehicle> vehicles = db.Vehicles.ToList();
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].PlateNumber == plate)
                {
                    MessageBox.Show("Pojazd z takim numerem rejestracyjnym już istnieje.");
                    return;
                }
            }

            // dodanie pojazdu
            Vehicle vehicle = new Vehicle();
            vehicle.PlateNumber = plate;
            vehicle.Model = model;
            vehicle.Seats = seats;
            vehicle.Active = active;

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            MessageBox.Show("Dodano pojazd ");
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
            if (_selectedVehicleId == null)
            {
                MessageBox.Show("Zaznacz pojazd w tabeli.");
                return;
            }

            string plate, model;
            int seats;
            bool active;

            if (!ValidateVehicleInputs(out plate, out model, out seats, out active))
                return;

            long vehicleId = _selectedVehicleId.Value;

            AppDbContext db = new AppDbContext();
            List<Vehicle> vehicles = db.Vehicles.ToList();

            // sprawdzenie duplikatu numeru rejestracyjnego
            for (int i = 0; i < vehicles.Count; i++)
            {
                Vehicle v = vehicles[i];

                if (v.PlateNumber == plate && v.VehicleId != vehicleId)
                {
                    MessageBox.Show("Inny pojazd ma już taki numer rejestracyjny.");
                    return;
                }
            }

            // znalezienie pojazdu
            Vehicle vehicle = null;
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].VehicleId == vehicleId)
                {
                    vehicle = vehicles[i];
                    break;
                }
            }

            if (vehicle == null)
            {
                MessageBox.Show("Pojazd nie istnieje.");
                LoadVehicles("");
                return;
            }

            // aktualizacja danych
            vehicle.PlateNumber = plate;
            vehicle.Model = model;
            vehicle.Seats = seats;
            vehicle.Active = active;

            db.SaveChanges();

            MessageBox.Show("Zapisano zmiany ");
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
            if (_selectedVehicleId == null) { MessageBox.Show("Zaznacz pojazd w tabeli."); return; }

            AppDbContext db = new AppDbContext();
            long vehicleId = _selectedVehicleId.Value;

            // sprawdzenie czy pojazd jest używany w trasach
            List<Route> routes = db.Routes.ToList();
            for (int i = 0; i < routes.Count; i++)
                if (routes[i].VehicleId == vehicleId)
                {
                    MessageBox.Show("Nie można usunąć pojazdu — jest przypisany do tras.");
                    return;
                }

            if (MessageBox.Show("Czy na pewno usunąć pojazd?", "Potwierdzenie",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            // znalezienie pojazdu
            List<Vehicle> vehicles = db.Vehicles.ToList();
            Vehicle vehicle = null;
            for (int i = 0; i < vehicles.Count; i++)
                if (vehicles[i].VehicleId == vehicleId) { vehicle = vehicles[i]; break; }

            if (vehicle == null) return;

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            MessageBox.Show("Usunięto pojazd ");
            btnClearVehicleForm_Click(sender, e);
            LoadVehicles("");
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
        AppDbContext db = new AppDbContext();

        List<Vehicle> vehicles = db.Vehicles.ToList();
        List<KeyValuePair<long, string>> items = new List<KeyValuePair<long, string>>();

        // sortowanie malejąco po ID
        for (int i = 0; i < vehicles.Count - 1; i++)
            for (int j = i + 1; j < vehicles.Count; j++)
                if (vehicles[i].VehicleId < vehicles[j].VehicleId)
                {
                    Vehicle tmp = vehicles[i];
                    vehicles[i] = vehicles[j];
                    vehicles[j] = tmp;
                }

        // budowanie listy do ComboBox
        for (int i = 0; i < vehicles.Count; i++)
        {
            Vehicle v = vehicles[i];
            string text = v.PlateNumber + " | " + v.Model + " | miejsca: " + v.Seats;
            items.Add(new KeyValuePair<long, string>(v.VehicleId, text));
        }

        cmbRouteVehicle.DataSource = items;
        cmbRouteVehicle.DisplayMember = "Value";
        cmbRouteVehicle.ValueMember = "Key";
    }

    // Ładuje listę tras z bazy danych (opcjonalnie z filtrem wyszukiwania)
    private void LoadRoutes(string filter)
    {
        AppDbContext db = new AppDbContext();

        List<Route> allRoutes = db.Routes.ToList();
        List<Route> result = new List<Route>();

        if (filter != null) filter = filter.Trim().ToLower();

        // filtrowanie
        if (filter != null && filter != "")
        {
            for (int i = 0; i < allRoutes.Count; i++)
            {
                Route r = allRoutes[i];

                string start = (r.StartCity == null) ? "" : r.StartCity.ToLower();
                string end = (r.EndCity == null) ? "" : r.EndCity.ToLower();

                if (start.Contains(filter) || end.Contains(filter))
                    result.Add(r);
            }
        }
        else
        {
            result = allRoutes;
        }

        // sortowanie malejąco po RouteId
        for (int i = 0; i < result.Count - 1; i++)
            for (int j = i + 1; j < result.Count; j++)
                if (result[i].RouteId < result[j].RouteId)
                {
                    Route tmp = result[i];
                    result[i] = result[j];
                    result[j] = tmp;
                }

        dgvRoutes.DataSource = result;
    }

    // Obsługuje przycisk wczytujący wszystkie trasy oraz pojazdy do ComboBox
    private void btnLoadRoutes_Click(object sender, EventArgs e)
    {
        try
        {
            LoadVehiclesToRouteCombo();
            LoadVehicles("");
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

            object idObj = dgvRoutes.CurrentRow.Cells["RouteId"].Value;
            if (idObj == null) return;

            _selectedRouteId = Convert.ToInt64(idObj);

            txtStartCity.Text = GetRouteCellText("StartCity");
            txtEndCity.Text = GetRouteCellText("EndCity");
            txtPricePerson.Text = GetRouteCellText("PricePerson");

            object depObj = dgvRoutes.CurrentRow.Cells["DepartureTime"].Value;
            if (depObj != null) dtpDepartureTime.Value = Convert.ToDateTime(depObj);

            object vehicleIdObj = dgvRoutes.CurrentRow.Cells["VehicleId"].Value;
            if (vehicleIdObj != null) cmbRouteVehicle.SelectedValue = Convert.ToInt64(vehicleIdObj);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    // Pobiera tekst z danej kolumny w zaznaczonym wierszu tabeli tras
    private string GetRouteCellText(string columnName)
    {
        object value = dgvRoutes.CurrentRow.Cells[columnName].Value;
        if (value == null) return "";
        return value.ToString();
    }

    // Waliduje dane trasy (pojazd, miasta, data wyjazdu, cena)
    private bool ValidateRouteInputs(out long vehicleId, out string start, out string end,
        out DateTime departure, out decimal pricePerson)
    {
        vehicleId = 0;
        pricePerson = 0;

        start = txtStartCity.Text.Trim();
        end = txtEndCity.Text.Trim();
        departure = dtpDepartureTime.Value;

        // pojazd
        if (cmbRouteVehicle.SelectedValue == null ||
            !long.TryParse(cmbRouteVehicle.SelectedValue.ToString(), out vehicleId))
        {
            MessageBox.Show("Wybierz pojazd dla trasy.");
            return false;
        }

        // miasta
        if (start.Length < 2) { MessageBox.Show("Miasto startowe jest za krótkie."); return false; }
        if (end.Length < 2) { MessageBox.Show("Miasto docelowe jest za krótkie."); return false; }

        // cena
        string raw = txtPricePerson.Text.Trim().Replace(',', '.');
        if (!decimal.TryParse(raw,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out pricePerson) || pricePerson < 0)
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
            long vehicleId;
            string start, end;
            DateTime departure;
            decimal price;

            if (!ValidateRouteInputs(out vehicleId, out start, out end, out departure, out price))
                return;

            AppDbContext db = new AppDbContext();

            Route route = new Route();
            route.VehicleId = vehicleId;
            route.StartCity = start;
            route.EndCity = end;
            route.DepartureTime = departure;
            route.PricePerson = price;

            db.Routes.Add(route);
            db.SaveChanges();

            MessageBox.Show("Dodano trasę ");
            btnClearRouteForm_Click(sender, e);
            LoadRoutes(txtSearchRoute.Text);
        }
        catch (Exception ex)
        {
            string details = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
            MessageBox.Show("Błąd: " + details);
        }
    }

    // Aktualizuje dane zaznaczonej trasy w bazie danych
    private void btnUpdateRoute_Click(object sender, EventArgs e)
    {
        try
        {
            if (_selectedRouteId == null) { MessageBox.Show("Zaznacz trasę w tabeli."); return; }

            long vehicleId; string start, end; DateTime departure; decimal price;
            if (!ValidateRouteInputs(out vehicleId, out start, out end, out departure, out price)) return;

            AppDbContext db = new AppDbContext();
            long routeId = _selectedRouteId.Value;

            List<Route> routes = db.Routes.ToList();
            Route route = null;

            for (int i = 0; i < routes.Count; i++)
                if (routes[i].RouteId == routeId) { route = routes[i]; break; }

            if (route == null) { MessageBox.Show("Trasa nie istnieje."); LoadRoutes(""); return; }

            route.VehicleId = vehicleId;
            route.StartCity = start;
            route.EndCity = end;
            route.DepartureTime = departure; // prosto
            route.PricePerson = price;

            db.SaveChanges();

            MessageBox.Show("Zapisano zmiany ");
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
            if (_selectedRouteId == null) { MessageBox.Show("Zaznacz trasę w tabeli."); return; }

            AppDbContext db = new AppDbContext();
            long routeId = _selectedRouteId.Value;

            // blokada: są rezerwacje na trasę
            List<Reservation> reservations = db.Reservations.ToList();
            for (int i = 0; i < reservations.Count; i++)
                if (reservations[i].RouteId == routeId)
                {
                    MessageBox.Show("Nie można usunąć trasy — istnieją rezerwacje na tę trasę.");
                    return;
                }

            if (MessageBox.Show("Czy na pewno usunąć trasę?", "Potwierdzenie",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            // znalezienie trasy
            List<Route> routes = db.Routes.ToList();
            Route route = null;
            for (int i = 0; i < routes.Count; i++)
                if (routes[i].RouteId == routeId) { route = routes[i]; break; }

            if (route == null) return;

            db.Routes.Remove(route);
            db.SaveChanges();

            MessageBox.Show("Usunięto trasę ");
            btnClearRouteForm_Click(sender, e);
            LoadRoutes("");
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
        AppDbContext db = new AppDbContext();

        List<Client> clients = db.Clients.ToList();
        List<KeyValuePair<long, string>> items = new List<KeyValuePair<long, string>>();

        // sortowanie malejąco po ID
        for (int i = 0; i < clients.Count - 1; i++)
            for (int j = i + 1; j < clients.Count; j++)
                if (clients[i].ClientId < clients[j].ClientId)
                {
                    Client tmp = clients[i];
                    clients[i] = clients[j];
                    clients[j] = tmp;
                }

        // budowanie listy do ComboBox
        for (int i = 0; i < clients.Count; i++)
        {
            Client c = clients[i];
            string text = c.FirstName + " " + c.LastName + " | " + c.Email;
            items.Add(new KeyValuePair<long, string>(c.ClientId, text));
        }

        cmbResClient.DataSource = items;
        cmbResClient.DisplayMember = "Value";
        cmbResClient.ValueMember = "Key";
    }

    // Wczytuje listę tras do ComboBox (wybór trasy dla rezerwacji)
    // Wczytuje listę tras do ComboBox (wybór trasy dla rezerwacji)
    private void LoadRoutesToReservationCombo()
    {
        AppDbContext db = new AppDbContext();

        List<Route> routes = db.Routes.ToList();
        List<KeyValuePair<long, string>> items = new List<KeyValuePair<long, string>>();

        // sortowanie malejąco po ID
        for (int i = 0; i < routes.Count - 1; i++)
            for (int j = i + 1; j < routes.Count; j++)
                if (routes[i].RouteId < routes[j].RouteId)
                {
                    Route tmp = routes[i];
                    routes[i] = routes[j];
                    routes[j] = tmp;
                }

        // budowanie listy do ComboBox
        for (int i = 0; i < routes.Count; i++)
        {
            Route r = routes[i];
            string text = r.StartCity + " -> " + r.EndCity + " | " + r.DepartureTime + " | " + r.PricePerson + " zł";
            items.Add(new KeyValuePair<long, string>(r.RouteId, text));
        }

        cmbResRoute.DataSource = items;
        cmbResRoute.DisplayMember = "Value";
        cmbResRoute.ValueMember = "Key";
    }

    // Ładuje listę rezerwacji z bazy danych (opcjonalnie z filtrem wyszukiwania)
    private void LoadReservations(string filter)
    {
        AppDbContext db = new AppDbContext();

        List<Reservation> all = db.Reservations.ToList();
        List<Reservation> result = new List<Reservation>();

        if (filter != null) filter = filter.Trim().ToLower();

        // filtrowanie
        if (filter != null && filter != "")
        {
            for (int i = 0; i < all.Count; i++)
            {
                Reservation r = all[i];

                string type = (r.ServiceType == null) ? "" : r.ServiceType.ToLower();
                string status = (r.Status == null) ? "" : r.Status.ToLower();

                if (type.Contains(filter) || status.Contains(filter))
                    result.Add(r);
            }
        }
        else
        {
            result = all;
        }

        // sortowanie malejąco po ReservationId
        for (int i = 0; i < result.Count - 1; i++)
            for (int j = i + 1; j < result.Count; j++)
                if (result[i].ReservationId < result[j].ReservationId)
                {
                    Reservation tmp = result[i];
                    result[i] = result[j];
                    result[j] = tmp;
                }

        dgvReservations.DataSource = result;
    }

    // Obsługuje przycisk wczytujący wszystkie rezerwacje oraz dane do ComboBoxów
    private void btnLoadReservations_Click(object sender, EventArgs e)
    {
        try
        {
            LoadClientsToReservationCombo();
            LoadRoutesToReservationCombo();
            LoadReservations("");
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

            object idObj = dgvReservations.CurrentRow.Cells["ReservationId"].Value;
            if (idObj == null) return;
            _selectedReservationId = Convert.ToInt64(idObj);

            object clientIdObj = dgvReservations.CurrentRow.Cells["ClientId"].Value;
            if (clientIdObj != null) cmbResClient.SelectedValue = Convert.ToInt64(clientIdObj);

            object routeIdObj = dgvReservations.CurrentRow.Cells["RouteId"].Value;
            if (routeIdObj != null) cmbResRoute.SelectedValue = Convert.ToInt64(routeIdObj);

            cmbServiceType.SelectedItem = GetResCellText("ServiceType", "PERSON");
            cmbResStatus.SelectedItem = GetResCellText("Status", "NEW");

            object createdObj = dgvReservations.CurrentRow.Cells["CreatedAt"].Value;
            if (createdObj != null) dtpResCreatedAt.Value = Convert.ToDateTime(createdObj);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    // Pobiera tekst z tabeli rezerwacji, a jeśli brak to zwraca wartość domyślną
    private string GetResCellText(string columnName, string defaultValue)
    {
        object value = dgvReservations.CurrentRow.Cells[columnName].Value;
        if (value == null) return defaultValue;

        string text = value.ToString();
        if (text == null || text.Trim() == "") return defaultValue;

        return text;
    }


    // Waliduje dane rezerwacji (klient, trasa, typ usługi, status, data)
    private bool ValidateReservationInputs(out long clientId, out long routeId, out string serviceType, out string status, out DateTime createdAtUtc)
    {
        clientId = 0;
        routeId = 0;
        createdAtUtc = DateTime.UtcNow;

        serviceType = "PERSON";
        status = "NEW";

        if (cmbServiceType.SelectedItem != null) serviceType = cmbServiceType.SelectedItem.ToString();
        if (cmbResStatus.SelectedItem != null) status = cmbResStatus.SelectedItem.ToString();

        if (cmbResClient.SelectedValue == null || !long.TryParse(cmbResClient.SelectedValue.ToString(), out clientId))
        {
            MessageBox.Show("Wybierz klienta.");
            return false;
        }

        if (cmbResRoute.SelectedValue == null || !long.TryParse(cmbResRoute.SelectedValue.ToString(), out routeId))
        {
            MessageBox.Show("Wybierz trasę.");
            return false;
        }

        if (serviceType != "PERSON" && serviceType != "PACKAGE")
        {
            MessageBox.Show("Niepoprawny typ usługi.");
            return false;
        }

        if (status.Length < 2)
        {
            MessageBox.Show("Status jest niepoprawny.");
            return false;
        }

        createdAtUtc = dtpResCreatedAt.Value.ToUniversalTime();
        return true;
    }

    // Dodaje nową rezerwację do bazy danych po poprawnej walidacji danych
    private void btnAddReservation_Click(object sender, EventArgs e)
    {
        try
        {
            long clientId, routeId; string serviceType, status; DateTime createdAtUtc;
            if (!ValidateReservationInputs(out clientId, out routeId, out serviceType, out status, out createdAtUtc)) return;

            AppDbContext db = new AppDbContext();

            Reservation res = new Reservation();
            res.ClientId = clientId;
            res.RouteId = routeId;
            res.ServiceType = serviceType;
            res.Status = status;
            res.CreatedAt = createdAtUtc;

            db.Reservations.Add(res);
            db.SaveChanges();

            MessageBox.Show("Dodano rezerwację ");
            btnClearReservationForm_Click(sender, e);
            LoadReservations(txtSearchReservation.Text);
        }
        catch (Exception ex)
        {
            string msg = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
            MessageBox.Show("Błąd: " + msg);
        }
    }


    // Aktualizuje dane zaznaczonej rezerwacji w bazie danych
    private void btnUpdateReservation_Click(object sender, EventArgs e)
    {
        if (_selectedReservationId == null) { MessageBox.Show("Zaznacz rezerwację w tabeli."); return; }

        long clientId, routeId; string serviceType, status; DateTime createdAtUtc;
        if (!ValidateReservationInputs(out clientId, out routeId, out serviceType, out status, out createdAtUtc)) return;

        AppDbContext db = new AppDbContext();
        long resId = _selectedReservationId.Value;

        List<Reservation> reservations = db.Reservations.ToList();
        Reservation res = null;

        for (int i = 0; i < reservations.Count; i++)
            if (reservations[i].ReservationId == resId) { res = reservations[i]; break; }

            if (res == null) { MessageBox.Show("Rezerwacja nie istnieje."); LoadReservations(""); return; }

        res.ClientId = clientId;
        res.RouteId = routeId;
        res.ServiceType = serviceType;
        res.Status = status;
        res.CreatedAt = createdAtUtc;

        db.SaveChanges();

        MessageBox.Show("Zapisano zmiany ");
        LoadReservations(txtSearchReservation.Text);
    }

    // Usuwa zaznaczoną rezerwację z bazy danych (jeśli brak powiązań)
    private void btnDeleteReservation_Click(object sender, EventArgs e)
    {
        if (_selectedReservationId == null) { MessageBox.Show("Zaznacz rezerwację w tabeli."); return; }

        AppDbContext db = new AppDbContext();
        long resId = _selectedReservationId.Value;

        // sprawdzenie powiązań z płatnościami
        List<Payment> payments = db.Payments.ToList();
        for (int i = 0; i < payments.Count; i++)
            if (payments[i].ReservationId == resId)
            {
                MessageBox.Show("Nie można usunąć rezerwacji — istnieją powiązane płatności lub paczki.");
                return;
            }

        // sprawdzenie powiązań z paczkami
        List<Package> packages = db.Packages.ToList();
        for (int i = 0; i < packages.Count; i++)
            if (packages[i].ReservationId == resId)
            {
                MessageBox.Show("Nie można usunąć rezerwacji — istnieją powiązane płatności lub paczki.");
                return;
            }

        if (MessageBox.Show("Czy na pewno usunąć rezerwację?", "Potwierdzenie",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        // znalezienie rezerwacji
        List<Reservation> reservations = db.Reservations.ToList();
        Reservation res = null;
        for (int i = 0; i < reservations.Count; i++)
            if (reservations[i].ReservationId == resId) { res = reservations[i]; break; }

        if (res == null) return;

        db.Reservations.Remove(res);
        db.SaveChanges();

        MessageBox.Show("Usunięto rezerwację ");
        btnClearReservationForm_Click(sender, e);
        LoadReservations("");
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
            // Pobranie roku i miesiąca z kontrolki
            int year = dtpReportMonth.Value.Year;
            int month = dtpReportMonth.Value.Month;

            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1);

            AppDbContext db = new AppDbContext();

            // Lista wynikowa
            List<string> lines = new List<string>();

            // Nagłówek CSV
            lines.Add("ReservationId;CreatedAt;ServiceType;Status;ClientId;ClientName;ClientEmail;RouteId;StartCity;EndCity;DepartureTime;PricePerson");

            // Pobranie wszystkich rezerwacji
            List<Reservation> reservations = db.Reservations.ToList();
            List<Client> clients = db.Clients.ToList();
            List<Route> routes = db.Routes.ToList();

            // Przetwarzanie danych
            foreach (Reservation r in reservations)
            {
                if (r.CreatedAt >= startDate && r.CreatedAt < endDate)
                {
                    Client client = null;
                    Route route = null;

                    // Szukanie klienta
                    foreach (Client c in clients)
                    {
                        if (c.ClientId == r.ClientId)
                        {
                            client = c;
                            break;
                        }
                    }

                    // Szukanie trasy
                    foreach (Route ro in routes)
                    {
                        if (ro.RouteId == r.RouteId)
                        {
                            route = ro;
                            break;
                        }
                    }

                    if (client != null && route != null)
                    {
                        string line =
                            Csv(r.ReservationId) + ";" +
                            Csv(r.CreatedAt.ToString("yyyy-MM-dd HH:mm")) + ";" +
                            Csv(r.ServiceType) + ";" +
                            Csv(r.Status) + ";" +
                            Csv(r.ClientId) + ";" +
                            Csv(client.FirstName + " " + client.LastName) + ";" +
                            Csv(client.Email) + ";" +
                            Csv(r.RouteId) + ";" +
                            Csv(route.StartCity) + ";" +
                            Csv(route.EndCity) + ";" +
                            Csv(route.DepartureTime.ToString("yyyy-MM-dd HH:mm")) + ";" +
                            Csv(route.PricePerson.ToString("0.00"));

                        lines.Add(line);
                    }
                }
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "raport_rezerwacje_" + year + "_" + month + ".csv";
            sfd.Filter = "CSV (*.csv)|*.csv";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            File.WriteAllLines(sfd.FileName, lines, Encoding.UTF8);

            MessageBox.Show("Zapisano CSV\nRekordów: " + (lines.Count - 1));
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    // Funkcja pomocnicza do bezpiecznego zapisu CSV
    private static string Csv(object value)
    {
        if (value == null)
            return "";

        string text = value.ToString();

        if (text.Contains(";") || text.Contains("\""))
        {
            text = text.Replace("\"", "\"\"");
            text = "\"" + text + "\"";
        }

        return text;
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

