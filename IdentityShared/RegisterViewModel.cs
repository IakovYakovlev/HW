using System.ComponentModel.DataAnnotations;

namespace IdentityShared
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле электронная почта обязательно для ввода. Пример -> \"name@name.ru\"")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", 
            ErrorMessage = "Поле должно содержать @. Пример -> \"name@name.ru\"")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле пароль обязательно для ввода")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле подтверждение пароля обязательно для ввода")]
        public string ConfirmPassword { get; set; }
    }
}
