using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace netbullAPI.Negocio
{
    public class NE_Produto
    {
        private DAOProduto _daoProduto;

        public NE_Produto(DAOProduto daoProduto)
        {
            _daoProduto = daoProduto;
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            return await _daoProduto.GetAllAsync();
        }

        public async Task<Produto> GetPorIdAsync(int id)
        {
            return await _daoProduto.GetPorIdAsync(id);
        }

        public async Task<Produto> AdicionaProduto(Produto produto)
        {
            try
            {
                return await _daoProduto.AdicionaProduto(produto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Produto> AtualizaProduto(Produto produto)
        {
            try
            {
                return await _daoProduto.AtualizaProduto(produto);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<bool> DeletaProduto(int id)
        {
            try
            {
                return await _daoProduto.DeletaProduto(id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
   
}
