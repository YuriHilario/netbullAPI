using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netbullAPI.Entidade
{
    public class Item
    {
        [Key]
        [Required]
        public int item_id { get; set; }
        public float item_valor { get; set; }
        public float item_quantproduto { get; set; }
        [ForeignKey("Pedido")]
        public int item_idPedido { get; set; }
        public Produto produto { get; set; }
    }
}