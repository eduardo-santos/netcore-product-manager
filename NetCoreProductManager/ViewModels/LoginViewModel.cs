using System.ComponentModel.DataAnnotations;

namespace NetCoreProductManager.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar?")]
        public bool RememberMe { get; set; }
    }
}
