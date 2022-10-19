using MongoDB.Driver;
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

    }
}
