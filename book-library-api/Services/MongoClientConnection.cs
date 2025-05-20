using MongoDB.Driver;

namespace book_library_api.Services
{
    public class MongoClientConnection
    {
        private readonly string connectionUri = "mongodb+srv://kensalmeron:<db_password>@test-kendal.kcvaupk.mongodb.net/?retryWrites=true&w=majority&appName=test-kendal";

        public MongoClient GetMongoClient()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            return client;
        }
    }
}
