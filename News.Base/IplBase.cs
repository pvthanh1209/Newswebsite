using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using News.Base;
using News.Base.Implement;
using News.Base.Interface;
using News.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Base
{
    public class IplBase : IBase
    {
        #region contructor
        private NewsEdgeContext _dbContext;
        public IConfiguration _configuration { get; }
        public IplBase(NewsEdgeContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        #endregion
        #region repository
        private IUsers _userRepo;
        public IUsers users
        {
            get
            {
                return _userRepo ?? (_userRepo = new IplUser(_dbContext, _configuration));
            }
        }

        private News.Base.Interface.IAccount _accountRepo;
        public News.Base.Interface.IAccount account
        {
            get
            {
                return _accountRepo ?? (_accountRepo = new IplAccount(_dbContext, _configuration));
            }
        }
        private ICategories _categoriesRepo;
        public ICategories categories
        {
            get
            {
                return _categoriesRepo ?? (_categoriesRepo = new IplCategories(_dbContext, _configuration));
            }
        }
        private INews _newsRepo;
        public INews news
        {
            get
            {
                return _newsRepo ?? (_newsRepo = new IplNews(_dbContext, _configuration));
            }
        }
        private ICategoriesDetail _categoriesDetailRepo;
        public ICategoriesDetail categoriesDetail
        {
            get
            {
                return _categoriesDetailRepo ?? (_categoriesDetailRepo = new IplCategoriesDetail(_dbContext, _configuration));
            }
        }
        private IComment _commentsRepo;
        public IComment comments
        {
            get
            {
                return _commentsRepo ?? (_commentsRepo = new IplComment(_dbContext, _configuration));
            }
        }
        private IHistoryViewNews _historyViewNewsRepo;
        public IHistoryViewNews HistoryViewNews
        {

            get
            {
                return _historyViewNewsRepo ?? (_historyViewNewsRepo = new IplHistoryViewNews(_dbContext, _configuration));
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        #endregion
    }
}
