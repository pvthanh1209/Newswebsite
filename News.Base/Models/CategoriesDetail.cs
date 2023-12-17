using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Models
{
    public class CategoriesDetail
    {
        public int Id { get; set; }
        public int CateId { get; set; }
        public string Name { get; set; }
        public string? Thumb { get; set; }
        public bool IsHome { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Title { get; set; }
    }
}
