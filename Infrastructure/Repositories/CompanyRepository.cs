using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CompanyRepository(DataContext context) : BaseRepositories<CompanyEntity>(context)
{
    private readonly DataContext _context = context;

}
