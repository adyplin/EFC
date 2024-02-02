using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ConsoleApp.Service;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects-Education\EFC\Infrastructure\Data\local_database.mdf;Integrated Security=True;Connect Timeout=30"));
services.AddScoped<CustomerRepository>();
services.AddScoped<CustomerAddressRepository>();
services.AddScoped<CustomerContactRepository>();
services.AddScoped<RoleRepository>();
services.AddScoped<CompanyRepository>();
services.AddScoped<CustomerService>();

services.AddSingleton<MenuService>();

}).Build();

var menuService = builder.Services.GetRequiredService<MenuService>();
menuService.ShowMainMenu();
