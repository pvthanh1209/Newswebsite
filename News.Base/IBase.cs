using News.Base.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base
{
    public interface IBase
    {  
        IUsers users { get; }
        IAccount account { get; }
        void Commit();
    }
}
