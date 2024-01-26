using Infrastructure.Repositories;
namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository, CustomerContactRepository customerContactRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly CustomerContactRepository _customerContactRepository = customerContactRepository;

    public bool CreateProduct(string Email)
    {
        if (!_customerContactRepository.Exists(x => x.Email == Email))
        {
            return true;
        }
        return false;
    }
}