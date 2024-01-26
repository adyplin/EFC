using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CompanyEntity
{
    [Key]
    public int CompanyId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string CompanyName { get; set; } = null!;

    public virtual ICollection<CustomerEntity> Customer { get; set; } = new HashSet<CustomerEntity>();

}
