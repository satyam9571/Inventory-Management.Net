using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Common.DTOs
{
    public class StockEntryDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string EntryType { get; set; } // e.g., "In" or "Out" – adjust if it's an enum
        public DateTime EntryDate { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
