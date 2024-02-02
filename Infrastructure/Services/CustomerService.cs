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

    public async Task<CustomerEntity> GetOneCustomerAsync(string email)
    {
        try
        {
            var customerEntity = await _customerRepository.GetOneAsync(x => x.CustomerContact.Email == email);

            if (customerEntity != null)
            {
                return customerEntity;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return null!;
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
    {
        try
        {
            var customerEntities = await _customerRepository.GetAllAsync();

            if (customerEntities != null)
            {
                return customerEntities.ToList();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return null!;
    }

    public async Task<bool> UpdateCustomerAsync(string email, CustomerEntity updatedCustomer)
    {
        try
        {
            var existingCustomer = await _customerRepository.GetOneAsync(x => x.CustomerContact.Email == email);

            if (existingCustomer != null)
            {
                {
                    existingCustomer.FirstName = updatedCustomer.FirstName;
                    existingCustomer.LastName = updatedCustomer.LastName;
                    existingCustomer.CustomerAddress = updatedCustomer.CustomerAddress;
                    existingCustomer.CustomerContact = updatedCustomer.CustomerContact;
                    existingCustomer.Role = updatedCustomer.Role;
                    existingCustomer.Company = updatedCustomer.Company;
                };

                existingCustomer.CustomerContact.Email = email;

                var result = await _customerRepository.UpdateAsync(x => x.CustomerId == existingCustomer.CustomerId, existingCustomer);

                return result != null;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return false;
    }

    public async Task<bool> DeleteCustomerAsync(string email)
    {
        try
        {
            var existingCustomer = await _customerRepository.GetOneAsync(x => x.CustomerContact.Email == email);

            if (existingCustomer != null)
            {
                var result = await _customerRepository.DeleteAsync(x => x.CustomerId == existingCustomer.CustomerId);

                return result;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return false;
    }


}