using Infrastructure.Entities;
using Infrastructure.Services;
using System.Reflection.Emit;

namespace Presentation.ConsoleApp.Service;

public class MenuService(CustomerService customerService)
{
    private readonly CustomerService _customerService = customerService;


    public async Task ShowAddCustomer()
    {
        Console.Clear();
        DisplayMenuTitle("Add new customer");

        Console.Write("\nCompany name: ");
        var companyName = Console.ReadLine()!;

        Console.Write("\nRole: ");
        var roleName = Console.ReadLine()!;

        Console.Write("\nFirst name: ");
        var firstName = Console.ReadLine()!;

        Console.Write("\nLast name: ");
        var lastName = Console.ReadLine()!;

        Console.Write("\nEmail: ");
        var email = Console.ReadLine()!;

        Console.Write("\nPhone number: ");
        var phoneNumber = Console.ReadLine()!;

        Console.Write("\nStreet: ");
        var street = Console.ReadLine()!;

        Console.Write("\nZipcode: ");
        var zipCode = Console.ReadLine()!;

        Console.Write("\nCity: ");
        var city = Console.ReadLine()!;

        Console.Write("\nCountry: ");
        var country = Console.ReadLine()!;

        var customer = new CustomerEntity
        {
            FirstName = firstName,
            LastName = lastName,
            CustomerAddress = new CustomerAddressEntity
            {
                Street = street,
                City = city,
                ZipCode = zipCode,
                Country = country
            },
            CustomerContact = new CustomerContactEntity
            {
                Email = email,
                PhoneNumber = phoneNumber
            },
            Role = new RoleEntity
            {
                RoleName = roleName
            },
            Company = new CompanyEntity
            {
                CompanyName = companyName
            }
        };

        var result = await _customerService.CreateContactAsync(customer, firstName, lastName, street, city, zipCode, country, email, phoneNumber, companyName, roleName);

        if (result != null)
        {
            Console.WriteLine("Customer was created.");
        }
        else
        {
            Console.WriteLine("Something went wrong. Please try again.");
        }

        DisplayPressAnyKey();
    }
    private void DisplayPressAnyKey()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    private void DisplayMenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"{title}");
        Console.WriteLine();
    }

}




