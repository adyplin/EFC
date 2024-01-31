using Infrastructure.Entities;
using Infrastructure.Repositories;
namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository, CustomerContactRepository customerContactRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly CustomerContactRepository _customerContactRepository = customerContactRepository;

    public bool CreateCustomer(string Email, CustomerEntity customerEntity)
    {
        var _customerEntity = _customerContactRepository.GetOne(x => x.Email == Email);

        if (customerEntity != null)
        {
            var newCustomer = new CustomerEntity
            {
                FirstName = customerEntity.FirstName,
                LastName = customerEntity.LastName,
                CustomerAddress = customerEntity.CustomerAddress,
                CustomerContact = new CustomerContactEntity { Email = Email },
                Role = customerEntity.Role,
                Company = customerEntity.Company,
            };

            var result = _customerRepository.Create(newCustomer);
            return result != null;
        }
        return false;
    }
}