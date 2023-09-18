using Product.BLL.Abstract;
using Product.BLL.Constants;
using Product.Core.Entity.Concrete;
using Product.Core.Result;
using Product.Core.Security;
using Product.Dal.Abstract;
using Product.Entity.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.BLL.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private ITokenHelper _tokenHelper;
        private readonly IUserDal _userDal;
        private readonly IUserOperationClaimDal _userOperationClaimDal;


        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserDal userDal, IUserOperationClaimDal userOperationClaimDal)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userDal = userDal;
            _userOperationClaimDal = userOperationClaimDal;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            try
            {
                var claims = _userService.GetClaims(user);
                var accessToken = _tokenHelper.CreateToken(user, claims.Data);
                return new SuccessDataResult<AccessToken>(accessToken, "Ok", Messages.success);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<AccessToken>(null, ex.Message, Messages.err_null);
            }
        }



        public IDataResult<string> Login(UserLoginDto userLoginDto)
        {
            var userToCheck = _userService.GetByMail(userLoginDto.Email);
            if (userToCheck.Data == null)
            {
                return new ErrorDataResult<string>(null, "user not found", Messages.err_null);
            }
            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<string>(null, "invalid password", Messages.err_null);
            }

            var tokenResult = CreateAccessToken(userToCheck.Data);
            if (tokenResult.Success)
            {
                var accessToken = tokenResult.Data;
                return new SuccessDataResult<string>(accessToken.Token, "user login successful", Messages.success);

            }
            else
            {
                return new ErrorDataResult<string>(null, "Token creation error", Messages.err_null);
            }
        }





        public IDataResult<bool> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                Email = userRegisterDto.Email,
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userDal.Add(user);
            var userRole = new UserOperationClaim()
            {
                OperationClaimId = 2,
                UserId = user.Id
            };
            _userOperationClaimDal.Add(userRole);
            return new SuccessDataResult<bool>(true, "Ok", Messages.success);
        }

        public IDataResult<User> UserExists(string email)
        {
            try
            {

                var result = _userDal.Get(x => x.Email == email);
                if (result != null)
                {
                    return new ErrorDataResult<User>(null,"this user is registered in the system",Messages.err_null);
                }
                return new SuccessDataResult<User>(result, "Ok", Messages.success);

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(null, ex.Message, Messages.err_null);
            }

        }
    }
}
