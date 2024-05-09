namespace pruebaSuperllantas.Models
{
    public class Customer
    {
        public int customerId { get; set; }
        public string customerType { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public decimal specialDiscount { get; set; }
        public int salesAdvisorId { get; set; }
        public User refSalesAdvisor { get; set; } // Relación ForeignKey
    }
}
