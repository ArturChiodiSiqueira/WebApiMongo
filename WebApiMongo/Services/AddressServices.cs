using MongoDB.Driver;
using System.Collections.Generic;
using WebApiMongo.Models;
using WebApiMongo.Utils;

namespace WebApiMongo.Services
{
    public class AddressServices
    {
        private readonly IMongoCollection<Address> _address;

        public AddressServices(IDatabaseSettings settings)
        {
            var address = new MongoClient(settings.ConnectionString);
            var database = address.GetDatabase(settings.DatabaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);
        }

        public Address Create(Address address)
        {
            _address.InsertOne(address);
            return address;
        }

        public List<Address> Get() => _address.Find<Address>(address => true).ToList(); //retorna EVERYBODY
        public Address Get(string id) => _address.Find<Address>(address => address.Id == id).FirstOrDefault();//retorna quem achar.
        public void Update(string id, Address addressIn)
        {
            _address.ReplaceOne(address => address.Id == id, addressIn);
            Get(addressIn.Id);
        }
        public void Remove(Address clientIn) => _address.DeleteOne(client => client.Id == clientIn.Id);
    }
}
