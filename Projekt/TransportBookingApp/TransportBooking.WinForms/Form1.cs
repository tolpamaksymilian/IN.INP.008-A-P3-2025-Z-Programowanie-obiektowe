using System;
using System.Windows.Forms;
using TransportBooking.Domain.Entities;
using TransportBooking.Services.Interfaces;
using TransportBooking.Services.Services;

namespace TransportBooking.WinForms;

public partial class Menu : Form
{
    private readonly IClientService _clients = new ClientService();

    public Menu()
    {
        InitializeComponent();
        dgvClients.AutoGenerateColumns = true;
        dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvClients.MultiSelect = false;
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        try
        {
            dgvClients.DataSource = _clients.GetAll();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            var c = new Client
            {
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = $"jan.kowalski{DateTime.Now.Ticks}@test.pl",
                Phone = "500100200",
                Address = "Testowa 1",
                PostalCode = "00-001",
                City = "Rzeszów"
            };

            _clients.Add(c);
            dgvClients.DataSource = _clients.GetAll();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (dgvClients.CurrentRow?.DataBoundItem is Client c)
            {
                _clients.Delete(c.ClientId);
                dgvClients.DataSource = _clients.GetAll();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnClients_Click(object sender, EventArgs e)
    {
        using var f = new FormClients();
        f.ShowDialog();
    }

}
