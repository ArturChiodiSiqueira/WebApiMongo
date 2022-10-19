using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiMongo.Models;
using WebApiMongo.Services;

namespace WebApiMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientServices _clientService;
        private readonly AddressServices _addressService;

        public ClientController(ClientServices clientService, AddressServices addressService)
        {
            _clientService = clientService;
            _addressService = addressService;
        }

        [HttpGet]
        public ActionResult<List<Client>>Get() => _clientService.Get();
        [HttpGet("{id:length(24)}", Name = "GetClient")]
        public ActionResult<Client> Get(string id)
        {
            var client = _clientService.Get(id);

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public ActionResult<Client> Create(Client client)
        {
            Address address = _addressService.Create(client.Address);
            client.Address = address;

            _clientService.Create(client);
            return CreatedAtRoute("GetClient", new { id = client.Id.ToString() }, client);
        }

        [HttpPut]
        public ActionResult<Client> Update(Client clientIn, string id)
        {
            var client = _clientService.Get(id);

            if (client == null)
                return NotFound();

            _clientService.Update(id, clientIn);

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var client = _clientService.Get(id);

            if (client == null)
                return NotFound();

            _clientService.Remove(client);

            return NoContent();
        }
    }
}
