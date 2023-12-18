using BaseRepo.Interfaces;
using BaseRepo.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore.Migrations;
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
    public class IplHistoryViewNews : Repository<HistoryViewNews>, IHistoryViewNews
    {
        public IConfiguration _configuration { get; set; }
        internal string _cnnString = string.Empty;
        public NewsEdgeContext _dbContext;
        public IplHistoryViewNews(NewsEdgeContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Models.News> GetHistoryNewsByUserId(int userId)
        {
            var list = new List<Models.News>();
            var unitOfWork = new UnitOfWorkFactory(_cnnString);
            try
            {
                using (var u = unitOfWork.Create(false))
                {
                    var p = new DynamicParameters();
                    p.Add("@userId", userId);
                    list = u.GetIEnumerable<Models.News>("Sp_GetHistoryViewNewsByUserId", p).ToList();
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
