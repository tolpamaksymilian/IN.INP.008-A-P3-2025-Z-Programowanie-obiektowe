using System;
using System.Linq;
using System.Windows.Forms;
using TransportBooking.Domain.Entities;
using TransportBooking.Services.Interfaces;
using TransportBooking.Services.Services;

namespace TransportBooking.WinForms;

public partial class FormClients : Form
{
    private readonly IClientService _service = new ClientService();

    public FormClients()
    {
        InitializeComponent();

        dgvClients.AutoGenerateColumns = true;
        dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvClients.MultiSelect = false;
    }

    private void LoadData()
    {
        dgvClients.DataSource = _service.GetAll();
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        try { LoadData(); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "B³¹d"); }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var f = new ClientEditForm();
        if (f.ShowDialog() == DialogResult.OK)
        {
            try
            {
                _service.Add(f.Client);
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "B³¹d"); }
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (dgvClients.CurrentRow?.DataBoundItem is not Client selected)
            return;

        using var f = new ClientEditForm(selected);
        if (f.ShowDialog() == DialogResult.OK)
        {
            try
            {
                _service.Update(f.Client);
                LoadData();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "B³¹d"); }
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (dgvClients.CurrentRow?.DataBoundItem is not Client selected)
            return;

        var confirm = MessageBox.Show(
            $"Usun¹æ klienta: {selected.FirstName} {selected.LastName}?",
            "PotwierdŸ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        try
        {
            _service.Delete(selected.ClientId);
            LoadData();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "B³¹d"); }
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            var q = (txtSearch.Text ?? "").Trim().ToLower();
            var data = _service.GetAll();

            if (!string.IsNullOrWhiteSpace(q))
            {
                data = data.Where(c =>
                        (c.FirstName ?? "").ToLower().Contains(q) ||
                        (c.LastName ?? "").ToLower().Contains(q) ||
                        (c.Email ?? "").ToLower().Contains(q))
                    .ToList();
            }

            dgvClients.DataSource = data;
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "B³¹d"); }
    }
}
