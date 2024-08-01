using Dapper;
using Microsoft.Data.Sqlite;
using swiftmt799_web_api.Models;

namespace swiftmt799_web_api.Repositories
{
    public class SwiftMT799Repository : ISwiftMT799Repository
    {
        private string connectionString;
        private readonly string insertSQL = @"INSERT INTO swift_mt799_messages (basicHeader, applicationHeader, userHeader, transactionReference,relatedReference, narrative, trailer)
                                        VALUES (@BasicHeader, @ApplicationHeader, @UserHeader, @TransactionReference,@RelatedReference, @Narrative, @Trailer);";
        private readonly string selectSQL = @"SELECT * FROM swift_mt799_messages";

        public SwiftMT799Repository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<SwiftMT799Message>> FindAllAsync()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<SwiftMT799Message>(selectSQL);
            }
        }

        public async Task SaveAsync(SwiftMT799Message message)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                await connection.ExecuteAsync(insertSQL, message);
            }
        }
    }
}
