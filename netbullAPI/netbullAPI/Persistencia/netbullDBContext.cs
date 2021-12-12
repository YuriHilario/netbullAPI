using Microsoft.EntityFrameworkCore;
using netbullAPI.Entidade;

namespace netbullAPI.Persistencia
{
    public class netbullDBContext : DbContext 
    {
        public netbullDBContext(DbContextOptions<netbullDBContext> opt) : base(opt)
        {
                
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Item> Itens { get; set; }
    }
}
