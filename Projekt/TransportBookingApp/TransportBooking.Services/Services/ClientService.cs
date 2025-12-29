using Microsoft.EntityFrameworkCore;
using TransportBooking.Data.Context;
using TransportBooking.Domain.Entities;
using TransportBooking.Services.Interfaces;

namespace TransportBooking.Services.Services;

public class ClientService : IClientService
{
    public List<Client> GetAll()
    {
        using var db = DbContextFactory.Create();
        return db.Clients
            .AsNoTracking()
            .OrderByDescending(c => c.ClientId)
            .ToList();
    }

    public Client? GetById(int id)
    {
        using var db = DbContextFactory.Create();
        return db.Clients.AsNoTracking().FirstOrDefault(c => c.ClientId == id);
    }

    public Client Add(Client client)
    {
        Validate(client);

        using var db = DbContextFactory.Create();

        // unikamy duplikatu email
        var exists = db.Clients.Any(c => c.Email == client.Email);
        if (exists)
            throw new ArgumentException("Klient z takim adresem e-mail już istnieje.");

        client.CreatedAt = DateTime.UtcNow;

        db.Clients.Add(client);
        db.SaveChanges();

        return client;
    }

    public void Update(Client client)
    {
        if (client.ClientId <= 0)
            throw new ArgumentException("Nieprawidłowe ID klienta.");

        Validate(client);

        using var db = DbContextFactory.Create();

        var exists = db.Clients.Any(c => c.Email == client.Email && c.ClientId != client.ClientId);
        if (exists)
            throw new ArgumentException("Inny klient ma już ten sam e-mail.");

        db.Clients.Update(client);
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        using var db = DbContextFactory.Create();

        var entity = db.Clients.FirstOrDefault(c => c.ClientId == id);
        if (entity == null) return;

        db.Clients.Remove(entity);
        db.SaveChanges();
    }

    private static void Validate(Client client)
    {
        if (string.IsNullOrWhiteSpace(client.FirstName))
            throw new ArgumentException("Imię jest wymagane.");
        if (string.IsNullOrWhiteSpace(client.LastName))
            throw new ArgumentException("Nazwisko jest wymagane.");
        if (string.IsNullOrWhiteSpace(client.Email))
            throw new ArgumentException("E-mail jest wymagany.");

        // prosta walidacja maila (wystarczy do projektu)
        if (!client.Email.Contains('@') || !client.Email.Contains('.'))
            throw new ArgumentException("Nieprawidłowy format e-mail.");
    }
}
