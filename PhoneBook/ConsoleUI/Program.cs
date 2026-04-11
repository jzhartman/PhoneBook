using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.ConsoleUI;
using PhoneBook.ConsoleUI.Services;
using PhoneBook.Infrastructure;

namespace PhoneBook.Presentation;

internal class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddPresentation();
                services.AddInfrastructure(context);
            })
            .Build();


        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PhoneBookDbContext>();
        db.Database.EnsureCreated();

        var mainMenu = host.Services.GetRequiredService<MainMenuService>();
        mainMenu.Run();
    }
}
