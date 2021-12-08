using System.ComponentModel.DataAnnotations;

namespace netbullAPI.Security.ViewModels
{
    public class RegistrarUserViewModel
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public string user_nome { get; set; }
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        public string user_email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string user_accessKey { get; set; }
    }
}
