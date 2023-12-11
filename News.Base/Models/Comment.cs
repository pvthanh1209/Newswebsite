using System;
using System.Collections.Generic;

namespace News.Base.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? NewId { get; set; }
        public int? UserId { get; set; }
        public string? Comment1 { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
