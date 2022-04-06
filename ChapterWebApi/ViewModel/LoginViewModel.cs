using System.ComponentModel.DataAnnotations;

namespace ChapterWebApi.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o email do isuário")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário")]
        public string senha { get; set; }
    }
}
