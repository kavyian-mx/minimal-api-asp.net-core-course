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

        public List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category{ Id=1, Name="front"},
                new Category{ Id=2, Name="asp.net"},
                new Category{ Id=3, Name="js"},
                new Category{ Id=4, Name="html"}
            };
        }


        public async Task<int> InsertAsync(Category category)
        {
            _ctx.Categories.Add(category);
            await _ctx.SaveChangesAsync();
            return category.Id;
        }


    }
}

