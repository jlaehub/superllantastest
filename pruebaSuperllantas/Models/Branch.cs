namespace pruebaSuperllantas.Models
{
    public class Branch
    {
        public int branchId { get; set; }
        public int companyId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public Company refCompany { get; set; } // Relación ForeignKey
    }
}
