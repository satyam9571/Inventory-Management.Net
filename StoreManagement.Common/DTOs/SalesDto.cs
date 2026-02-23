namespace StoreManagement.Core.DTOs
{
    public class SalesDto
    {
        public int SaleId { get; set; }

        public string BillNo { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal GrandTotal { get; set; }

        public string PaymentMode { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}