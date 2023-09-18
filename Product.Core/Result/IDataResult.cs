using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Result
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
