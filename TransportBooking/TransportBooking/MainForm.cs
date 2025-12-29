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

    private void btnTestDb_Click(object sender, EventArgs e)
    {
        try
        {
            using var db = new AppDbContext();
            var ok = db.Database.CanConnect();

            MessageBox.Show(ok ? "Połączenie z bazą OK ✅" : "Brak połączenia z bazą ❌");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    private void btnLoadClients_Click(object sender, EventArgs e)
    {
        try
        {
            using var db = new AppDbContext();

            var clients = db.Clients
                .AsNoTracking()
                .OrderByDescending(c => c.ClientId)
                .ToList();

            dgvClients.DataSource = clients;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }



}
