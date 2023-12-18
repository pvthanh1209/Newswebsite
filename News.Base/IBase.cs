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
        ICategories categories { get; }
        INews news { get; }
        ICategoriesDetail categoriesDetail { get; }
        IComment comments { get; }  
        IHistoryViewNews HistoryViewNews { get; }
        void Commit();
    }
}
