using BaseRepo.Interfaces;
using News.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Interface
{
    public interface INews: IRepository<News.Base.Models.News>
    {
        List<News.Base.Models.News> GetNewListAllPaging( string search, int cateId, int offset, int limit);
    }
}
