using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using News.Base;
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
        public void Commit()
        {
            _dbContext.SaveChanges();
        }
        #endregion
    }
}
