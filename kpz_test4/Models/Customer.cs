using System.ComponentModel.DataAnnotations;

namespace kpz_test4.Models
{
    public class Customer
    {
        public int id { set; get; }
        [Required(ErrorMessage = "Ім'я клієнта є обов'язковим")]
        public string customer_name { set; get; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Телефон повинен містити 10 цифр")]
        public string phone { set; get; }
        public string? email { set; get; }
    }
}
