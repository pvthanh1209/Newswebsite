using System;
using System.Collections.Generic;

namespace News.Base.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public int? CateId { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? Thumb { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? AccountId { get; set; }
        public bool? IsHome { get; set; }
        public bool? IsHot { get; set; }
    }
}
