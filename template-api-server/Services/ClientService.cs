

using System.Text.Json;

public class ClientService : IClientService
{
    private readonly ILogger<ClientService> _logger;
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

    public ClientService(ILogger<ClientService> logger)
    {
        _logger = logger;

    }

    public Client GetClientById(Guid id)
    {
        var client = clients.First(c => c.Id == id);
        if (client == null)
        {
            throw new ClientException(ClientErrorType.NotFound, $"Client with ID {id} not found.");
        }

        return client;
    }

    public Client? GetClientByName(string clientName)
    {
        return clients.FirstOrDefault(c => c.Name == clientName);
    }

    public IEnumerable<Client> GetClientsCreatedBefore(DateTime time)
    {
        return clients.Where(c => c.CreatedAt < time);
    }

    public IEnumerable<Client> GetAllClients()
    {
        _logger.LogInformation("Fetching all clients.");
        return clients;
    }

    public void AddClient(Client client)
    {
        clients.Add(client);
         _logger.LogInformation("Updated Clients {Clients}",  JsonSerializer.Serialize(clients));
    }

}