using Npgsql;
using Polly;

namespace Teams.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating postresql database.");

                    var retry = Policy.Handle<NpgsqlException>()
                            .WaitAndRetry(
                                retryCount: 5,
                                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2,4,8,16,32 sc
                                onRetry: (exception, retryCount, context) =>
                                {
                                    logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                                });

                    //if the postgresql server container is not created on run docker compose this
                    //migration can't fail for network related exception. The retry options for database operations
                    //apply to transient exceptions                    
                    retry.Execute(() => ExecuteMigrations(configuration));

                    logger.LogInformation("Migrated postresql database.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postresql database");
                }
            }

            return host;
        }

        private static void ExecuteMigrations(IConfiguration configuration)
        {

            using NpgsqlConnection connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();

            using var command = new NpgsqlCommand
            {
                Connection = connection
            };
            command.CommandText = "DROP TABLE IF EXISTS Teams";
            command.ExecuteNonQuery();

            //Brand
            command.CommandText = @"CREATE TABLE Teams(TeamId SERIAL PRIMARY KEY, 
                                                                TeamName VARCHAR(60) NOT NULL,
                                                                TeamLogo VARCHAR(100) NOT NULL,          
                                                                )";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Teams(TeamName, TeamLogo) VALUES('Orlando Pirates', 'Pirates.jpg')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Teams(TeamName, TeamLogo) VALUES('Kaizer Cheifs','Cheifs.jpg)";
            command.ExecuteNonQuery();
        }
    }
}
