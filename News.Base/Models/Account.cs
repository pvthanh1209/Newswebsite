using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace News.Base.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool IsActive { get; set; }
        public string? FullName { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
    }
}
