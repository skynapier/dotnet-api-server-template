namespace template_api_server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;


[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("{id:guid}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Client retrieved successfully", typeof(Client))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Client with the specified ID not found")]
    [SwaggerOperation(Summary = "Retrieve a client by ID", Description = "Fetches details of a client by their ID.")]
    public ActionResult<Client> GetClientById(Guid id)
    {
        try
        {
            var client = _clientService.GetClientById(id);
            return Ok(client);
        }
        catch (ClientException ex)
        {
            if (ex.ErrorType == ClientErrorType.NotFound)
            {
                return NotFound(ex.Message);
            }
            // Handle other types of ClientException if needed
            throw;
        }

    }

    [HttpGet("{clientName}")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Client retrieved successfully", typeof(Client))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Client with the specified name not found")]
    [SwaggerOperation(Summary = "Retrieve a client by name", Description = "Fetches details of a client by their name.")]
    public ActionResult<Client> GetClientByName(string clientName)
    {
        var client = _clientService.GetClientByName(clientName);
        if (client == null)
        {
            return NotFound($"Client with name {clientName} not found.");
        }
        return Ok(client);
    }

    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK, "Clients retrieved successfully", typeof(IEnumerable<Client>))]
    [SwaggerOperation(Summary = "Retrieve all clients", Description = "Fetches a list of all clients.")]
    public ActionResult<IEnumerable<Client>> GetClients()
    {
        return Ok(_clientService.GetAllClients());
    }

    [HttpGet("created-before/{time}")]
    [SwaggerOperation(Summary = "Get clients created before a specific time")]
    public ActionResult<IEnumerable<Client>> GetClientsCreatedBefore(
        [FromRoute]
        [SwaggerParameter(Description = "Time in the format 'yyyy-MM-ddTHH:mm:ss' (e.g., '2023-10-30T12:00:00')")]
        DateTime time)
    {
        return Ok(_clientService.GetClientsCreatedBefore(time));
    }


    [HttpPost]
    [SwaggerOperation(Summary = "Add a new client to the system")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Client added successfully", typeof(Client))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid client details provided")]

    public ActionResult<Client> AddClient(
        [FromBody]
        [SwaggerParameter(Description = "Client details to be added")]
        Client client)
    {
        if (client == null || string.IsNullOrEmpty(client.Name))
        {
            return BadRequest("Client details are invalid.");
        }

        _clientService.AddClient(client);
        return Ok(client);
    }
}