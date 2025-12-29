namespace TransportBooking.WinForms
{
    partial class Menu
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
            dgvClients = new DataGridView();
            btnLoad = new Button();
            btnAdd = new Button();
            btnDelete = new Button();
            btnClients = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClients).BeginInit();
            SuspendLayout();
            // 
            // dgvClients
            // 
            dgvClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClients.Location = new Point(12, 12);
            dgvClients.Name = "dgvClients";
            dgvClients.RowHeadersWidth = 51;
            dgvClients.Size = new Size(776, 318);
            dgvClients.TabIndex = 0;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(12, 386);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(206, 29);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "Wczytaj";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 351);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(206, 29);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Dodaj testowego";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(224, 386);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(206, 29);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Usuń zaznaczonego";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClients
            // 
            btnClients.Location = new Point(639, 378);
            btnClients.Name = "btnClients";
            btnClients.Size = new Size(94, 29);
            btnClients.TabIndex = 4;
            btnClients.Text = "btnClients";
            btnClients.UseVisualStyleBackColor = true;
            btnClients.Click += btnClients_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClients);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(btnLoad);
            Controls.Add(dgvClients);
            Name = "Menu";
            Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)dgvClients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvClients;
        private Button btnLoad;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnClients;
    }
}
