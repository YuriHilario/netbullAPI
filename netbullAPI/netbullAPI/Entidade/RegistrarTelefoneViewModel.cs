using System.ComponentModel.DataAnnotations.Schema;

namespace netbullAPI.Entidade
{
    public class RegistrarTelefoneViewModel
    {
        public int telefone_numero { get; set; }
        [ForeignKey("Pessoa")]
        public int telefone_idPessoa { get; set; }
    }
}
