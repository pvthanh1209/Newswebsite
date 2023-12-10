using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Session
{
    public class AdminSession
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
    public class CustomerSession
    {
        public string SessionId { get; set; }
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}
