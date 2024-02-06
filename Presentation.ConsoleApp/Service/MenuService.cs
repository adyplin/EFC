using Infrastructure.Entities;
using Infrastructure.Services;
using System.Diagnostics.Eventing.Reader;

namespace Presentation.ConsoleApp.Service;

public class MenuService(CustomerService customerService)
{
    private readonly CustomerService _customerService = customerService;


    public void ShowMainMenu()
    {
        while (true)
        {
            DisplayMenuTitle("Menu Option");
            Console.WriteLine($"{"1.",-3} Add a new customer");
            Console.WriteLine($"{"2.",-3} View the customer list");
            Console.WriteLine($"{"3.",-3} View detailed information about a customer");
            Console.WriteLine($"{"4.",-3} Update a customer");
            Console.WriteLine($"{"5.",-3} Remove a customer");
            Console.WriteLine($"{"0.",-3} Exit the application");
            Console.WriteLine();
            Console.Write("Enter menu option: ");

            int option;


            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        Console.Clear();
                        ShowAddCustomer();
                        break;

                    case 2:
                        Console.Clear();
                        ShowCustomerList();
                        break;

                    case 3:
                        Console.Clear();
                        ShowCustomerDetails();
                        break;

                    case 4:
                        Console.Clear();
                        ShowUpdateCustomer();
                        break;

                    case 5:
                        Console.Clear();
                        ShowDeleteCustomer();
                        break;

                    case 0:
                        ShowExitApplication();
                        break;

                    default:
                        Console.WriteLine("\nInvalid Option. Press any key try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

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

    public async Task ShowCustomerList()
    {
        Console.Clear();
        DisplayMenuTitle("Customer List");
        Console.WriteLine();
        

        var existingCustomerList = _customerService.GetAllCustomersAsync();
        var customers = await existingCustomerList;
        Console.Clear();

        if (customers != null)
        {
            foreach (var customer in customers)
            {
                
                Console.WriteLine($"{customer.Company.CompanyName}");
                Console.WriteLine($"{customer.Role.RoleName}");
                Console.WriteLine($"{customer.FirstName} {customer.LastName}");
                Console.WriteLine($"{customer.CustomerContact.Email}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No customers found.");
        }
        DisplayPressAnyKey();
    }

    public async Task ShowCustomerDetails()
    {
        Console.Clear();
        DisplayMenuTitle("Customer Details");
        Console.WriteLine();

        Console.Write("Enter customer email: ");
        var email = Console.ReadLine();

        if (email != null)
        {
            var customer = await _customerService.GetOneCustomerAsync(email);
            Console.Clear();

            if (customer != null)
            {
                Console.Clear();
                Console.WriteLine($"{"0.",-2} {customer.Company.CompanyName}");
                Console.WriteLine($"{"0.",-2}{customer.Role.RoleName}");
                Console.WriteLine($"{"0.",-2}{customer.FirstName} {customer.LastName}");
                Console.WriteLine($"{"0.",-2}{customer.CustomerContact.Email}");
                Console.WriteLine($"{"0.",-2}{customer.CustomerContact.PhoneNumber}");
                Console.WriteLine($"{"0.",-2}{customer.CustomerAddress.Street}");
                Console.WriteLine($"{"0.",-2}{customer.CustomerAddress.ZipCode} {customer.CustomerAddress.City}");
                Console.WriteLine($"{"0.",-2}{customer.CustomerAddress.Country}");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }
        else
        {
            Console.WriteLine("Email cannot be null.");
        }
    }

    public async Task ShowUpdateCustomer()
    {
        Console.Clear();
        DisplayMenuTitle("Update Customer");
        Console.WriteLine();

        Console.Write("Enter customer email to update: ");
        var email = Console.ReadLine();

        if (email != null)
        {
            var existingCustomer = _customerService.GetOneCustomerAsync(email).Result;
            Console.Clear();

            if (existingCustomer != null)
            {
                Console.Clear();
                Console.Write("Enter new company name: ");
                existingCustomer.Company.CompanyName = Console.ReadLine()!;

                Console.Write("Enter new role name: ");
                existingCustomer.Role.RoleName = Console.ReadLine()!;

                Console.Write("Enter new First name: ");
                existingCustomer.FirstName = Console.ReadLine()!;

                Console.Write("Enter new last name: ");
                existingCustomer.LastName = Console.ReadLine()!;

                Console.Write("Enter new email: ");
                existingCustomer.CustomerContact.Email = Console.ReadLine()!;

                Console.Write("Enter new phone number: ");
                existingCustomer.CustomerContact.PhoneNumber = Console.ReadLine()!;

                Console.Write("Enter new street name: ");
                existingCustomer.CustomerAddress.Street = Console.ReadLine()!;

                Console.Write("Enter new postal code: ");
                existingCustomer.CustomerAddress.ZipCode = Console.ReadLine()!;

                Console.Write("Enter new city: ");
                existingCustomer.CustomerAddress.City = Console.ReadLine()!;

                Console.Write("Enter new country: ");
                existingCustomer.CustomerAddress.Country = Console.ReadLine()!;

                var updated = await _customerService.UpdateCustomerAsync(email, existingCustomer);

                if (updated)
                {
                    Console.WriteLine("Customer was updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to updated customer.");
                }
            }
            else
            {
                Console.WriteLine("Customer not found. Please try again.");
            }
        }
        else
        {
            Console.WriteLine("Email cannot be null.");
        }

        DisplayPressAnyKey();
    }

    public void ShowDeleteCustomer()
    {
        Console.Clear();
        DisplayMenuTitle("Delete Customer");
        Console.WriteLine();

        Console.Write("Enter an customer email to delete: ");
        var email = Console.ReadLine()!;

        var existingCustomer = _customerService.GetOneCustomerAsync(email);
        existingCustomer.Wait();

        var customerToDelete = existingCustomer.Result;
        Console.Clear();

        if (customerToDelete != null)
        {
            Console.Clear();
            Console.WriteLine($"{customerToDelete.Company.CompanyName}");
            Console.WriteLine($"{customerToDelete.Role.RoleName}");
            Console.WriteLine($"{customerToDelete.FirstName} {customerToDelete.LastName}");
            Console.WriteLine($"{customerToDelete.CustomerContact.Email}");

            Console.Write($"Are you sure you want to delete this customer? (y/n)");
            var confirmation = Console.ReadLine()!;

            if (confirmation.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                var deleted = _customerService.DeleteCustomerAsync(email);
                deleted.Wait();

                var deleteResult = deleted.Result;

                if (deleteResult)
                {
                    Console.Clear();
                    Console.WriteLine("Customer deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete customer.");
                }
            }
            else
            {
                Console.WriteLine("Deletion cancelled. Customer not deleted.");
            }
        }
        else
        {
            Console.WriteLine("Customer not found.");
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
    private void ShowExitApplication()
    {
        Console.Clear();
        Console.Write("Are you sure you want to close this application? (y/n): ");
        var option = Console.ReadLine() ?? "";

        if (option.Equals("y", StringComparison.OrdinalIgnoreCase))
            Environment.Exit(0);
    }

}




