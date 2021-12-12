﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netbullAPI.Entidade
{
    public class Pedido
    {
        [Key]
        [Required]
        public int pedido_id { get; set; }
        public DateTime pedido_time { get; set; }
        [ForeignKey("Endereco")]
        public int pedido_idEndereco { get; set; }
        public float pedido_valor { get; set; }
        public List<Item> itens { get; set; }
        [ForeignKey("Pessoa")]
        public int pedido_idPessoa { get; set; }
        [ForeignKey("User")]
        public int pedido_idUsuario { get; set; }
        public EnumStatusPedido pedido_status { get; set;}
        //Pedido deve receber uma lista de itens
        //Pessoa deve receber uma lista de pedidos
    }
}
