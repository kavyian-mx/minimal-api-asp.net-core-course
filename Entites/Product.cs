using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
     public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان دوره الزامی است")]
        [MaxLength(100, ErrorMessage = "عنوان دوره نمی‌تواند بیش از ۱۰۰ کاراکتر باشد")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "توضیحات الزامی است")]
        [MaxLength(500, ErrorMessage = "توضیحات نمی‌تواند بیش از ۵۰۰ کاراکتر باشد")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "تاریخ شروع الزامی است")]
        public DateTime TimeStart { get; set; }

        [Required(ErrorMessage = "تاریخ پایان الزامی است")]
        public DateTime TimeEnd { get; set; }

        [Display(Name = "آنلاین است؟")]
        public bool IsOnline { get; set; }

        [MaxLength(250, ErrorMessage = "آدرس تصویر نمی‌تواند بیش از ۲۵۰ کاراکتر باشد")]
        public string? UrlImage { get; set; }
    }
}
