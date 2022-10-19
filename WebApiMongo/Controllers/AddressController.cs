using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiMongo.Models;
using WebApiMongo.Services;

namespace WebApiMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressServices _addressServices;
        public AddressController(AddressServices addressServices)
        {
            _addressServices = addressServices;
        }
        [HttpGet]
        public ActionResult<List<Address>> Get() => _addressServices.Get();
        [HttpGet("{id:length(24)}", Name = "GetAddress")]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressServices.Get(id);
            if (address == null)
                return NotFound();
            return Ok(address);
        }
        [HttpPost]
        public ActionResult<Address> Create(Address address)
        {
            _addressServices.Create(address);
            return CreatedAtRoute("GetAddress", new { id = address.Id.ToString() }, address); ;
        }
        [HttpPut]
        public ActionResult<Address> Update(string id, Address clientIn)
        {
            var client = _addressServices.Get(id); //apenas verificação se o cliente existe
            if (client == null)
                return NotFound();
            _addressServices.Update(id, clientIn); //manipulação do objeto criado!!
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Remove(string id)
        {
            var address = _addressServices.Get(id); //verificação se o objeto existe, se sim ele trás.
            if (address == null)
                return NotFound();
            _addressServices.Remove(address);
            return NoContent();
        }
    }
}
