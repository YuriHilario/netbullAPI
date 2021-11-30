using System.ComponentModel.DataAnnotations;

namespace netbullAPI.Entidade
{
    public class Telefone
    {
        [Key]
        [Required]
        public int telefone_id { get; set; }
        public int telefone_numero { get; set; }
        public int telefone_idPessoa { get; set; }
    }
}
