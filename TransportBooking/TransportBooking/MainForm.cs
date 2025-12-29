namespace TransportBooking;

using TransportBooking.Data.Context;



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


}
