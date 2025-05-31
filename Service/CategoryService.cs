using DataBase;
using Entites;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Service
{
    public class CategoryService
    {
        private readonly Course _ctx;

        public CategoryService(Course ctx)
        {
            _ctx = ctx;
        }

        public async Task< List<Category>> GetCategories()
        {
            return await _ctx.Categories.OrderBy(c=>c.Name).AsNoTrackingWithIdentityResolution().ToListAsync();
        }



        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _ctx.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<int> InsertAsync(Category category)
        {
            _ctx.Categories.Add(category);
            await _ctx.SaveChangesAsync();
            return category.Id;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            var categoryToUpdate = await _ctx.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (categoryToUpdate == null)
                return false; 

            categoryToUpdate.Name = category.Name;

            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _ctx.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
                return false; //      

            _ctx.Categories.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }



    }
}

