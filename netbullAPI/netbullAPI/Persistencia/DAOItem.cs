using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;

namespace netbullAPI.Repository
{

    public class DAOItem : DaoBase
    {
        private netbullDBContext _netbullDBContext;
        public DAOItem(INotificador notificador, IConfiguration configuration, netbullDBContext netbullDBContext) : base(notificador, configuration)
        {
            _netbullDBContext = netbullDBContext;
        }

        public IEnumerable<Item> BuscaItens()
        {
            return _netbullDBContext.Itens.AsEnumerable();
        }

        public Item BuscaItemPorId(int id)
        {
            return _netbullDBContext.Itens.Where(x => x.item_id == id).FirstOrDefault();
        }
    }


}