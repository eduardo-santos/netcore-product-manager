using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Threading.Tasks;

namespace NetCoreProductManager.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly CategoryComponent categoryComponent;

        public CategoriesController(NetCoreProductManagerContext context)
        {
            categoryComponent = new CategoryComponent(context);
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await categoryComponent.GetAllCategories();

            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var category = await categoryComponent.GetCategory(id.Value);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await categoryComponent.Create(category);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //Log the error
                ModelState.AddModelError("", ex.Message);
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            var category = await categoryComponent.GetCategory(id.Value);

            if (category == null)
                return NotFound();
            
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Created")] Category category)
        {
            try
            {
                if (id != category.Id)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    await categoryComponent.Edit(category);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //Log the error
                ModelState.AddModelError("", ex.Message);
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id, string deleteErrorMessage = null)
        {
            if (!id.HasValue)
                return NotFound();

            var category = await categoryComponent.GetCategory(id.Value);

            if (category == null)
                return NotFound();

            if (deleteErrorMessage != null)
                ViewData["ErrorMessage"] = deleteErrorMessage;

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await categoryComponent.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { id = id, deleteErrorMessage = ex.Message });
            }
        }
    }
}
