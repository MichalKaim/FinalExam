using CrimeApi.Models;
using MongoDB.Driver;
using System.Security.Authentication;

namespace CrimeApi.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<CrimeEvent> _collection;

        public EventRepository(IConfiguration configuration)
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            _client = new MongoClient(settings);
            _database = _client.GetDatabase(configuration["DatabaseName"]);
            _collection = _database.GetCollection<CrimeEvent>(configuration["CollectionName"]);
        }

        public async Task<IEnumerable<CrimeEvent>> GetAllAsync() => await _collection.Find(x => true).ToListAsync();
        public async Task<string> Create(CrimeEvent crimeEvent)
        {
            await _collection.InsertOneAsync(crimeEvent);
            return crimeEvent.Id;
        }

        public async Task AddLawEnforcement(string eventId, int lawId)
        {
            var newEvent = await _collection.FindAsync(x => x.Id == eventId);
            var updatedEvent = newEvent.First();
            updatedEvent.LawEnforcementId = lawId;
            await _collection.ReplaceOneAsync(x => x.Id == eventId, updatedEvent);
        }
    }
}
