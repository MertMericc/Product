using Product.Core.Entity.Concrete;
using Product.Core.Result;
using Product.Core.Security;
using Product.Entity.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.BLL.Abstract
{
    public interface IAuthService
    {
        IDataResult<bool> Register(UserRegisterDto userRegisterDto);
        IDataResult<string> Login(UserLoginDto userLoginDto);
        IDataResult<User> UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);


    }
}
