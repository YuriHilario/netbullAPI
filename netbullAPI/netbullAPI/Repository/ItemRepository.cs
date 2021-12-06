using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Persistencia;

namespace netbullAPI.Repository
{

    public class ItemRepository
    {
        private netbullDBContext _ItemContexto;
        public ItemRepository(netbullDBContext ItemContexto)
        {
            _ItemContexto = ItemContexto;
        }

        public IEnumerable<Item> BuscaItens()
        {
            return _ItemContexto.Itens.AsEnumerable();
        }

        public Item BuscaItemPorId(int id)
        {
            return _ItemContexto.Itens.Where(x => x.item_id == id).FirstOrDefault();
        }
    }


}