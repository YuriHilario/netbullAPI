using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Util;

namespace netbullAPI.Persistencia
{
    public class DAOItem : DaoBase
    {
        private netbullDBContext netbullDBContext;
        public DAOItem(INotificador notificador, IConfiguration configuration, netbullDBContext netbullDBContext) : base(notificador, configuration)
        {
            this.netbullDBContext = netbullDBContext;
        }
        public IEnumerable<Item> BuscaItemPedido(int id)
        {
            try
            {
                var pedido = netbullDBContext.Pedidos.Where(p => p.pedido_id == id).FirstOrDefault();
                if (pedido == null)
                {
                    Notificar("Pedido informado inexistente");
                    return null;
                }
                else
                {
                    var itens = from item in netbullDBContext.Itens
                                where item.iten_idPedido == id
                                select item;
                    return itens;
                }
            }
            catch (Exception e)
            {
                Notificar(e.Message);
                throw e;
            }
        }
        public Item AdicionaItem(Item item)
        {
            var pedido = netbullDBContext.Pedidos.Where(p => p.pedido_id == item.iten_idPedido).FirstOrDefault();
            if (pedido == null)
            {
                Notificar("Pedido informado inexistente");
                return null;
            }
            try
            {
                var produto = netbullDBContext.Produtos.Where(p => p.produto_id == item.iten_idProduto).FirstOrDefault();
                Item novo_item = new Item()
                {
                    iten_id = netbullDBContext.Itens.Max(i => i.iten_id) + 1,
                    iten_idPedido = item.iten_idPedido,
                    iten_qtdproduto = item.iten_qtdproduto,
                    iten_valor = produto.produto_valor * item.iten_qtdproduto
                };
                netbullDBContext.Add(novo_item);
                netbullDBContext.SaveChanges();
                return novo_item;
            }
            catch(Exception e)
            {
                Notificar(e.Message);
                throw e;
            }
        }
        public bool DeletaItem(int id)
        {
            var item_existente = netbullDBContext.Itens.Where(item => item.iten_id == id).FirstOrDefault();
            if(item_existente == null)
            {
                Notificar("Item informado inexistente");
                return false;
            }
            netbullDBContext.Remove(item_existente);
            netbullDBContext.SaveChanges();
            return true;
        }
        public bool AlteraQuantidadeProduto(Item item, int quantidade)
        {
            var pedido = netbullDBContext.Pedidos.Where(x => x.pedido_id == item.iten_idPedido).FirstOrDefault();
            if(pedido == null)
            {
                Notificar("Pedido informado inexistente");
                return false;
            }
            var item_existente = netbullDBContext.Itens.Where(i => i.iten_id == item.iten_id).FirstOrDefault();
            if (item_existente == null)
            {
                Notificar("Item informado inexistente");
                return false;
            }
            else
            {
                item_existente.iten_qtdproduto = quantidade;
                netbullDBContext.Update(item_existente);
                netbullDBContext.SaveChanges();
                return true;
            }
        }
    }
}
