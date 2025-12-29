using TransportBooking.Domain.Entities;

namespace TransportBooking.Services.Interfaces;

public interface IClientService
{
    List<Client> GetAll();
    Client? GetById(int id);

    Client Add(Client client);
    void Update(Client client);
    void Delete(int id);
}
