using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Po_Assignment.Models
{
    public class Po_Details
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        [Key]
        
        public int ID { get; set; }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "Item Quantity is required.")]
        public int ItemQuantity { get; set; }

     
        [Required(ErrorMessage = "Item Rate is required.")]
        public float ItemRate { get; set; }

        [Required(ErrorMessage = "Item Value is required.")]
        public float ItemValue { get; set; }

        [Required(ErrorMessage = "Item Notes is required.")]
        [StringLength(255, ErrorMessage = "Item Notes cannot exceed 255 characters.")]
        public string ItemNotes { get; set; }

        [Required(ErrorMessage = "Expected Date is required.")]
        [Display(Name = "Expected Date")]
        public DateTime ExpectedDate { get; set; }

       

        [Column("MaterialID")]
        public int MaterialEntryId { get; set; }

        // Define navigation property to represent the relationship
        [ForeignKey("MaterialEntryId")]
        public MaterialEntry MaterialEntry { get; set; }


    }
}
