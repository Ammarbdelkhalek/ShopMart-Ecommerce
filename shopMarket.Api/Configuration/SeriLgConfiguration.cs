using Serilog;

namespace shopMarket.Api.Configuration
{
    public static  class SeriLgConfiguration
    {
        public static IServiceCollection AddSerilogConfiguration(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
            try
            {
                Log.Information("Start Logging");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "something wrong happen");
            }
            finally
            {
                 Log.CloseAndFlushAsync();

            }
            return services;
        }
    }
}
