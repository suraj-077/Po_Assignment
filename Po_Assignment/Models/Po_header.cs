using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Po_Assignment.Models
{
    public class Po_header
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "OrderNumber is required.")]
        public int OrderNumber { get; set; }

        [Required(ErrorMessage = "Order Date is required.")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

      
        [StringLength(255, ErrorMessage = "Notes cannot exceed 255 characters.")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "OrderValue is required.")]
        public int OrderValue { get; set; }

        [Required(ErrorMessage = "Order Status is required.")]
        [StringLength(5, ErrorMessage = "Code must be 5 characters long.")]
        public string? OrderStatus { get; set; }

        // Define foreign key property
        [Column("VendorID")]
        public int VendorMasterId { get; set; }

        // Define navigation property to represent the relationship
        [ForeignKey("VendorMasterId")]
        [ScaffoldColumn(false)]
        public VendorMaster VendorMaster { get; set; }




    }
}
