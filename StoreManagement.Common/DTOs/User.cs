using System;

namespace StoreManagement.Common.DTOs
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password_hash { get; set; }
        public string full_name { get; set; }
        public string role { get; set; }
        public bool is_approved { get; set; }
        public DateTime created_at { get; set; }
    }
}
