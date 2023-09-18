using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.BLL.Abstract;
using Product.Entity.Dtos.ProductDtos;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("addproduct")]
        public IActionResult Add(ProductAddDto productAddDto)
        {
            var result = _productService.AddProduct(productAddDto);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getallproduct")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAllProduct();
            return Ok(result);
        }
        //[Authorize]
        [HttpPost("changestatusproduct")]
        public IActionResult ChangeStatus(int productId) 
        {
            var result = _productService.ChangeStatusProduct(productId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("getproductbyid")]
        public IActionResult GetProductById(int productId)
        {
            var result = _productService.GetProductById(productId);
            return Ok(result);
        }
        //[Authorize]
        [HttpPost("updateproduct")]
        public IActionResult UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var result = _productService.UpdateProduct(productUpdateDto);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("getlistwithpageproduct")]
        public IActionResult GetListWithPage(ProductListFilterDto productListFilterDto)
        {
            var result = _productService.GetListProductWithPage(productListFilterDto);
            return Ok(result);
        }
    }
}
