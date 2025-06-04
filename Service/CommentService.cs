using DataBase;
using Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public class CommentService
    {
        private readonly Course _ctx;

        public CommentService( Course ctx)
        {
            _ctx = ctx;
        }


        public async Task<List<Comment>> GetListComment()
        {

            return await _ctx.Comments.AsNoTracking().OrderBy(p => p.CommentTime).ToListAsync();
        }


        public async Task<Comment?> GetCommentAsyncId(int id)
        {
            return await _ctx.Comments.FirstOrDefaultAsync(p => p.Id == id);

        }

        public async Task<int> InsertAsync(Comment comment)
        {
            _ctx.Comments.Add(comment);
            await _ctx.SaveChangesAsync();
            return comment.Id;

        }
        public async Task<bool> CommentUpdate(Comment commentUpdate)
        {
            var comment = await _ctx.Comments.FirstOrDefaultAsync(t => t.Id == commentUpdate.Id);
            if (comment == null) return false;
            comment.CommentBody = commentUpdate.CommentBody;

            await _ctx.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _ctx.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return false;


            _ctx.Comments.Remove(comment);

            await _ctx.SaveChangesAsync();

            return true;

        }

        public async Task<List<Comment>> GetCommentsByProductIdAsync(int productId)
        {
            return await _ctx.Comments
                .Where(c => c.ProductId == productId)
                .OrderByDescending(c => c.CommentTime)
                .AsNoTracking()
                .ToListAsync();
        }


    }
}
