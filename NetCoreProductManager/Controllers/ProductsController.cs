using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelLayer;
using System;
using System.Threading.Tasks;

namespace NetCoreProductManager.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ProductComponent productComponent;
        private readonly CategoryComponent categoryComponent;

        public ProductsController(NetCoreProductManagerContext context)
        {
            productComponent = new ProductComponent(context);
            categoryComponent = new CategoryComponent(context);
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await productComponent.GetAllProducts();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var product = await productComponent.GetProduct(id.Value);

            if (product == null)
                return NotFound();
            
            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var categories = await categoryComponent.GetAllCategories();

            if (categories == null)
                RedirectToAction("Index");

            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IdCategory,Price")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await productComponent.Create(product);

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                //Log the error
                ModelState.AddModelError("", ex.Message);
            }

            var categories = await categoryComponent.GetAllCategories();

            if (categories == null)
                RedirectToAction("Index");

            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var product = await productComponent.GetProduct(id.Value);

            if (product == null)
                return NotFound();

            var categories = await categoryComponent.GetAllCategories();

            if (categories == null)
                RedirectToAction("Index");

            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IdCategory,Price,Created")] Product product)
        {
            try
            {
                if (id != product.Id)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    await productComponent.Edit(product);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //Log the error
                ModelState.AddModelError("", ex.Message);
            }

            var categories = await categoryComponent.GetAllCategories();

            if (categories == null)
                RedirectToAction("Index");

            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id, string deleteErrorMessage = null)
        {
            if (!id.HasValue)
                return NotFound();

            var product = await productComponent.GetProduct(id.Value);

            if (product == null)
                return NotFound();

            if (deleteErrorMessage != null)
                ViewData["ErrorMessage"] = deleteErrorMessage;

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await productComponent.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { id = id, deleteErrorMessage = ex.Message });
            }
        }
    }
}
