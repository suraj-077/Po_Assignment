using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Po_Assignment.Models
{
    public class MaterialEntry
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
        public string? ShortText { get; set; }

        [Required]
        [StringLength(255)]
        public string? LongText { get; set; }


        [Required]
        public int? Unit { get; set; }

        [Required]
        [StringLength(10)]
        public int? Reorder_Level { get; set; }

        [Required]
        public int? Min_Order_Quantity { get; set; }

        public bool IsActive { get; set; }
    }


    public class MaterialFormData
	{

        public string Code { get; set; }

		[Required(ErrorMessage = "Short Text is required")]
		[StringLength(150, ErrorMessage = "Short Text must be at most 150 characters long")]
		public string ShortText { get; set; }

		[Required(ErrorMessage = "Long Text is required")]
		[StringLength(255, ErrorMessage = "Long Text must be at most 255 characters long")]
		public string LongText { get; set; }

		[Required(ErrorMessage = "Reorder Level is required")]
		[Range(0, int.MaxValue, ErrorMessage = "Reorder Level must be a positive integer")]
		public int Reorder_Level { get; set; }

		[Required(ErrorMessage = "Min Order Quantity is required")]
		[Range(1, int.MaxValue, ErrorMessage = "Min Order Quantity must be a positive integer")]
		public int Min_Order_Quantity { get; set; }

		[Required(ErrorMessage = "Unit is required")]
		[Range(1, int.MaxValue, ErrorMessage = "Unit must be a positive integer")]
		public int Unit { get; set; }

		public bool IsActive { get; set; }
	}


}
