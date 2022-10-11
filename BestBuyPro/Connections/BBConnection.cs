using System;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace BestBuyPro.Connections
{
    public class BBConnection
    {
        private IConfigurationRoot _config;
        private string _connectionString;
        private IDbConnection _connection;

        public BBConnection()
        {
            _config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            _connectionString = _config.GetConnectionString("DefaultConnection");
            _connection = new MySqlConnection(_connectionString);
        }

        public IDbConnection GetConnection()
        {
            return _connection;
        }
    }
}

