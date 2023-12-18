using BaseRepo.Repositories;
using Dapper;
using Microsoft.Extensions.Configuration;
using News.Base.Interface;
using News.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Implement
{
    public class IplComment : Repository<Comment>, IComment
    {
        public IConfiguration _configuration { get; set; }
        internal string _cnnString = string.Empty;
        public NewsEdgeContext _dbContext;
        public IplComment(NewsEdgeContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Comment> GetCommentByNewsId(int newsId)
        {
            var list = new List<Comment>();
            var unitOfWork = new UnitOfWorkFactory(_cnnString);
            try
            {
                using (var u = unitOfWork.Create(false))
                {
                    var p = new DynamicParameters();
                    p.Add("@newsId", newsId);
                    list = u.GetIEnumerable<Comment>("Sp_GetComment_ByNewsId", p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list;
        }

        public List<Comment> GetCommentListAllPaging(string search, string startDate, string endDate, int offset, int limit)
        {
            var list = new List<Comment>();
            var unitOfWork = new UnitOfWorkFactory(_cnnString);
            try
            {
                using (var u = unitOfWork.Create(false))
                {
                    var p = new DynamicParameters();
                    p.Add("@search", search);
                    p.Add("@startDate", startDate);
                    p.Add("@endDate", endDate);
                    p.Add("@offset", offset);
                    p.Add("@limit", limit);
                    list = u.GetIEnumerable<Comment>("Sp_GetCommentListAllPaging", p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list;
        }
    }
}
