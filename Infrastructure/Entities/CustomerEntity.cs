using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CustomerEntity
{
    [Key]
    public int CustomerId { get; set; }

    public int CompanyId { get; set; }


    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual RoleEntity Role { get; set; } = null!;
    public virtual CompanyEntity Company { get; set; } = null!;
    public virtual CustomerAddressEntity CustomerAddress { get; set; } = null!;
    public virtual CustomerContactEntity CustomerContact { get; set; } = null!;
}
