﻿using BaseRepo.Repositories;
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
    public class IplNews : Repository<News.Base.Models.News>, INews
    {
        public IConfiguration _configuration { get; set; }
        internal string _cnnString = string.Empty;
        public NewsEdgeContext _dbContext;
        public IplNews(NewsEdgeContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _cnnString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Models.News> GetNewListAllPaging(string search, int cateId, int offset, int limit)
        {
            var list = new List<Models.News>();
            var unitOfWork = new UnitOfWorkFactory(_cnnString);
            try
            {         
                using (var u = unitOfWork.Create(false))
                {
                    var p = new DynamicParameters();
                    p.Add("@search", search);
                    p.Add("@cateId", cateId);
                    p.Add("@offset", offset);
                    p.Add("@limit", limit);
                    list = u.GetIEnumerable<Models.News>("Sp_GetNewListAllPaging", p).ToList();
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