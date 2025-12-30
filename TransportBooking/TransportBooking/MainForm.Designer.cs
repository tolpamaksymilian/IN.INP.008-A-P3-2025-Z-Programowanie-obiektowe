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
            db_connect_info = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnTestDb = new Button();
            tabPage5 = new TabPage();
            tabPage4 = new TabPage();
            tabPage3 = new TabPage();
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
            tabVehicles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVehicles).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClients).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabPage9
            // 
            tabPage9.Controls.Add(db_connect_info);
            tabPage9.Controls.Add(label4);
            tabPage9.Controls.Add(label3);
            tabPage9.Controls.Add(label2);
            tabPage9.Controls.Add(label1);
            tabPage9.Controls.Add(btnTestDb);
            tabPage9.Location = new Point(4, 29);
            tabPage9.Name = "tabPage9";
            tabPage9.Padding = new Padding(3);
            tabPage9.Size = new Size(1106, 510);
            tabPage9.TabIndex = 8;
            tabPage9.Text = "Test połączenia z bazą";
            tabPage9.UseVisualStyleBackColor = true;
            // 
            // db_connect_info
            // 
            db_connect_info.AutoSize = true;
            db_connect_info.Location = new Point(20, 182);
            db_connect_info.Name = "db_connect_info";
            db_connect_info.Size = new Size(128, 20);
            db_connect_info.TabIndex = 5;
            db_connect_info.Text = "Wynik połączenia:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 108);
            label4.Name = "label4";
            label4.Size = new Size(74, 20);
            label4.TabIndex = 4;
            label4.Text = "Port: 5432";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 79);
            label3.Name = "label3";
            label3.Size = new Size(126, 20);
            label3.TabIndex = 3;
            label3.Text = "Hasło: ************";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 49);
            label2.Name = "label2";
            label2.Size = new Size(210, 20);
            label2.TabIndex = 2;
            label2.Text = "Nazwa uzytkownika: username";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 19);
            label1.Name = "label1";
            label1.Size = new Size(231, 20);
            label1.TabIndex = 1;
            label1.Text = "Nazwa bazy danych: transport_db";
            // 
            // btnTestDb
            // 
            btnTestDb.Location = new Point(20, 141);
            btnTestDb.Name = "btnTestDb";
            btnTestDb.Size = new Size(263, 29);
            btnTestDb.TabIndex = 0;
            btnTestDb.Text = "TEST POŁĄCZENIA Z BAZĄ DANYCH";
            btnTestDb.UseVisualStyleBackColor = true;
            btnTestDb.Click += btnTestDb_Click;
            // 
            // tabPage5
            // 
            tabPage5.Location = new Point(4, 29);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(1106, 510);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Raport CSV";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 29);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1106, 510);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Rezerwacje";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1106, 510);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Trasy/Kursy";
            tabPage3.UseVisualStyleBackColor = true;
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
            tabVehicles.Size = new Size(1106, 510);
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
            tabPage1.Size = new Size(1106, 510);
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
            tabControl1.Size = new Size(1114, 543);
            tabControl1.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1156, 567);
            Controls.Add(tabControl1);
            Name = "MainForm";
            Text = "Panel Admina - Zarządzanie transportem";
            tabPage9.ResumeLayout(false);
            tabPage9.PerformLayout();
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
        private Label db_connect_info;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnTestDb;
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
    }
}
