using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Application.Interfaces;

namespace PhoneBook.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, HostBuilderContext context)
    {
        services.AddTransient<IContactRepository, ContactRepository>();

        var connectionString = context.Configuration.GetConnectionString("PhoneBook");

        services.AddDbContext<PhoneBookDbContext>(options =>
        options.UseSqlite(connectionString));

        return services;
    }
}
