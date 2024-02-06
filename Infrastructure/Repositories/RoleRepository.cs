using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class RoleRepository(DataContext context) : BaseRepositories<RoleEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<RoleEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Roles
                .Include(i => i.Customer)
                .ToListAsync();

            if (existingEntities != null)
            {
                return existingEntities;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return null!;
    }

    public override async Task<RoleEntity> GetOneAsync(Expression<Func<RoleEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Roles
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
}
