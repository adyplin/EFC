using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
                .Include(i => i.Company).ThenInclude(i => i.CompanyName)
                .Include(i => i.Role).ThenInclude(i => i.RoleName)
                .Include(i => i.CustomerAddress).ThenInclude(i => i.Street)
                .Include(i => i.CustomerAddress).ThenInclude(i => i.City)
                .Include(i => i.CustomerAddress).ThenInclude(i => i.ZipCode)
                .Include(i => i.CustomerAddress).ThenInclude(i => i.Country)
                .Include(i => i.CustomerContact).ThenInclude(i => i.Email)
                .Include(i => i.CustomerContact).ThenInclude(i => i.PhoneNumber)
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
            var existingEntity = await _context.Customers
                .Include(i => i.Company).ThenInclude(i => i.CompanyName)
                .Include(i => i.Role).ThenInclude(i => i.RoleName)
                .Include(i => i.CustomerContact).ThenInclude(i => i.Email)
                
                .ToListAsync();

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

    public override Task<CustomerEntity> UpdateAsync(Expression<Func<CustomerEntity, bool>> expression, CustomerEntity newEntity)
    {
        return base.UpdateAsync(expression, newEntity);
    }

    public override Task<bool> DeleteAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        return base.DeleteAsync(expression);
    }
}


