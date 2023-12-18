using BaseRepo.Interfaces;
using News.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Interface
{
    public interface IComment: IRepository<Comment>
    {
        List<Comment> GetCommentByNewsId(int newsId);
        List<Comment> GetCommentListAllPaging(string search, string startDate, string endDate, int offset, int limit);
    }
}
