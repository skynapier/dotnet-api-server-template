public interface IClientService
{
    Client GetClientById(Guid id);
    Client? GetClientByName(string clientName);
    IEnumerable<Client> GetClientsCreatedBefore(DateTime time);

    IEnumerable<Client> GetAllClients();
    
    void AddClient(Client client);
}