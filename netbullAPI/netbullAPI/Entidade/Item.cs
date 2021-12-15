﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netbullAPI.Entidade
{
    public class Item
    {
        [Key]
        [Required]
        public int item_id { get; set; }
        public decimal item_valor { get; set; }
        public int item_qtdproduto { get; set; }
        [ForeignKey("Pedido")]
        public int item_idPedido { get; set; }
        [ForeignKey("Produto")]
        public int item_idProduto { get; set; }
        virtual public Produto produto { get; set; }
    }
}
