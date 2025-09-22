using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRODUCT_CATALOG_SPA.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Range(0.01, 9999999.99)]
        [Column(TypeName = "decimal(18,2)")]  // <-- specify precision & scale

        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must have up to 2 decimals.")]
        public decimal Price { get; set; }


        [StringLength(500)]
        public string? Description { get; set; }  // optional

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        // Store image as byte array
        public byte[]? ImageData { get; set; }

        // Optional: Store content type (like image/png)
        [StringLength(50)]
        public string? ImageType
        {
            get; set;

        }

        [StringLength(100)]
        public string? Category { get; set; }

    }
}
