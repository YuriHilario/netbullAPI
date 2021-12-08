using System.ComponentModel.DataAnnotations;

namespace netbullAPI.Security.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public string user_nome { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string user_accessKey { get; set; }
    }
}
