using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
   public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام دسته‌بندی الزامی است.")]
        [StringLength(100, ErrorMessage = "نام دسته‌بندی نباید بیش از 100 کاراکتر باشد.")]
        public string Name { get; set; } = "";
    }
}
