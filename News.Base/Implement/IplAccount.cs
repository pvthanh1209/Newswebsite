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
    public class IplAccount : Repository<Account>, IAccount
    {
        public IConfiguration _configuration { get; set; }
        internal string _cnnString = string.Empty;
        public NewsEdgeContext _dbContext;
        public IplAccount(NewsEdgeContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Account> GetAllPaging(string search, int offset, int limit)
        {
            var list = new List<Account>();
            var unitOfWork = new UnitOfWorkFactory(_cnnString);
            try
            {
                using (var u = unitOfWork.Create(false))
                {
                    var p = new DynamicParameters();
                    p.Add("@search", search);
                    p.Add("@offset", offset);
                    p.Add("@limit", limit);
                    list = u.GetIEnumerable<Account>("Sp_GetAccountListAll", p).ToList();
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
