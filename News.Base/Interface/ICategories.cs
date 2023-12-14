using BaseRepo.Interfaces;
using News.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Base.Interface
{
    public interface ICategories: IRepository<Category>
    {
        List<Category> GetCategoryListAll(string search);
    }
}
