using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerAddressRepository(DataContext context) : BaseRepositories<CustomerAddressEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<CustomerAddressEntity> GetOneAsync(Expression<Func<CustomerAddressEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.CustomerAddresses
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

    public override async Task<IEnumerable<CustomerAddressEntity>> GetAllAsync()
    {
        try
        {
            var existingEntity = await _context.CustomerAddresses.ToListAsync();

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
