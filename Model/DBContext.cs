using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace NotebookAppApi.Model
{
    // DB context
    public class DBContext
    {
        private readonly IMongoDatabase _database = null;

        // Defaut constructor
        public DBContext(IOptions<Settings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);

            if(mongoClient != null)
                _database = mongoClient.GetDatabase(settings.Value.Database);
        }

        // 
        public IMongoCollection<Note> Notes{
            get {
                return _database.GetCollection<Note>("Notes");
            }
        } 

    }
}