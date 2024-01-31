using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CompanyRepository(DataContext context) : BaseRepositories<CompanyEntity>(context)
{
    private readonly DataContext _context = context;


    public override async Task<CompanyEntity> GetOneAsync(Expression<Func<CompanyEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Companies
                .Include(i => i.Role).ThenInclude(i => i.RoleName)
                .Include(i => i.Customer).ThenInclude(i => i.FirstName)
                .Include(i => i.Customer).ThenInclude(i => i.LastName)

                .FirstOrDefaultAsync(expression);

            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }

    public override async Task<IEnumerable<CompanyEntity>> GetAllAsync()
    {
        try
        {
            var existingEntity = await _context.Companies.ToListAsync();

            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }
        return null!;
    }
}
