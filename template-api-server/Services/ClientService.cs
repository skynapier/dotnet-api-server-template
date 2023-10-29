public class ClientService : IClientService
{
    private readonly List<Client> clients = new List<Client>
{
    new Client
    {
        Id = Guid.NewGuid(),
        Name = "Client1",
        Email = "client1@example.com",
        CreatedAt = DateTime.UtcNow // Current UTC time
    },
    new Client
    {
        Id = Guid.NewGuid(),
        Name = "Client2",
        Email = "client2@example.com",
        CreatedAt = DateTime.Parse("2023-01-01T12:00:00Z") // Specific date using a string
    },
    new Client
    {
        Id = Guid.NewGuid(),
        Name = "Client3",
        Email = "client3@example.com",
        CreatedAt = DateTimeOffset.FromUnixTimeSeconds(1675219200).UtcDateTime // Using a Unix timestamp for 2023-02-01T12:00:00Z 
    }
};

    public IEnumerable<Client> GetAllClients()
    {
        return clients;
    }

    public void AddClient(Client client)
    {
        clients.Add(client);
    }
}