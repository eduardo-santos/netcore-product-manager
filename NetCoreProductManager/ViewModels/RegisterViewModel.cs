using System.ComponentModel.DataAnnotations;

namespace NetCoreProductManager.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(100, ErrorMessage = "A {0} senha precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação da senha não são iguais.")]
        public string ConfirmPassword { get; set; }
    }
}
