using System.IO;
using Microsoft.Data.Sqlite;
using Dapper;

namespace StrokeControl.Framework;

public class Database
{
    private const string DbFileName = "images.db";

    public static void InitializeDatabase()
    {
        if (!File.Exists(DbFileName))
        {
            using var connection = new SqliteConnection($"Data Source={DbFileName}");
            connection.Open();

            connection.Execute(
                @"CREATE TABLE Images (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Url TEXT NOT NULL
                );

                CREATE TABLE Tags (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );

                CREATE TABLE ImageTags (
                    ImageId INTEGER NOT NULL,
                    TagId INTEGER NOT NULL,
                    FOREIGN KEY (ImageId) REFERENCES Images (Id),
                    FOREIGN KEY (TagId) REFERENCES Tags (Id),
                    UNIQUE (ImageId, TagId)
                );");

            connection.Close();
        }
    }
}