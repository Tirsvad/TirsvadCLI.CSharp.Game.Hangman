using Hangman;
using HangmanLibrary.Logic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = CreateDefaultBuilder(args).Build();
using var scope = host.Services.CreateScope();
IServiceProvider services = scope.ServiceProvider;
try
{
    services.GetRequiredService<App>().Run(args);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static IHostBuilder CreateDefaultBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<IMessages, Messages>();
            services.AddSingleton<IMenuText, MenuText>();
            services.AddSingleton<App>();
        });
}
