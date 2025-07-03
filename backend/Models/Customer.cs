namespace ChatGPTIntegration.Models
{
    /// <summary>
    /// Represents a single customer in the system.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Unique identifier for the customer (Primary Key).
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// First name of the customer.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the customer.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the customer.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
