using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerRepository(DataContext context) : BaseRepositories<CustomerEntity>(context)
{
    private readonly DataContext _context = context;


    public override async Task<CustomerEntity> GetOneAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Customers
                .Include(i => i.Company)
                .Include(i => i.Role)
                .Include(i => i.CustomerAddress)
                .Include(i => i.CustomerContact)
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

    public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Customers
                .Include(i => i.Company)
                .Include(i => i.Role)
                .Include(i => i.CustomerContact)
                
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

    public override async Task<CustomerEntity> UpdateAsync(Expression<Func<CustomerEntity, bool>> expression, CustomerEntity newEntity)
    {
        try
        {
            var existingEntity = await _context.Customers
           .Include(i => i.CustomerAddress)
           .Include(i => i.CustomerContact)
           .Include(i => i.Company)
           .Include(i => i.Role)
           .FirstOrDefaultAsync();

            if (existingEntity != null)
            {
                existingEntity.CustomerContact.Email = newEntity.CustomerContact.Email;

                await _context.SaveChangesAsync();

                return existingEntity;
            }
        }
        catch (Exception ex)
        {
            Debug.Write("ERROR :: " + ex.Message);
        }

        return null!;
    }

    public override async Task<bool> DeleteAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Customers
                .Include(i => i.CustomerAddress)
                .Include(i => i.CustomerContact)
                .Include(i => i.Company)
                .Include(i => i.Role)
                .FirstOrDefaultAsync(expression);

            if (existingEntity != null)
            {
                _context.Customers.Remove(existingEntity);
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


