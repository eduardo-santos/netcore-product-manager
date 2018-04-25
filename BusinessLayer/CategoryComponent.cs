using DataLayer;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CategoryComponent
    {
        private readonly NetCoreProductManagerContext _context;

        public CategoryComponent(NetCoreProductManagerContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetCategory(int idCategory)
        {
            return await _context.Category.AsNoTracking().SingleOrDefaultAsync(m => m.Id == idCategory);
        }

        public async Task<int> Create(Category category)
        {
            try
            {
                category.Created = DateTime.Now;

                _context.Add(category);

                await _context.SaveChangesAsync();

                return category.Id;
            }
            catch(DbUpdateException)
            {
                throw new Exception("Não foi possível criar a categoria. Tente novamente.");
            }
        }

        public async Task Edit(Category category)
        {
            try
            {
                _context.Update(category);

                await _context.SaveChangesAsync();                
            }
            catch (DbUpdateException)
            {
                throw new Exception("Não foi possível editar a categoria. Tente novamente.");
            }
        }

        public async Task Delete(int idCategory)
        {
            try
            {
                var category = await GetCategory(idCategory);

                _context.Category.Remove(category);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Falha na exclusão. Tente novamente.");
            }
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
