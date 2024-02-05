using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class RoleRepository(DataContext context) : BaseRepositories<RoleEntity>(context)
{
    private readonly DataContext _context = context;

    public async override Task<RoleEntity> CreateAsync(RoleEntity entity)
    {
        try
        {
            var existingRole = await _context.Roles.FirstOrDefaultAsync(i => i.RoleName == entity.RoleName);

            if (existingRole != null)
            {
                return existingRole;
            }

            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
            return null!;
        }
    }

    public override async Task<IEnumerable<RoleEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Roles
                .Include(i => i.Customer)
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
                .Include(i => i.Customer)
                .Include(i => i.Customer)
                .FirstOrDefaultAsync();

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
