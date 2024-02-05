﻿using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CompanyRepository(DataContext context) : BaseRepositories<CompanyEntity>(context)
{
    private readonly DataContext _context = context;

    public async override Task<CompanyEntity> CreateAsync(CompanyEntity entity)
    {
        try
        {
            var existingCompany = await _context.Companies.FirstOrDefaultAsync(i => i.CompanyName == entity.CompanyName);

            if (existingCompany != null)
            {
                return existingCompany;
            }

            _context.Companies.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
            return null!;
        }
    }


    public override async Task<CompanyEntity> GetOneAsync(Expression<Func<CompanyEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Companies
               
                .Include(i => i.Customer)
                .Include(i => i.Customer)

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
            var existingEntity = await _context.Companies
                .Include(i => i.Customer)
                .Include(i => i.Customer)

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

}
