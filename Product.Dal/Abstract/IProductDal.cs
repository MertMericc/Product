using Product.Core.Repository;
using Product.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Dal.Abstract
{
    public interface IProductDal: IEntityRepository<Products>
    {
    }
}
