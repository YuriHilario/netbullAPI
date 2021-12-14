using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace netbullAPI.Negocio
{
    public class NE_Produto
    {
        private DAOProduto _daoProduto;

        public NE_Produto(netbullDBContext PessoaContexto, DAOProduto daoProduto)
        {
            _daoProduto = daoProduto;
        }

        public IEnumerable<Produto> BuscaProduto()
        {
            return _daoProduto.BuscaProduto();
        }

        public Produto BuscaProdutoPorId(int id)
        {
            return _daoProduto.BuscaProdutoPorId(id);
        }
    }
    public Produto AdicionaProduto(Produto produto)
    {
        try
        {
            return _daoProduto.AdicionaProduto(produto);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public Produto AtualizaProduto(Produto produto)
    {
        try
        {
            return _daoProduto.AtualizaProduto(produto);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public bool DeletaProduto(int id)
    {
        try
        {
            return _daoProduto.DeletaProduto(id);
        }
        catch (Exception e)
        {

            throw e;
        }
    }
}
