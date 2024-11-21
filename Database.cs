using System;
using System.Data.SQLite;

namespace WisejWebApplication2
{
    public static class Database
    {
        public static string connectionString = "Data Source=addressmanager.db;Version=3;";
        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createOrganizationsTable = @"
                    CREATE TABLE IF NOT EXISTS Organizations (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT,
                        Street TEXT,
                        Zip TEXT,
                        City TEXT,
                        Country TEXT
                    );";

                string createStaffTable = @"
                    CREATE TABLE IF NOT EXISTS Staff (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        OrganizationId INTEGER,
                        Title TEXT,
                        FirstName TEXT,
                        LastName TEXT,
                        Phone TEXT,
                        Email TEXT,
                        FOREIGN KEY(OrganizationId) REFERENCES Organizations(Id)
                    );";

                using (var command = new SQLiteCommand(createOrganizationsTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SQLiteCommand(createStaffTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Add methods for CRUD operations here
    }
}