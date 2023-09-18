using Product.Core.Entity.Concrete;
using Product.Core.Result;
using Product.Entity.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.BLL.Abstract
{
    public interface IUserService
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByMail(string email);

        IDataResult<List<User>> GetList();
        IDataResult<User> GetById(int userId);

        IDataResult<bool> Add(UserRegisterDto userAddDto);
    }
}
