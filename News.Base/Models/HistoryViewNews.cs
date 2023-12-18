using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Models
{
    public class HistoryViewNews
    {
        public long Id { get; set; }    
        public int NewsId { get; set; } 
        public int UserId { get; set; } 
    }
}
