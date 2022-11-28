using Telegram.Bot;

namespace DevelopersGame.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotClient(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var client = new TelegramBotClient(configuration["Token"]);
            var webHook = $"{configuration["Url"]}/api/message/update";

            return serviceCollection
                .AddTransient<ITelegramBotClient>(x => client);
        }
    }
}