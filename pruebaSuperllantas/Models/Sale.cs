namespace pruebaSuperllantas.Models
{
    public class Sale
    {
        public int saleId { get; set; }
        public int customerId { get; set; }
        public int branchId { get; set; }
        public string saleType { get; set; }
        public decimal temporaryDiscount { get; set; }
        public DateTime saleDate { get; set; }
        public Customer refCustomer { get; set; } // Relación ForeignKey
        public Branch refBranch { get; set; } // Relación ForeignKey
    }
}
