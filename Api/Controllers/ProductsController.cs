using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataLayer;
using ModelLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductComponent productComponent;

        public ProductsController(NetCoreProductManagerContext context)
        {
            productComponent = new ProductComponent(context);
        }

        // GET: api/Products/GetAll
        [HttpGet("GetAllProducts")]
        public async Task<JsonResult> GetAllProducts()
        {
            var products = await productComponent.GetAllProducts();

            return Json(products);
        }

        // GET api/Products/GetProduct/3
        [HttpGet("GetProduct/{id}")]
        public async Task<JsonResult> GetProduct(int id)
        {
            var product = await productComponent.GetProduct(id);

            return Json(product);
        }

        // POST api/Products/CreateProduct
        [HttpPost("CreateProduct")]
        public async Task<JsonResult> CreateProduct([FromBody] Product newProduct)
        {
            var newProductId = await productComponent.Create(newProduct);

            return Json(newProductId);
        }

        // PUT api/Products/EditProduct/3
        [HttpPut("EditProduct/{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] Product product)
        {
            if (product == null || id <= 0)
                return BadRequest();

            var oldProduct = await productComponent.GetProduct(id);

            if(oldProduct == null)
                return NotFound();

            product.Id = id;

            await productComponent.Edit(product);

            return Json("Produto alterado com sucesso!");
        }

        // DELETE api/Products/3
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<JsonResult> DeleteProduct(int id)
        {
            await productComponent.Delete(id);

            return Json("Produto excluído com sucesso!");
        }
    }
}
