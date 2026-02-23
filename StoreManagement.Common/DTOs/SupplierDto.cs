namespace StoreManagement.Core.DTOs
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string GstNumber { get; set; }
        public string PanNumber { get; set; }
        public string PaymentTerms { get; set; }
        public int LeadTimeDays { get; set; }
        public decimal MinimumOrderValue { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal OutstandingBalance { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
