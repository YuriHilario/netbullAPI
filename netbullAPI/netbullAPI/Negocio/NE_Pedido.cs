using netbullAPI.Entidade;
using netbullAPI.Persistencia;

namespace netbullAPI.Negocio
{
    public class NE_Pedido
    {
        private DAOPedido daoPedido;
        public NE_Pedido(DAOPedido daoPedido)
        {
            this.daoPedido = daoPedido;
        }
        public IEnumerable<Pedido> BuscaPedidosCliente(int id)
        {
            return daoPedido.BuscaPedidosPessoa(id);
        }
        public Pedido AdicionaPedido(Pedido pedido)
        {
            return daoPedido.AdicionaPedido(pedido);
        }
        public bool AlteraStatusPedido(Pedido pedido, EnumStatusPedido status)
        {
            return daoPedido.AlteraStatusPedido(pedido, status);
        }

        public bool DeletaPedido(int id)
        {
            return daoPedido.DeletaPedido(id);
        }
    }
}
