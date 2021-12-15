using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netbullAPI.Entidade
{
    public class Item
    {
        [Key]
        [Required]
        public int iten_id { get; set; }
        public decimal iten_valor { get; set; }
        public int iten_qtdproduto { get; set; }
        [ForeignKey("Pedido")]
        public int iten_idPedido { get; set; }
        [ForeignKey("Produto")]
        public int iten_idProduto { get; set; }
    }
}
