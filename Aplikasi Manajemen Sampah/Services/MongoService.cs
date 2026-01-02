using MongoDB.Driver;
using Aplikasi_Manajemen_Sampah.Models;

namespace Aplikasi_Manajemen_Sampah.Services
{
    public class MongoService
    {
        private readonly IMongoDatabase _db;

        public MongoService()
        {
            var client = new MongoClient(
                "mongodb+srv://USERNAME:PASSWORD@clustersampah.vm4iwtj.mongodb.net/db_sampah"
            );

            _db = client.GetDatabase("db_sampah");
        }

        // Database (dipakai service / form lain)
        public IMongoDatabase Database => _db;

        // Collections (nama HARUS sama dengan Atlas)
        public IMongoCollection<User> Users =>
            _db.GetCollection<User>("users");

        public IMongoCollection<Sampah> Sampah =>
            _db.GetCollection<Sampah>("sampah");

        public IMongoCollection<Penjemputan> Penjemputan =>
            _db.GetCollection<Penjemputan>("penjemputan");
    }
}
