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
            btnTestDb = new Button();
            dgvClients = new DataGridView();
            btnLoadClients = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            tabPage5 = new TabPage();
            tabPage6 = new TabPage();
            tabPage7 = new TabPage();
            tabPage8 = new TabPage();
            tabPage9 = new TabPage();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            db_connect_info = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvClients).BeginInit();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage9.SuspendLayout();
            SuspendLayout();
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
            // dgvClients
            // 
            dgvClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClients.Location = new Point(52, 35);
            dgvClients.Name = "dgvClients";
            dgvClients.RowHeadersWidth = 51;
            dgvClients.Size = new Size(563, 69);
            dgvClients.TabIndex = 1;
            // 
            // btnLoadClients
            // 
            btnLoadClients.Location = new Point(133, 120);
            btnLoadClients.Name = "btnLoadClients";
            btnLoadClients.Size = new Size(207, 29);
            btnLoadClients.TabIndex = 2;
            btnLoadClients.Text = "Wczytaj klientów";
            btnLoadClients.UseVisualStyleBackColor = true;
            btnLoadClients.Click += btnLoadClients_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage7);
            tabControl1.Controls.Add(tabPage8);
            tabControl1.Controls.Add(tabPage9);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(776, 343);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(768, 310);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dgvClients);
            tabPage2.Controls.Add(btnLoadClients);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(768, 198);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(768, 198);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 29);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(768, 198);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            tabPage5.Location = new Point(4, 29);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(768, 198);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "tabPage5";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            tabPage6.Location = new Point(4, 29);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(768, 198);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "tabPage6";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            tabPage7.Location = new Point(4, 29);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(768, 198);
            tabPage7.TabIndex = 6;
            tabPage7.Text = "tabPage7";
            tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            tabPage8.Location = new Point(4, 29);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new Padding(3);
            tabPage8.Size = new Size(768, 310);
            tabPage8.TabIndex = 7;
            tabPage8.Text = "tabPage8";
            tabPage8.UseVisualStyleBackColor = true;
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
            tabPage9.Size = new Size(768, 310);
            tabPage9.TabIndex = 8;
            tabPage9.Text = "Test połączenia z bazą";
            tabPage9.UseVisualStyleBackColor = true;
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 49);
            label2.Name = "label2";
            label2.Size = new Size(210, 20);
            label2.TabIndex = 2;
            label2.Text = "Nazwa uzytkownika: username";
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 108);
            label4.Name = "label4";
            label4.Size = new Size(74, 20);
            label4.TabIndex = 4;
            label4.Text = "Port: 5432";
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(792, 454);
            Controls.Add(tabControl1);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvClients).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage9.ResumeLayout(false);
            tabPage9.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnTestDb;
        private DataGridView dgvClients;
        private Button btnLoadClients;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private TabPage tabPage9;
        private Label db_connect_info;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
