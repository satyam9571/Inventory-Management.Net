using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Common.DTOs
{
    public class StockEntryDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal SellingUnit { get; set; }

    }
}
