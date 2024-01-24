using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class CustomerAddressEntity
{
    [Key, ForeignKey(nameof(CustomerEntity))]
    public int CustomerId { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Street { get; set; } = null!;


    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string City { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string ZipCode { get; set; } = null!;


    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Country { get; set; } = null!;

    public virtual CustomerEntity Customer { get; set; } = null!;

}
