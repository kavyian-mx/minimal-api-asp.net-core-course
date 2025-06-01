using DataBase;
using Entites;
using Microsoft.EntityFrameworkCore;

namespace Service
{
   public class TeacherService
    {
        private readonly Course _ctx;   
        public TeacherService(Course ctx)
        {
            _ctx = ctx;

        }



        public async Task<List<Teacher>> GetTeachers()
        {
            return await _ctx.Teachers.AsNoTracking().ToListAsync();
        }

        public async Task<Teacher?> GetTeacherIdAsync(int id)
        {
            return await _ctx.Teachers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Teacher>> SearchAsync(string? lastName, string? firstName)
        {
            var query = _ctx.Teachers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(t => t.LastName.Contains(lastName));

            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(t => t.FirstName.Contains(firstName));

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _ctx.Teachers.AnyAsync(c => c.Id == id);
        }
        public async Task<int> InsertAsync(Teacher teacher)
        {
            _ctx.Teachers.Add(teacher);
            await _ctx.SaveChangesAsync();
            return teacher.Id;
        }

        public async Task<bool> UpdateAsync(Teacher updatedTeacher)
        {
            var teacher = await _ctx.Teachers.FirstOrDefaultAsync(t => t.Id == updatedTeacher.Id);
            if (teacher == null) return false;

            teacher.FirstName = updatedTeacher.FirstName;
            teacher.LastName = updatedTeacher.LastName;
            teacher.Birtday = updatedTeacher.Birtday;
            teacher.UrlImage = updatedTeacher.UrlImage;

            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teacher = await _ctx.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null) return false;

            _ctx.Teachers.Remove(teacher);
            await _ctx.SaveChangesAsync();
            return true;
        }



    }
}
