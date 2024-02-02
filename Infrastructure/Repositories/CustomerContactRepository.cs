using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerContactRepository(DataContext context) : BaseRepositories<CustomerContactEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<CustomerContactEntity> UpdateAsync(Expression<Func<CustomerContactEntity, bool>> expression, CustomerContactEntity entity)
    {
        try
        {
            var existingCustomer = await _context.CustomerContacts
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(expression);

            if (existingCustomer != null)
            {
                existingCustomer.Email = entity.Email;
                await _context.SaveChangesAsync();

                return existingCustomer;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return null!;
    }

    public override async Task<bool> DeleteAsync(Expression<Func<CustomerContactEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.CustomerContacts
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(expression);

            if (existingEntity != null)
            {
                _context.CustomerContacts.Remove(existingEntity);
                await _context.SaveChangesAsync();

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return false;
    }
}
