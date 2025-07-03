namespace ChatGPTIntegration.Models
{
    /// <summary>
    /// Represents an order placed by a customer for a specific product.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Unique identifier for the order (Primary Key).
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// ID of the customer who placed the order (Foreign Key).
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// ID of the product that was ordered (Foreign Key).
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Number of units ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Date and time when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
