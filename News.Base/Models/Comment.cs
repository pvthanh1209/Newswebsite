using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace News.Base.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? NewId { get; set; }
        public int? UserId { get; set; }
        public string? ContentComment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public string? Username { get; set; }
        [NotMapped]
        public string? CateName { get; set; }
        [NotMapped]   
        
        public string? Title { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
    }
}
