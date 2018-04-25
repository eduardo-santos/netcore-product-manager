using DataLayer;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ProductComponent
    {
        private readonly NetCoreProductManagerContext _context;

        public ProductComponent(NetCoreProductManagerContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Product.Include(r => r.Category).ToListAsync();
        }

        public async Task<Product> GetProduct(int idProduct)
        {
            return await _context.Product.Include(r => r.Category).AsNoTracking().SingleOrDefaultAsync(m => m.Id == idProduct);
        }

        public async Task<int> Create(Product Product)
        {
            try
            {
                Product.Created = DateTime.Now;

                _context.Add(Product);

                await _context.SaveChangesAsync();

                return Product.Id;
            }
            catch (DbUpdateException)
            {
                throw new Exception("Não foi possível criar o produto. Tente novamente.");
            }
        }

        public async Task Edit(Product Product)
        {
            try
            {
                _context.Update(Product);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Não foi possível editar o produto. Tente novamente.");
            }
        }

        public async Task Delete(int idProduct)
        {
            try
            {
                var Product = await GetProduct(idProduct);

                _context.Product.Remove(Product);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Falha na exclusão. Tente novamente.");
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
