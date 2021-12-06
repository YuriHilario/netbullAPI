using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace netbullAPI.Negocio
{
    public class NE_Item
    {
        private DAOItem _daoItem;

        public NE_Item(netbullDBContext PessoaContexto, DAOItem daoItem)
        {
            _daoItem = daoItem;
        }

        public IEnumerable<Item> BuscaItens()
        {
            return _daoItem.BuscaItens();
        }

        public Item BuscaItemPorId(int id)
        {
            return _daoItem.BuscaItemPorId(id);
        }
    }
}
