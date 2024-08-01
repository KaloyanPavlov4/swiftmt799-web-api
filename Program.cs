
using Microsoft.Data.Sqlite;
using NLog;
using swiftmt799_web_api.Middleware;
using swiftmt799_web_api.Repositories;
using swiftmt799_web_api.Services;

namespace swiftmt799_web_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.Setup().LoadConfigurationFromFile("NLog.config");

            builder.Services.AddControllers();
            builder.Services.AddSingleton<ISwiftMT799Repository, SwiftMT799Repository>();
            builder.Services.AddSingleton<ISwiftMT799Service, SwiftMT799Service>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            initDB(builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));

            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }

        private static void initDB(string dataSource)
        {
            using (var connection = new SqliteConnection(dataSource))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"CREATE TABLE IF NOT EXISTS swift_mt799_messages (
                                        basicHeader VARCHAR(255),
                                        applicationHeader VARCHAR(100),
                                        userHeader VARCHAR(16),
                                        transactionReference VARCHAR(16) PRIMARY KEY,
                                        relatedReference VARCHAR(16),
                                        narrative VARCHAR(7000),
                                        trailer varchar(255)
                                        );";
                command.ExecuteNonQuery();
            }
        }
    }
}
