using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Core.DTOs
{
    public class CategoryDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
    }
}
