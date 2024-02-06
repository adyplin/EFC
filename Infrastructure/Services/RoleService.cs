using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Services;

public class RoleService(RoleRepository roleRepository)
{
    private readonly RoleRepository _roleRepository = roleRepository;

    public async Task<RoleEntity> CreateRoleAsync(string roleName)
    {
        try
        {
            var existingRole = await _roleRepository.GetOneAsync(x => x.RoleName == roleName);

            if (existingRole == null)
            {
                var newRole = new RoleEntity { RoleName = roleName };

                var result = await _roleRepository.CreateAsync(newRole);
                return result;
            }
            else
            {
                return existingRole;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
            return null!;
        }
    }

}
