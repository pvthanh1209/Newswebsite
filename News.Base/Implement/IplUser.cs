using BaseRepo.Repositories;
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
    public class IplUser :Repository<User>, IUsers
    {
        public IConfiguration _configuration { get; }
        internal string _cnnString = string.Empty;
        public NewsEdgeContext _dbContext;
        public IplUser(NewsEdgeContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
