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
    public class CategoriesController : Controller
    {
        private readonly CategoryComponent categoryComponent;

        public CategoriesController(NetCoreProductManagerContext context)
        {
            categoryComponent = new CategoryComponent(context);
        }

        // GET: api/categories/GetAll
        [HttpGet("GetAllCategories")]
        public async Task<JsonResult> GetAllCategories()
        {
            var categories = await categoryComponent.GetAllCategories();

            return Json(categories);
        }

        // GET api/categories/GetCategory/3
        [HttpGet("GetCategory/{id}")]
        public async Task<JsonResult> GetCategory(int id)
        {
            var category = await categoryComponent.GetCategory(id);

            return Json(category);
        }

        // POST api/categories/CreateCategory
        [HttpPost("CreateCategory")]
        public async Task<JsonResult> CreateCategory([FromBody] Category newCategory)
        {
            var newCategoryId = await categoryComponent.Create(newCategory);

            return Json(newCategoryId);
        }

        // PUT api/categories/EditCategory/3
        [HttpPut("EditCategory/{id}")]
        public async Task<IActionResult> EditCategory(int id, [FromBody] Category category)
        {
            if (category == null || id <= 0)
                return BadRequest();

            var oldCategory = await categoryComponent.GetCategory(id);

            if(oldCategory == null)
                return NotFound();

            category.Id = id;

            await categoryComponent.Edit(category);

            return Json("Categoria alterada com sucesso!");
        }

        // DELETE api/categories/3
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<JsonResult> DeleteCategory(int id)
        {
            await categoryComponent.Delete(id);

            return Json("Categoria excluída com sucesso!");
        }
    }
}
