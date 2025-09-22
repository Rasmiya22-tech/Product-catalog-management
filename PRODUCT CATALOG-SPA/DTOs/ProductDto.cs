using System.ComponentModel.DataAnnotations;

namespace PRODUCT_CATALOG_SPA.DTOs
{
    public class ProductDto
    {
        [Required, StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Range(0.01, 9999999.99)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must have up to 2 decimals.")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public IFormFile? Image { get; set; } // new for file upload

        [StringLength(100)]
        public string? Category { get; set; }

    }
}
