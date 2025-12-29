using TransportBooking.Data.Context;
using TransportBooking.Domain.Entities;

namespace TransportBooking.Services
{
    public class ClientService
    {
        public List<Client> GetAll()
        {
            using var db = DbContextFactory.Create();
            return db.Clients.ToList();
        }

        public void Add(Client client)
        {
            using var db = DbContextFactory.Create();
            db.Clients.Add(client);
            db.SaveChanges();
        }

        public void Delete(int clientId)
        {
            using var db = DbContextFactory.Create();
            var client = db.Clients.FirstOrDefault(c => c.ClientId == clientId);
            if (client == null) return;

            db.Clients.Remove(client);
            db.SaveChanges();
        }
    }
}
