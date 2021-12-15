using Dapper;
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Security.Models;
using netbullAPI.Util;
using netbullAPI.ViewModels;

namespace netbullAPI.Persistencia
{
    public class DAOPedido : DaoBase
    {
        private netbullDBContext netbullDBContext;
        private IConfiguration configuration;
        public DAOPedido(INotificador notificador, IConfiguration configuration, netbullDBContext netbullDBContext) : base(notificador, configuration)
        {
            this.netbullDBContext = netbullDBContext;
            this.configuration = configuration;
        }

        public IEnumerable<object> BuscaPedidosPessoa(int id)
        {
            var pessoa = netbullDBContext.Pessoas.Where(pessoa => pessoa.pessoa_id == id).FirstOrDefault();
            if (pessoa == null)
            {
                Notificar("Cliente informado inexistente");
                return null;
            }
            else
            {
                var historico_pedidos = from pedido in netbullDBContext.Pedidos
                                        join itens in netbullDBContext.Itens on pedido.pedido_id equals itens.item_idPedido
                                        where pedido.pedido_idPessoa == id
                                        select new
                                        {
                                            pedido = pedido,
                                            itens = itens
                                        };
                return historico_pedidos;
            }
        }

        public int BuscaPedidosUser(string name)
        {
            User user;

            string sqlUser = $@"SELECT user_id FROM users WHERE user_nome = {name}";

            var connection = getConnection();

            using (connection)
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {

                    user = connection.Query<User>(sqlUser, transaction).FirstOrDefault();

                    transaction.Commit();
                }
            }
            return user.user_id;
        }

        public bool DeletaPedido(int id)
        {
            var pedido_existente = netbullDBContext.Pedidos.Where(pedido => pedido.pedido_id == id).FirstOrDefault();
            if (pedido_existente == null)
            {
                Notificar("Pedido informado inexistente");
                return false;
            }
            netbullDBContext.Remove(pedido_existente);
            netbullDBContext.SaveChanges();
            return true;
        }

        public bool AlteraStatusPedido(Pedido pedido, EnumStatusPedido status)
        {
            var pessoa = netbullDBContext.Pessoas.Where(x => x.pessoa_id == pedido.pedido_idPessoa).FirstOrDefault();
            if (pessoa == null)
            {
                Notificar("Cliente informado inexistente");
                return false;
            }
            var pedido_existente = netbullDBContext.Pedidos.Where(p => p.pedido_id == pedido.pedido_id).FirstOrDefault();
            if(pedido_existente == null)
            {
                Notificar("Pedido informado inexistente");
                return false;
            }
            else
            {
                if (pedido_existente.pedido_idPessoa != pedido.pedido_idPessoa)
                {
                    Notificar("Pessoa registrada com este pedido é diferente da informada");
                    return false;
                }
                else
                {
                    pedido_existente.pedido_status = status;
                    netbullDBContext.Update(pedido_existente);
                    netbullDBContext.SaveChanges();
                    return true;
                }
            }
        }

        public Pedido AdicionaPedido(Pedido pedido)
        {
            var pessoa = netbullDBContext.Pessoas.Where(p => p.pessoa_id == pedido.pedido_idPessoa).FirstOrDefault();
            if(pessoa == null)
            {
                Notificar("Cliente informado inexistente");
                return null;
            }
            try
            {
                Pedido novo_pedido = new Pedido()
                {
                    pedido_id = 1,
                    pedido_idPessoa = pedido.pedido_idPessoa,
                    pedido_status = EnumStatusPedido.pedido_reservado,
                    pedido_valor = pedido.pedido_valor,
                    pedido_time = DateTime.UtcNow
                    
                };
                netbullDBContext.Add(novo_pedido);
                netbullDBContext.SaveChanges();
                return novo_pedido;
            }
            catch (Exception e)
            {
                Notificar(e.Message);
                throw e;
            }
        }
    }
}
