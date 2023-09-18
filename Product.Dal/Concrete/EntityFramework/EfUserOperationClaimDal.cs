using Product.Core.Entity.Concrete;
using Product.Core.EntityFramework;
using Product.Dal.Abstract;
using Product.Dal.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Dal.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal:EfEntityRepositoryBase<UserOperationClaim,ProdDbContext>,IUserOperationClaimDal
    {
    }
}
