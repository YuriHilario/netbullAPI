using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace netbullAPI.Entidade
{
    public class Produto
    {
        [Key]
        [Required]
        public int produto_id { get; set; }
        [Required]
        public string produto_nome { get; set; }   
        public double produto_valor { get; set; }
        [ForeignKey("Item")]
        public Item Item { get; set; }  
    }
}
