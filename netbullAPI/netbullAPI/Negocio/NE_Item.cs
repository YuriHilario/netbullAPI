using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace netbullAPI.Negocio
{
    public class NE_Item
    {
        private ItemRepository PE_Item;

        public NE_Item(netbullDBContext PessoaContexto)
        {
            PE_Item = new ItemRepository(PessoaContexto);
        }

        public IEnumerable<Item> BuscaItens()
        {
            return PE_Item.BuscaItens();
        }

        public Item BuscaItemPorId(int id)
        {
            return PE_Item.BuscaItemPorId(id);
        }
    }
}
