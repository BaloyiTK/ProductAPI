namespace ProductAPI.Models.Domain
{
    // Represents a product entity in the domain model.
    public class Product
    {
        // Unique identifier for the product.
        public Guid Id { get; set; }

        // Name of the product.
        public string Name { get; set; }

        // Description of the product.
        public string Description { get; set; } 

        // Price of the product. Using float, but consider using decimal for monetary values.
        public float Price { get; set; }

        // Image URL or path associated with the product.
        public string Image { get; set; } 
    }
}
