namespace template_api_server.Controllers;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetClients()
    {
        return Ok(_clientService.GetAllClients());
    }

    [HttpPost]
    public ActionResult<Client> AddClient([FromBody] Client client)
    {
        if (client == null || string.IsNullOrEmpty(client.Name))
        {
            return BadRequest("Client details are invalid.");
        }

        _clientService.AddClient(client);
        return Ok(client);
    }
}