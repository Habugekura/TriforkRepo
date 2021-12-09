using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsumerApp
{
    public static class DatabaseHelper
    {
        public static void CreateDatabase()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./myDb.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {

                connection.Open();

                var tableCommander = connection.CreateCommand();
                tableCommander.CommandText = "CREATE TABLE approved_messages(MessageText VARCHAR(50), TimeStamp VARCHAR(255));";

            };
        }

        public static void InsertIntoDatabase(string messageText, DateTime msgTimeStamp)
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./myDb.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = $"INSERT INTO approved_messages VALUES({messageText}, {msgTimeStamp})";
                    insertCmd.ExecuteNonQuery();
                    transaction.Commit();
                }

            };

        }

        public static void ReadFromDatabase()
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./myDb.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var selectCmd = connection.CreateCommand();
                    selectCmd.CommandText = "SELECT * FROM approved_messages";

                    using (var reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var result = reader.GetString(0);
                            result += reader.GetString(1);
                            Console.WriteLine(result);
                        }
                    }
                }

            };

        }
    }
}
