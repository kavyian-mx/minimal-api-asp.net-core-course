using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entites
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "متن نظر الزامی است")]
        [MaxLength(1000, ErrorMessage = "متن نظر نمی‌تواند بیش از ۱۰۰۰ کاراکتر باشد")]
        public string CommentBody { get; set; } = string.Empty;

        [Required]
        public DateTime CommentTime { get; set; } = DateTime.Now;

        // ربط دادن به محصول
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}

