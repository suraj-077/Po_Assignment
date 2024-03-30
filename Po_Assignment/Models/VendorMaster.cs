using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Po_Assignment.Models
{
    public class VendorMaster
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        [Key]

        public int Id { get; set; }

        [Required]
        [StringLength(5)] 
        public string? Code { get; set; }

        [Required]
        [StringLength(150)]
        public string? Name { get; set; }

        [Required]
        [StringLength(255)] 
        public string? AddressLine1 { get; set; }

        [Required]
        [StringLength(255)] 
        public string? AddressLine2 { get; set; }

        [Required]
        [StringLength(150)]
        public string? ContactEmail { get; set; }

        [Required]
        [StringLength(10)] 
        public string? ContactNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string? ValidTillDate { get; set; }

        public bool IsActive { get; set; }
    }
}