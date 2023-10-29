public interface IClientService
{
    IEnumerable<Client> GetAllClients();
    void AddClient(Client client);
}