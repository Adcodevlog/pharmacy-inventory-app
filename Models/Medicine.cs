namespace MedicineApp.Models
{
    public class Medicine
    {
        public string FullName { get; set; }
        public string Notes { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
    }
}