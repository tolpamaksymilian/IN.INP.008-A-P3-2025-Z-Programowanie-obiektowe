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
            ((System.ComponentModel.ISupportInitialize)dgvClients).BeginInit();
            SuspendLayout();
            // 
            // btnTestDb
            // 
            btnTestDb.Location = new Point(525, 409);
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
            dgvClients.Location = new Point(12, 12);
            dgvClients.Name = "dgvClients";
            dgvClients.RowHeadersWidth = 51;
            dgvClients.Size = new Size(776, 231);
            dgvClients.TabIndex = 1;
            // 
            // btnLoadClients
            // 
            btnLoadClients.Location = new Point(12, 249);
            btnLoadClients.Name = "btnLoadClients";
            btnLoadClients.Size = new Size(207, 29);
            btnLoadClients.TabIndex = 2;
            btnLoadClients.Text = "Wczytaj klientów";
            btnLoadClients.UseVisualStyleBackColor = true;
            btnLoadClients.Click += this.btnLoadClients_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLoadClients);
            Controls.Add(dgvClients);
            Controls.Add(btnTestDb);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvClients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnTestDb;
        private DataGridView dgvClients;
        private Button btnLoadClients;
    }
}
