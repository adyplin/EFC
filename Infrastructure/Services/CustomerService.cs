using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository, CustomerContactRepository customerContactRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly CustomerContactRepository _customerContactRepository = customerContactRepository;

    public async Task<bool> CreateContactAsync(CustomerEntity customer, string firstName, string lastName, string street, string city, string zipCode, string country, string email, string phoneNumber, string companyName, string roleName)
    {
        try
        {
            var existingCustomer = await _customerContactRepository.GetOneAsync(x => x.Email == email);
             
            if (existingCustomer == null && customer != null)
            {
                var newCustomer = new CustomerEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    CustomerAddress = new CustomerAddressEntity { Street = street, City = city, ZipCode = zipCode, Country = country },
                    CustomerContact = new CustomerContactEntity { Email = email, PhoneNumber = phoneNumber, },
                    Role = new RoleEntity { RoleName = roleName },
                    Company = new CompanyEntity { CompanyName = companyName }

                };

                var result = await _customerRepository.CreateAsync(newCustomer);

                return result != null;
            }
        } 
        catch (Exception ex) 
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return false;
    }


}