using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepositories<TEntity> where TEntity : class
{
    private readonly DataContext _context;

    protected BaseRepositories(DataContext context)
    {
        _context = context;
    }

    public virtual async Task <TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }


    public virtual async Task <TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }


    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var existingEntities = await _context.Set<TEntity>().ToListAsync();
            if (existingEntities != null)
            {
                return existingEntities;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }



    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity newEntity)
    {
        try
        {
            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(newEntity);
                await _context.SaveChangesAsync();

                return existingEntity;

            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                _context.Set<TEntity>().Remove(existingEntity);
                await _context.SaveChangesAsync();

                return true;

            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }

    public virtual async Task<bool> ExistingAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var existing = await _context.Set<TEntity>().AnyAsync(expression);

            return existing;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }
}
