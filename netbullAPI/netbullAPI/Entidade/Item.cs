using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace netbullAPI.Entidade
{
    public class Item
    {
        [Key]
        [Required]
        public int item_id { get; set; }
        [Required]
        public string item_nome { get; set; }   
        public double item_valor { get; set; }  
        public EnumTipoItem item_tipo { get; set; }
    }
}
