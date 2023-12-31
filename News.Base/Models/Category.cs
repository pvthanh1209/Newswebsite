﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace News.Base.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string? CateName { get; set; }
        public string? Title { get; set; }
        public string? Thumb { get; set; }
        public bool IsActive { get; set; }
        public string? ShortDescription { get; set; }
        public bool IsMenu { get; set; }
        public bool IsHome { get; set; }    
        public DateTime? CreatedDate { get; set; }
        [NotMapped]
        public int TotalRow { get; set; }
    }
}
