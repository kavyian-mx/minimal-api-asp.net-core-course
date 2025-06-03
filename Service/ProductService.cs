using DataBase;
using DataBase.Migrations;
using Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Service
{
    public class ProductService
    {
        private readonly Course _ctx;

        public ProductService(Course ctx)
        {
            _ctx = ctx;
        }

       public async Task<List<Product>> GetProducts()
        {

            return await _ctx.products.AsNoTracking().OrderBy(p=>p.Title).ToListAsync();
        }


        public async Task<Product?> GetProductAsyncId(int id)
        {
            return await _ctx.products.FirstOrDefaultAsync(c => c.Id == id);

        }


        public async Task<List<Product>> SearchAsync(string? title)
        {
            var query = _ctx.products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(p => p.Title.Contains(title));

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _ctx.products.AnyAsync(c => c.Id == id);
        }


        public async Task<int> InsertAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _ctx.products.Add(product);
            await _ctx.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _ctx.products.FindAsync(id);
            if (product == null) return false;

            _ctx.products.Remove(product);
            await _ctx.SaveChangesAsync();
            return true;

        }
        public async Task<bool> UpdateAsync(Product updatedProduct)
        {
            var product = await _ctx.products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);
            if (product == null) return false;

            product.Title = updatedProduct.Title;
            product.Description = updatedProduct.Description;
            product.TimeStart = updatedProduct.TimeStart;
            product.TimeEnd = updatedProduct.TimeEnd;
            product.IsOnline = updatedProduct.IsOnline;
            product.UrlImage = updatedProduct.UrlImage;

            await _ctx.SaveChangesAsync();
            return true;
        }



    }
}
