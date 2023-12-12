using BaseRepo.Interfaces;
using News.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Interface
{
    public interface IAccount: IRepository<Account>
    {
        List<Account> GetAllPaging(string search, int offset, int limit);
    }
}
