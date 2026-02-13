using MongoDB.Driver;
using Aplikasi_Manajemen_Sampah.Models;
using System;
using System.Windows.Forms;

namespace Aplikasi_Manajemen_Sampah.Services
{
    public class MongoService
    {
        private IMongoDatabase _db;

        public MongoService()
        {
            try
            {
                string connectionString =
                    "mongodb+srv://lumbantoruansamuel07_db_user:samuel16@aplikasimanajemensampah.uljmyee.mongodb.net/?retryWrites=true&w=majority&appName=aplikasimanajemensampahdb";

                var client = new MongoClient(connectionString);
                _db = client.GetDatabase("ManajemenSampahDB");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi database gagal.\n\n" + ex.Message,
                                "MongoDB Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                _db = null; // supaya tidak crash
            }
        }

        // Akses Database (dengan pengecekan)
        public IMongoDatabase Database
        {
            get
            {
                if (_db == null)
                    throw new Exception("Database belum terhubung.");
                return _db;
            }
        }

        // Collection aman (tidak crash kalau DB null)
        public IMongoCollection<User>? Users =>
            _db?.GetCollection<User>("Users");

        public IMongoCollection<Sampah>? Sampah =>
            _db?.GetCollection<Sampah>("Sampah");

        public IMongoCollection<Penjemputan>? Penjemputan =>
            _db?.GetCollection<Penjemputan>("Penjemputan");
    }
}
