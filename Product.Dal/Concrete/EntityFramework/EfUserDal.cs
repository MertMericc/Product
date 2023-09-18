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
    public class EfUserDal : EfEntityRepositoryBase<User, ProdDbContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context =  new ProdDbContext())
            {
                var result = from claims in context.OperationClaims
                             join userclaim in context.UserOperationClaims
                             on claims.Id equals userclaim.OperationClaimId
                             where userclaim.UserId == user.Id
                             select new OperationClaim { Id = claims.Id, Name = claims.Name };
                return result.ToList();
            }
        }
    }
}
