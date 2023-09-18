using Product.Core.EntityFramework;
using Product.Dal.Abstract;
using Product.Dal.Concrete.Context;
using Product.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Dal.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Products, ProdDbContext>, IProductDal
    {
    }
}
