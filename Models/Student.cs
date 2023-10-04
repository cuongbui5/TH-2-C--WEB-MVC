using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Student
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name không được để trống.")]
    [StringLength(100,MinimumLength = 4,ErrorMessage = "Name phải có tối thiểu 4 kí tự và không được vượt quá 100 kí tự.")]
    public string? Name { get; set; }
    
    // [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "Email không đúng định dạng.")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có định dạng và đuôi là gmail.com.")]
    [Required(ErrorMessage ="Email bắt buộc phải được nhập")]
    public string? Email { get; set; }
    [StringLength(100, MinimumLength = 8)]
    [Required(ErrorMessage ="Mật khẩu không được để trống.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=!]).*$", 
        ErrorMessage = "Mật khẩu phải chứa ít nhất một ký tự viết hoa, viết thường, chữ số và ký tự đặc biệt.")]
    public string? Password { get; set; }
    [Required(ErrorMessage = "Ngành không được để trống")]
    public Branch? Branch { get; set; }
    [Required(ErrorMessage = "Giới tính không được để trống")]
    public Gender Gender { get; set; }
    [Required(ErrorMessage = "Phải chọn hệ bạn đang học.")]
    public bool IsRegular { get; set; }
    [DataType(DataType.MultilineText)]
    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    public string? Address { get; set; }
    [Range(typeof(DateTime), "1/1/1963", "12/31/2005")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Ngày sinh không được để trống")]
    public DateTime? DateOfBirth { get; set; }
    [Required(ErrorMessage = "Điểm không được để trống!")]
    [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10.")]
    public int Score { get; set; }
    
}