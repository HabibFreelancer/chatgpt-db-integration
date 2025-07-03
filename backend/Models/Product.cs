namespace ChatGPTIntegration.Models
{
    /// <summary>
    /// Represents a product that can be ordered by customers.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for the product (Primary Key).
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }
    }
}
