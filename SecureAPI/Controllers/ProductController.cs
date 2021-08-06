using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace SecureAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductList products;
        public ProductController()
        {
            products = new ProductList();
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(products);
        }
    }
}
