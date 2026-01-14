namespace TransportBooking
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabPage9 = new TabPage();
            txtDbDetails = new TextBox();
            lblDbStatus = new Label();
            btnTestDb = new Button();
            tabPage5 = new TabPage();
            label25 = new Label();
            btnExportCsv = new Button();
            dtpReportMonth = new DateTimePicker();
            tabPage4 = new TabPage();
            btnClearReservationForm = new Button();
            btnDeleteReservation = new Button();
            btnUpdateReservation = new Button();
            btnAddReservation = new Button();
            btnLoadReservations = new Button();
            txtSearchReservation = new TextBox();
            btnSearchReservation = new Button();
            label24 = new Label();
            dtpResCreatedAt = new DateTimePicker();
            label23 = new Label();
            cmbResStatus = new ComboBox();
            label22 = new Label();
            cmbServiceType = new ComboBox();
            label21 = new Label();
            cmbResRoute = new ComboBox();
            label20 = new Label();
            cmbResClient = new ComboBox();
            dgvReservations = new DataGridView();
            tabPage3 = new TabPage();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            btnClearRouteForm = new Button();
            btnDeleteRoute = new Button();
            btnLoadRoutes = new Button();
            btnAddRoute = new Button();
            btnUpdateRoute = new Button();
            btnSearchRoute = new Button();
            txtSearchRoute = new TextBox();
            txtPricePerson = new TextBox();
            dtpDepartureTime = new DateTimePicker();
            txtEndCity = new TextBox();
            txtStartCity = new TextBox();
            cmbRouteVehicle = new ComboBox();
            dgvRoutes = new DataGridView();
            tabVehicles = new TabPage();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            btnUpdateVehicle = new Button();
            btnClearVehicleForm = new Button();
            btnDeleteVehicle = new Button();
            btnAddVehicle = new Button();
            btnLoadVehicles = new Button();
            btnSearchVehicle = new Button();
            txtSearchVehicle = new TextBox();
            chkVehicleActive = new CheckBox();
            txtVehicleModel = new TextBox();
            txtSeats = new TextBox();
            txtPlateNumber = new TextBox();
            dgvVehicles = new DataGridView();
            tabPage1 = new TabPage();
            btnClearClientForm = new Button();
            btnUpdateClient = new Button();
            btnDeleteClient = new Button();
            btnSearchClient = new Button();
            txtSearchClient = new TextBox();
            txtPostalCode = new TextBox();
            txtCity = new TextBox();
            txtAddress = new TextBox();
            txtPhone = new TextBox();
            txtLastName = new TextBox();
            txtEmail = new TextBox();
            txtFirstName = new TextBox();
            label10 = new Label();
            label11 = new Label();
            label9 = new Label();
            btnLoadClients = new Button();
            btnAddClient = new Button();
            dgvClients = new DataGridView();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            tabControl1 = new TabControl();
            tabPage9.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReservations).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoutes).BeginInit();
            tabVehicles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClients).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabPage9
            // 
            tabPage9.Controls.Add(txtDbDetails);
            tabPage9.Controls.Add(lblDbStatus);
            tabPage9.Controls.Add(btnTestDb);
            tabPage9.Location = new Point(4, 29);
            tabPage9.Name = "tabPage9";
            tabPage9.Padding = new Padding(3);
            tabPage9.Size = new Size(1124, 451);
            tabPage9.TabIndex = 8;
            tabPage9.Text = "Test połączenia z bazą";
            tabPage9.UseVisualStyleBackColor = true;
            // 
            // txtDbDetails
            // 
            txtDbDetails.Location = new Point(15, 91);
            txtDbDetails.Multiline = true;
            txtDbDetails.Name = "txtDbDetails";
            txtDbDetails.ReadOnly = true;
            txtDbDetails.ScrollBars = ScrollBars.Vertical;
            txtDbDetails.Size = new Size(366, 105);
            txtDbDetails.TabIndex = 2;
            // 
            // lblDbStatus
            // 
            lblDbStatus.AutoSize = true;
            lblDbStatus.Location = new Point(15, 58);
            lblDbStatus.Name = "lblDbStatus";
            lblDbStatus.Size = new Size(71, 20);
            lblDbStatus.TabIndex = 1;
            lblDbStatus.Text = "Status: —";
            // 
            // btnTestDb
            // 
            btnTestDb.Location = new Point(15, 15);
            btnTestDb.Name = "btnTestDb";
            btnTestDb.Size = new Size(366, 29);
            btnTestDb.TabIndex = 0;
            btnTestDb.Text = "Test połączenia z bazą";
            btnTestDb.UseVisualStyleBackColor = true;
            btnTestDb.Click += btnTestDb_Click;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(label25);
            tabPage5.Controls.Add(btnExportCsv);
            tabPage5.Controls.Add(dtpReportMonth);
            tabPage5.Location = new Point(4, 29);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(1124, 451);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Raport CSV";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(22, 21);
            label25.Name = "label25";
            label25.Size = new Size(223, 20);
            label25.TabIndex = 2;
            label25.Text = "Wybierz miesiąc i kliknij eksport.";
            // 
            // btnExportCsv
            // 
            btnExportCsv.Location = new Point(22, 87);
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.Size = new Size(333, 29);
            btnExportCsv.TabIndex = 1;
            btnExportCsv.Text = "Eksportuj CSV";
            btnExportCsv.UseVisualStyleBackColor = true;
            btnExportCsv.Click += btnExportCsv_Click;
            // 
            // dtpReportMonth
            // 
            dtpReportMonth.CustomFormat = "MM.yyyy";
            dtpReportMonth.Format = DateTimePickerFormat.Custom;
            dtpReportMonth.Location = new Point(22, 54);
            dtpReportMonth.Name = "dtpReportMonth";
            dtpReportMonth.ShowUpDown = true;
            dtpReportMonth.Size = new Size(333, 27);
            dtpReportMonth.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(btnClearReservationForm);
            tabPage4.Controls.Add(btnDeleteReservation);
            tabPage4.Controls.Add(btnUpdateReservation);
            tabPage4.Controls.Add(btnAddReservation);
            tabPage4.Controls.Add(btnLoadReservations);
            tabPage4.Controls.Add(txtSearchReservation);
            tabPage4.Controls.Add(btnSearchReservation);
            tabPage4.Controls.Add(label24);
            tabPage4.Controls.Add(dtpResCreatedAt);
            tabPage4.Controls.Add(label23);
            tabPage4.Controls.Add(cmbResStatus);
            tabPage4.Controls.Add(label22);
            tabPage4.Controls.Add(cmbServiceType);
            tabPage4.Controls.Add(label21);
            tabPage4.Controls.Add(cmbResRoute);
            tabPage4.Controls.Add(label20);
            tabPage4.Controls.Add(cmbResClient);
            tabPage4.Controls.Add(dgvReservations);
            tabPage4.Location = new Point(4, 29);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1124, 451);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Rezerwacje";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnClearReservationForm
            // 
            btnClearReservationForm.Location = new Point(6, 413);
            btnClearReservationForm.Name = "btnClearReservationForm";
            btnClearReservationForm.Size = new Size(280, 29);
            btnClearReservationForm.TabIndex = 17;
            btnClearReservationForm.Text = "Wyczyść pola";
            btnClearReservationForm.UseVisualStyleBackColor = true;
            btnClearReservationForm.Click += btnClearReservationForm_Click;
            // 
            // btnDeleteReservation
            // 
            btnDeleteReservation.Location = new Point(6, 378);
            btnDeleteReservation.Name = "btnDeleteReservation";
            btnDeleteReservation.Size = new Size(280, 29);
            btnDeleteReservation.TabIndex = 16;
            btnDeleteReservation.Text = "Usuń rezerwację";
            btnDeleteReservation.UseVisualStyleBackColor = true;
            btnDeleteReservation.Click += btnDeleteReservation_Click;
            // 
            // btnUpdateReservation
            // 
            btnUpdateReservation.Location = new Point(147, 343);
            btnUpdateReservation.Name = "btnUpdateReservation";
            btnUpdateReservation.Size = new Size(139, 29);
            btnUpdateReservation.TabIndex = 15;
            btnUpdateReservation.Text = "Zapisz rezerwację";
            btnUpdateReservation.UseVisualStyleBackColor = true;
            btnUpdateReservation.Click += btnUpdateReservation_Click;
            // 
            // btnAddReservation
            // 
            btnAddReservation.Location = new Point(6, 343);
            btnAddReservation.Name = "btnAddReservation";
            btnAddReservation.Size = new Size(139, 29);
            btnAddReservation.TabIndex = 14;
            btnAddReservation.Text = "Dodaj rezerwację";
            btnAddReservation.UseVisualStyleBackColor = true;
            btnAddReservation.Click += btnAddReservation_Click;
            // 
            // btnLoadReservations
            // 
            btnLoadReservations.Location = new Point(315, 105);
            btnLoadReservations.Name = "btnLoadReservations";
            btnLoadReservations.Size = new Size(155, 60);
            btnLoadReservations.TabIndex = 13;
            btnLoadReservations.Text = "Wczytaj rezerwacje";
            btnLoadReservations.UseVisualStyleBackColor = true;
            btnLoadReservations.Click += btnLoadReservations_Click;
            // 
            // txtSearchReservation
            // 
            txtSearchReservation.Location = new Point(315, 6);
            txtSearchReservation.Name = "txtSearchReservation";
            txtSearchReservation.Size = new Size(155, 27);
            txtSearchReservation.TabIndex = 12;
            // 
            // btnSearchReservation
            // 
            btnSearchReservation.Location = new Point(315, 39);
            btnSearchReservation.Name = "btnSearchReservation";
            btnSearchReservation.Size = new Size(155, 60);
            btnSearchReservation.TabIndex = 11;
            btnSearchReservation.Text = "Wyszkuj rezerwację";
            btnSearchReservation.UseVisualStyleBackColor = true;
            btnSearchReservation.Click += btnSearchReservation_Click;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(6, 287);
            label24.Name = "label24";
            label24.Size = new Size(246, 20);
            label24.TabIndex = 10;
            label24.Text = "Automatyczie ( ew- zmiana ręczna )";
            // 
            // dtpResCreatedAt
            // 
            dtpResCreatedAt.Location = new Point(6, 310);
            dtpResCreatedAt.Name = "dtpResCreatedAt";
            dtpResCreatedAt.Size = new Size(280, 27);
            dtpResCreatedAt.TabIndex = 9;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(6, 212);
            label23.Name = "label23";
            label23.Size = new Size(49, 20);
            label23.TabIndex = 8;
            label23.Text = "Status";
            // 
            // cmbResStatus
            // 
            cmbResStatus.FormattingEnabled = true;
            cmbResStatus.Location = new Point(6, 235);
            cmbResStatus.Name = "cmbResStatus";
            cmbResStatus.Size = new Size(280, 28);
            cmbResStatus.TabIndex = 7;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(6, 142);
            label22.Name = "label22";
            label22.Size = new Size(110, 20);
            label22.TabIndex = 6;
            label22.Text = "Osoba / Paczka";
            // 
            // cmbServiceType
            // 
            cmbServiceType.FormattingEnabled = true;
            cmbServiceType.Location = new Point(6, 165);
            cmbServiceType.Name = "cmbServiceType";
            cmbServiceType.Size = new Size(280, 28);
            cmbServiceType.TabIndex = 5;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(6, 74);
            label21.Name = "label21";
            label21.Size = new Size(88, 20);
            label21.TabIndex = 4;
            label21.Text = "Wybór trasy";
            // 
            // cmbResRoute
            // 
            cmbResRoute.FormattingEnabled = true;
            cmbResRoute.Location = new Point(6, 97);
            cmbResRoute.Name = "cmbResRoute";
            cmbResRoute.Size = new Size(280, 28);
            cmbResRoute.TabIndex = 3;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(6, 6);
            label20.Name = "label20";
            label20.Size = new Size(101, 20);
            label20.TabIndex = 2;
            label20.Text = "Wybór klienta";
            // 
            // cmbResClient
            // 
            cmbResClient.FormattingEnabled = true;
            cmbResClient.Location = new Point(6, 29);
            cmbResClient.Name = "cmbResClient";
            cmbResClient.Size = new Size(280, 28);
            cmbResClient.TabIndex = 1;
            // 
            // dgvReservations
            // 
            dgvReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReservations.Location = new Point(476, 6);
            dgvReservations.Name = "dgvReservations";
            dgvReservations.RowHeadersWidth = 51;
            dgvReservations.Size = new Size(642, 436);
            dgvReservations.TabIndex = 0;
            dgvReservations.CellClick += dgvReservations_CellClick;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(label19);
            tabPage3.Controls.Add(label18);
            tabPage3.Controls.Add(label17);
            tabPage3.Controls.Add(label16);
            tabPage3.Controls.Add(label15);
            tabPage3.Controls.Add(btnClearRouteForm);
            tabPage3.Controls.Add(btnDeleteRoute);
            tabPage3.Controls.Add(btnLoadRoutes);
            tabPage3.Controls.Add(btnAddRoute);
            tabPage3.Controls.Add(btnUpdateRoute);
            tabPage3.Controls.Add(btnSearchRoute);
            tabPage3.Controls.Add(txtSearchRoute);
            tabPage3.Controls.Add(txtPricePerson);
            tabPage3.Controls.Add(dtpDepartureTime);
            tabPage3.Controls.Add(txtEndCity);
            tabPage3.Controls.Add(txtStartCity);
            tabPage3.Controls.Add(cmbRouteVehicle);
            tabPage3.Controls.Add(dgvRoutes);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1124, 451);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Trasy/Kursy";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(29, 279);
            label19.Name = "label19";
            label19.Size = new Size(147, 20);
            label19.TabIndex = 17;
            label19.Text = "Cena za przejazd (zł)";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(29, 214);
            label18.Name = "label18";
            label18.Size = new Size(250, 20);
            label18.TabIndex = 16;
            label18.Text = "Wybierz datę planowanego odjazdu";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(29, 148);
            label17.Name = "label17";
            label17.Size = new Size(186, 20);
            label17.TabIndex = 15;
            label17.Text = "Wybierz miejsce docelowe";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(29, 83);
            label16.Name = "label16";
            label16.Size = new Size(178, 20);
            label16.TabIndex = 14;
            label16.Text = "Wybierz miejsce startowe";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(29, 10);
            label15.Name = "label15";
            label15.Size = new Size(113, 20);
            label15.TabIndex = 13;
            label15.Text = "Wybierz pojazd";
            // 
            // btnClearRouteForm
            // 
            btnClearRouteForm.Location = new Point(29, 405);
            btnClearRouteForm.Name = "btnClearRouteForm";
            btnClearRouteForm.Size = new Size(289, 29);
            btnClearRouteForm.TabIndex = 12;
            btnClearRouteForm.Text = "Wyczyść pola";
            btnClearRouteForm.UseVisualStyleBackColor = true;
            btnClearRouteForm.Click += btnClearRouteForm_Click;
            // 
            // btnDeleteRoute
            // 
            btnDeleteRoute.Location = new Point(29, 370);
            btnDeleteRoute.Name = "btnDeleteRoute";
            btnDeleteRoute.Size = new Size(289, 29);
            btnDeleteRoute.TabIndex = 11;
            btnDeleteRoute.Text = "Usuń zaznaczoną trasę";
            btnDeleteRoute.UseVisualStyleBackColor = true;
            btnDeleteRoute.Click += btnDeleteRoute_Click;
            // 
            // btnLoadRoutes
            // 
            btnLoadRoutes.Location = new Point(355, 109);
            btnLoadRoutes.Name = "btnLoadRoutes";
            btnLoadRoutes.Size = new Size(201, 68);
            btnLoadRoutes.TabIndex = 10;
            btnLoadRoutes.Text = "Wczytaj wszystkie trasy";
            btnLoadRoutes.UseVisualStyleBackColor = true;
            btnLoadRoutes.Click += btnLoadRoutes_Click;
            // 
            // btnAddRoute
            // 
            btnAddRoute.Location = new Point(29, 335);
            btnAddRoute.Name = "btnAddRoute";
            btnAddRoute.Size = new Size(137, 29);
            btnAddRoute.TabIndex = 9;
            btnAddRoute.Text = "Dodaj trasę";
            btnAddRoute.UseVisualStyleBackColor = true;
            btnAddRoute.Click += btnAddRoute_Click;
            // 
            // btnUpdateRoute
            // 
            btnUpdateRoute.Location = new Point(172, 335);
            btnUpdateRoute.Name = "btnUpdateRoute";
            btnUpdateRoute.Size = new Size(146, 29);
            btnUpdateRoute.TabIndex = 8;
            btnUpdateRoute.Text = "Zapisz zmiany";
            btnUpdateRoute.UseVisualStyleBackColor = true;
            btnUpdateRoute.Click += btnUpdateRoute_Click;
            // 
            // btnSearchRoute
            // 
            btnSearchRoute.Location = new Point(355, 43);
            btnSearchRoute.Name = "btnSearchRoute";
            btnSearchRoute.Size = new Size(201, 60);
            btnSearchRoute.TabIndex = 7;
            btnSearchRoute.Text = "Wyszukaj kurs";
            btnSearchRoute.UseVisualStyleBackColor = true;
            btnSearchRoute.Click += btnSearchRoute_Click;
            // 
            // txtSearchRoute
            // 
            txtSearchRoute.Location = new Point(355, 10);
            txtSearchRoute.Name = "txtSearchRoute";
            txtSearchRoute.Size = new Size(201, 27);
            txtSearchRoute.TabIndex = 6;
            // 
            // txtPricePerson
            // 
            txtPricePerson.Location = new Point(29, 302);
            txtPricePerson.Name = "txtPricePerson";
            txtPricePerson.Size = new Size(289, 27);
            txtPricePerson.TabIndex = 5;
            // 
            // dtpDepartureTime
            // 
            dtpDepartureTime.Location = new Point(29, 237);
            dtpDepartureTime.Name = "dtpDepartureTime";
            dtpDepartureTime.Size = new Size(289, 27);
            dtpDepartureTime.TabIndex = 4;
            // 
            // txtEndCity
            // 
            txtEndCity.Location = new Point(29, 171);
            txtEndCity.Name = "txtEndCity";
            txtEndCity.Size = new Size(289, 27);
            txtEndCity.TabIndex = 3;
            // 
            // txtStartCity
            // 
            txtStartCity.Location = new Point(29, 106);
            txtStartCity.Name = "txtStartCity";
            txtStartCity.Size = new Size(289, 27);
            txtStartCity.TabIndex = 2;
            // 
            // cmbRouteVehicle
            // 
            cmbRouteVehicle.FormattingEnabled = true;
            cmbRouteVehicle.Location = new Point(29, 37);
            cmbRouteVehicle.Name = "cmbRouteVehicle";
            cmbRouteVehicle.Size = new Size(289, 28);
            cmbRouteVehicle.TabIndex = 1;
            // 
            // dgvRoutes
            // 
            dgvRoutes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoutes.Location = new Point(562, 10);
            dgvRoutes.Name = "dgvRoutes";
            dgvRoutes.RowHeadersWidth = 51;
            dgvRoutes.Size = new Size(556, 432);
            dgvRoutes.TabIndex = 0;
            dgvRoutes.CellClick += dgvRoutes_CellClick;
            // 
            // tabVehicles
            // 
            tabVehicles.Controls.Add(label14);
            tabVehicles.Controls.Add(label13);
            tabVehicles.Controls.Add(label12);
            tabVehicles.Controls.Add(btnUpdateVehicle);
            tabVehicles.Controls.Add(btnClearVehicleForm);
            tabVehicles.Controls.Add(btnDeleteVehicle);
            tabVehicles.Controls.Add(btnAddVehicle);
            tabVehicles.Controls.Add(btnLoadVehicles);
            tabVehicles.Controls.Add(btnSearchVehicle);
            tabVehicles.Controls.Add(txtSearchVehicle);
            tabVehicles.Controls.Add(chkVehicleActive);
            tabVehicles.Controls.Add(txtVehicleModel);
            tabVehicles.Controls.Add(txtSeats);
            tabVehicles.Controls.Add(txtPlateNumber);
            tabVehicles.Controls.Add(dgvVehicles);
            tabVehicles.Location = new Point(4, 29);
            tabVehicles.Name = "tabVehicles";
            tabVehicles.Padding = new Padding(3);
            tabVehicles.Size = new Size(1124, 451);
            tabVehicles.TabIndex = 1;
            tabVehicles.Text = "Pojazdy";
            tabVehicles.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(18, 153);
            label14.Name = "label14";
            label14.Size = new Size(181, 20);
            label14.TabIndex = 14;
            label14.Text = "Liczba miejsc w pojeździe";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(18, 21);
            label13.Name = "label13";
            label13.Size = new Size(198, 20);
            label13.TabIndex = 13;
            label13.Text = "Numer rejestracyjny pojazdu";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(18, 83);
            label12.Name = "label12";
            label12.Size = new Size(188, 20);
            label12.TabIndex = 12;
            label12.Text = "Marka oraz model pojazdu";
            // 
            // btnUpdateVehicle
            // 
            btnUpdateVehicle.Location = new Point(9, 324);
            btnUpdateVehicle.Name = "btnUpdateVehicle";
            btnUpdateVehicle.Size = new Size(525, 29);
            btnUpdateVehicle.TabIndex = 11;
            btnUpdateVehicle.Text = "Zapisz zmiany";
            btnUpdateVehicle.UseVisualStyleBackColor = true;
            btnUpdateVehicle.Click += btnUpdateVehicle_Click;
            // 
            // btnClearVehicleForm
            // 
            btnClearVehicleForm.Location = new Point(283, 258);
            btnClearVehicleForm.Name = "btnClearVehicleForm";
            btnClearVehicleForm.Size = new Size(251, 29);
            btnClearVehicleForm.TabIndex = 10;
            btnClearVehicleForm.Text = "Wyczyść pola";
            btnClearVehicleForm.UseVisualStyleBackColor = true;
            // 
            // btnDeleteVehicle
            // 
            btnDeleteVehicle.Location = new Point(283, 289);
            btnDeleteVehicle.Name = "btnDeleteVehicle";
            btnDeleteVehicle.Size = new Size(251, 29);
            btnDeleteVehicle.TabIndex = 9;
            btnDeleteVehicle.Text = "Usuń zaznaczony";
            btnDeleteVehicle.UseVisualStyleBackColor = true;
            btnDeleteVehicle.Click += btnDeleteVehicle_Click;
            // 
            // btnAddVehicle
            // 
            btnAddVehicle.Location = new Point(9, 289);
            btnAddVehicle.Name = "btnAddVehicle";
            btnAddVehicle.Size = new Size(251, 29);
            btnAddVehicle.TabIndex = 8;
            btnAddVehicle.Text = "Dodaj pojazd";
            btnAddVehicle.UseVisualStyleBackColor = true;
            btnAddVehicle.Click += btnAddVehicle_Click;
            // 
            // btnLoadVehicles
            // 
            btnLoadVehicles.Location = new Point(9, 258);
            btnLoadVehicles.Name = "btnLoadVehicles";
            btnLoadVehicles.Size = new Size(251, 29);
            btnLoadVehicles.TabIndex = 7;
            btnLoadVehicles.Text = "Wczytaj pojazdy";
            btnLoadVehicles.UseVisualStyleBackColor = true;
            btnLoadVehicles.Click += btnLoadVehicles_Click_1;
            // 
            // btnSearchVehicle
            // 
            btnSearchVehicle.Location = new Point(295, 65);
            btnSearchVehicle.Name = "btnSearchVehicle";
            btnSearchVehicle.Size = new Size(239, 29);
            btnSearchVehicle.TabIndex = 6;
            btnSearchVehicle.Text = "Wyszukaj";
            btnSearchVehicle.UseVisualStyleBackColor = true;
            btnSearchVehicle.Click += btnSearchVehicle_Click;
            // 
            // txtSearchVehicle
            // 
            txtSearchVehicle.Location = new Point(295, 21);
            txtSearchVehicle.Name = "txtSearchVehicle";
            txtSearchVehicle.Size = new Size(239, 27);
            txtSearchVehicle.TabIndex = 5;
            // 
            // chkVehicleActive
            // 
            chkVehicleActive.AutoSize = true;
            chkVehicleActive.Location = new Point(18, 221);
            chkVehicleActive.Name = "chkVehicleActive";
            chkVehicleActive.Size = new Size(86, 24);
            chkVehicleActive.TabIndex = 4;
            chkVehicleActive.Text = "Aktywny";
            chkVehicleActive.UseVisualStyleBackColor = true;
            // 
            // txtVehicleModel
            // 
            txtVehicleModel.Location = new Point(18, 106);
            txtVehicleModel.Name = "txtVehicleModel";
            txtVehicleModel.Size = new Size(242, 27);
            txtVehicleModel.TabIndex = 3;
            // 
            // txtSeats
            // 
            txtSeats.Location = new Point(18, 176);
            txtSeats.Name = "txtSeats";
            txtSeats.Size = new Size(242, 27);
            txtSeats.TabIndex = 2;
            // 
            // txtPlateNumber
            // 
            txtPlateNumber.Location = new Point(18, 44);
            txtPlateNumber.Name = "txtPlateNumber";
            txtPlateNumber.Size = new Size(242, 27);
            txtPlateNumber.TabIndex = 1;
            // 
            // dgvVehicles
            // 
            dgvVehicles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVehicles.Location = new Point(557, 21);
            dgvVehicles.Name = "dgvVehicles";
            dgvVehicles.RowHeadersWidth = 51;
            dgvVehicles.Size = new Size(543, 332);
            dgvVehicles.TabIndex = 0;
            dgvVehicles.CellClick += dgvVehicles_CellClick;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnClearClientForm);
            tabPage1.Controls.Add(btnUpdateClient);
            tabPage1.Controls.Add(btnDeleteClient);
            tabPage1.Controls.Add(btnSearchClient);
            tabPage1.Controls.Add(txtSearchClient);
            tabPage1.Controls.Add(txtPostalCode);
            tabPage1.Controls.Add(txtCity);
            tabPage1.Controls.Add(txtAddress);
            tabPage1.Controls.Add(txtPhone);
            tabPage1.Controls.Add(txtLastName);
            tabPage1.Controls.Add(txtEmail);
            tabPage1.Controls.Add(txtFirstName);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(label9);
            tabPage1.Controls.Add(btnLoadClients);
            tabPage1.Controls.Add(btnAddClient);
            tabPage1.Controls.Add(dgvClients);
            tabPage1.Controls.Add(label8);
            tabPage1.Controls.Add(label7);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label5);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1124, 451);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Klienci";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnClearClientForm
            // 
            btnClearClientForm.Location = new Point(182, 398);
            btnClearClientForm.Name = "btnClearClientForm";
            btnClearClientForm.Size = new Size(125, 48);
            btnClearClientForm.TabIndex = 19;
            btnClearClientForm.Text = "Wyczyść pola";
            btnClearClientForm.UseVisualStyleBackColor = true;
            btnClearClientForm.Click += btnClearClientForm_Click;
            // 
            // btnUpdateClient
            // 
            btnUpdateClient.Location = new Point(25, 398);
            btnUpdateClient.Name = "btnUpdateClient";
            btnUpdateClient.Size = new Size(125, 48);
            btnUpdateClient.TabIndex = 18;
            btnUpdateClient.Text = "Zapisz zmiany";
            btnUpdateClient.UseVisualStyleBackColor = true;
            btnUpdateClient.Click += btnUpdateClient_Click;
            // 
            // btnDeleteClient
            // 
            btnDeleteClient.Location = new Point(876, 419);
            btnDeleteClient.Name = "btnDeleteClient";
            btnDeleteClient.Size = new Size(202, 27);
            btnDeleteClient.TabIndex = 17;
            btnDeleteClient.Text = "Usuń zaznaczonego klienta";
            btnDeleteClient.UseVisualStyleBackColor = true;
            btnDeleteClient.Click += btnDeleteClient_Click;
            // 
            // btnSearchClient
            // 
            btnSearchClient.Location = new Point(706, 419);
            btnSearchClient.Name = "btnSearchClient";
            btnSearchClient.Size = new Size(123, 27);
            btnSearchClient.TabIndex = 16;
            btnSearchClient.Text = "Wyszukaj klienta";
            btnSearchClient.UseVisualStyleBackColor = true;
            btnSearchClient.Click += btnSearchClient_Click;
            // 
            // txtSearchClient
            // 
            txtSearchClient.Location = new Point(367, 419);
            txtSearchClient.Name = "txtSearchClient";
            txtSearchClient.Size = new Size(292, 27);
            txtSearchClient.TabIndex = 15;
            // 
            // txtPostalCode
            // 
            txtPostalCode.Location = new Point(182, 292);
            txtPostalCode.Name = "txtPostalCode";
            txtPostalCode.Size = new Size(125, 27);
            txtPostalCode.TabIndex = 13;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(25, 292);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(125, 27);
            txtCity.TabIndex = 11;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(25, 206);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(282, 27);
            txtAddress.TabIndex = 9;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(182, 131);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(125, 27);
            txtPhone.TabIndex = 6;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(182, 39);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(125, 27);
            txtLastName.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(25, 131);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 2;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(25, 39);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(125, 27);
            txtFirstName.TabIndex = 0;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(182, 269);
            label10.Name = "label10";
            label10.Size = new Size(104, 20);
            label10.TabIndex = 14;
            label10.Text = "Kod pocztowy";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(25, 269);
            label11.Name = "label11";
            label11.Size = new Size(54, 20);
            label11.TabIndex = 12;
            label11.Text = "Miasto";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(25, 183);
            label9.Name = "label9";
            label9.Size = new Size(47, 20);
            label9.TabIndex = 10;
            label9.Text = "Adres";
            // 
            // btnLoadClients
            // 
            btnLoadClients.Location = new Point(367, 343);
            btnLoadClients.Name = "btnLoadClients";
            btnLoadClients.Size = new Size(711, 48);
            btnLoadClients.TabIndex = 2;
            btnLoadClients.Text = "Wczytaj klientów";
            btnLoadClients.UseVisualStyleBackColor = true;
            btnLoadClients.Click += btnLoadClients_Click;
            // 
            // btnAddClient
            // 
            btnAddClient.Location = new Point(25, 343);
            btnAddClient.Name = "btnAddClient";
            btnAddClient.Size = new Size(282, 48);
            btnAddClient.TabIndex = 8;
            btnAddClient.Text = "Dodaj klienta";
            btnAddClient.UseVisualStyleBackColor = true;
            btnAddClient.Click += btnAddClient_Click;
            // 
            // dgvClients
            // 
            dgvClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClients.Location = new Point(367, 16);
            dgvClients.Name = "dgvClients";
            dgvClients.RowHeadersWidth = 51;
            dgvClients.Size = new Size(711, 303);
            dgvClients.TabIndex = 1;
            dgvClients.CellClick += dgvClients_CellClick;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(182, 108);
            label8.Name = "label8";
            label8.Size = new Size(113, 20);
            label8.TabIndex = 7;
            label8.Text = "Numer telefonu";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(182, 16);
            label7.Name = "label7";
            label7.Size = new Size(72, 20);
            label7.TabIndex = 5;
            label7.Text = "Nazwisko";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(25, 108);
            label6.Name = "label6";
            label6.Size = new Size(88, 20);
            label6.TabIndex = 3;
            label6.Text = "Adres email";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 16);
            label5.Name = "label5";
            label5.Size = new Size(38, 20);
            label5.TabIndex = 1;
            label5.Text = "Imię";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabVehicles);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage9);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1132, 484);
            tabControl1.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1156, 495);
            Controls.Add(tabControl1);
            Name = "MainForm";
            Text = "Panel Admina - Zarządzanie transportem";
            tabPage9.ResumeLayout(false);
            tabPage9.PerformLayout();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReservations).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoutes).EndInit();
            tabVehicles.ResumeLayout(false);
            tabVehicles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).EndInit();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClients).EndInit();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage tabPage9;
        private TabPage tabPage5;
        private TabPage tabPage4;
        private TabPage tabPage3;
        private TabPage tabVehicles;
        private TabPage tabPage1;
        private Button btnClearClientForm;
        private Button btnUpdateClient;
        private Button btnDeleteClient;
        private Button btnSearchClient;
        private TextBox txtSearchClient;
        private TextBox txtPostalCode;
        private TextBox txtCity;
        private TextBox txtAddress;
        private TextBox txtPhone;
        private TextBox txtLastName;
        private TextBox txtEmail;
        private TextBox txtFirstName;
        private Label label10;
        private Label label11;
        private Label label9;
        private Button btnLoadClients;
        private Button btnAddClient;
        private DataGridView dgvClients;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private TabControl tabControl1;
        private CheckBox chkVehicleActive;
        private TextBox txtVehicleModel;
        private TextBox txtSeats;
        private TextBox txtPlateNumber;
        private DataGridView dgvVehicles;
        private Button btnUpdateVehicle;
        private Button btnClearVehicleForm;
        private Button btnDeleteVehicle;
        private Button btnAddVehicle;
        private Button btnLoadVehicles;
        private Button btnSearchVehicle;
        private TextBox txtSearchVehicle;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label15;
        private Button btnClearRouteForm;
        private Button btnDeleteRoute;
        private Button btnLoadRoutes;
        private Button btnAddRoute;
        private Button btnUpdateRoute;
        private Button btnSearchRoute;
        private TextBox txtSearchRoute;
        private TextBox txtPricePerson;
        private DateTimePicker dtpDepartureTime;
        private TextBox txtEndCity;
        private TextBox txtStartCity;
        private ComboBox cmbRouteVehicle;
        private DataGridView dgvRoutes;
        private Label label19;
        private Label label24;
        private DateTimePicker dtpResCreatedAt;
        private Label label23;
        private ComboBox cmbResStatus;
        private Label label22;
        private ComboBox cmbServiceType;
        private Label label21;
        private ComboBox cmbResRoute;
        private Label label20;
        private ComboBox cmbResClient;
        private DataGridView dgvReservations;
        private Button btnClearReservationForm;
        private Button btnDeleteReservation;
        private Button btnUpdateReservation;
        private Button btnAddReservation;
        private Button btnLoadReservations;
        private TextBox txtSearchReservation;
        private Button btnSearchReservation;
        private Button btnExportCsv;
        private DateTimePicker dtpReportMonth;
        private Label label25;
        private TextBox txtDbDetails;
        private Label lblDbStatus;
        private Button btnTestDb;
    }
}
