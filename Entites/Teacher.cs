using System.ComponentModel.DataAnnotations;

public class Teacher
{
    public int Id { get; set; }

    [Required(ErrorMessage = "نام اجباری است")]
    [StringLength(50, ErrorMessage = "نام نباید بیشتر از ۵۰ کاراکتر باشد")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "نام خانوادگی اجباری است")]
    [StringLength(50, ErrorMessage = "نام خانوادگی نباید بیشتر از ۵۰ کاراکتر باشد")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "تاریخ تولد الزامی است")]
    [DataType(DataType.Date)]
    public DateTime Birtday { get; set; }

    [Required(ErrorMessage = "آدرس عکس اجباری است")]
    [StringLength(200, ErrorMessage = "آدرس عکس نباید بیشتر از ۲۰۰ کاراکتر باشد")]
    [Url(ErrorMessage = "فرمت آدرس عکس معتبر نیست")]
    public string UrlImage { get; set; }
}
