using Product.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Entity.Dtos.UserDtos
{
    public class UserLoginDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
