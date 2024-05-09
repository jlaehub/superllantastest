namespace pruebaSuperllantas.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int branchId { get; set; }
        public decimal withholdingTax { get; set; }
        public decimal salesTax { get; set; }
        public Branch refBranch { get; set; } // Relación ForeignKey
    }
}
