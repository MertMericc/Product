using Product.Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Security
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User users, List<OperationClaim> operationClaims);
    }
}
