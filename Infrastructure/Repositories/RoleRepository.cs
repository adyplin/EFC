using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class RoleRepository(DataContext context) : BaseRepositories<RoleEntity>(context)
{
    private readonly DataContext _context = context;
}
