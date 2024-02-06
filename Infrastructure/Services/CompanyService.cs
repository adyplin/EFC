using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CompanyService(CompanyRepository companyRepository)
{
    private readonly CompanyRepository _companyRepository = companyRepository;

    public async Task<CompanyEntity> CreateCompanyAsync(string companyName)
    {
        try
        {
            var existingCompany = await _companyRepository.GetOneAsync(x => x.CompanyName == companyName);

            if (existingCompany == null)
            {
                var newCompany = new CompanyEntity { CompanyName = companyName };

                var result = await _companyRepository.CreateAsync(newCompany);

                return result;
            }
            else
            {
                return existingCompany;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
            return null!;
        }
    }

}
