using Product.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Entity.Dtos.PageDtos
{
    public class PagingFilterDto : IDto
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
