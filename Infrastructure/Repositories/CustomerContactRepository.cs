using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerContactRepository(DataContext context) : BaseRepositories<CustomerContactEntity>(context)
{
    private readonly DataContext _context = context;
}
