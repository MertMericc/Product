using Microsoft.AspNetCore.Mvc.Formatters;
using Product.BLL.Abstract;
using Product.BLL.Constants;
using Product.Core.Entity.Concrete;
using Product.Core.Result;
using Product.Core.Security;
using Product.Dal.Abstract;
using Product.Entity.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.BLL.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserOperationClaimDal _claimDal;

        public UserManager(IUserDal userDal, IUserOperationClaimDal claimDal)
        {
            _userDal = userDal;
            _claimDal = claimDal;
        }

        public IDataResult<bool> Add(UserRegisterDto userAddDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userAddDto.Password, out passwordHash, out passwordSalt);

            var user = new User()
            {
                Email = userAddDto.Email,
                Name = userAddDto.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            var userOperationClaim = new UserOperationClaim()
            {
                UserId = user.Id,
                OperationClaimId = 2
            };

            _userDal.Add(user);
            _claimDal.Add(userOperationClaim);

            return new SuccessDataResult<bool>(true,"Ok",Messages.success);
        }


        public IDataResult<User> GetById(int userId)
        {
            try
            {
                var user = _userDal.Get(x => x.Id == userId);
                return new SuccessDataResult<User>(user);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(null,ex.Message,Messages.err_null);
            }
          
        }

        public IDataResult<User> GetByMail(string email)
        {
            try
            {
                var user = _userDal.Get(x => x.Email == email);
                return new SuccessDataResult<User>(user);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(null, ex.Message, Messages.err_null);
            }

        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));

        }

        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetList().ToList());
        }
    }
}
