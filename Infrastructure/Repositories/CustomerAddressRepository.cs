using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerAddressRepository(DataContext context) : BaseRepositories<CustomerAddressEntity>(context)
{
    private readonly DataContext _context = context;
}
