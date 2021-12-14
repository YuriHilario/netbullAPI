using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;

namespace netbullAPI.Repository
{

    public class DAOProduto : DaoBase
    {
        private netbullDBContext _netbullDBContext;
        public DAOProduto(INotificador notificador, IConfiguration configuration, netbullDBContext netbullDBContext) : base(notificador, configuration)
        {
            _netbullDBContext = netbullDBContext;
        }

        public IEnumerable<DAOProduto> BuscaProduto()
        {
            try
            {
              return _netbullDBContext.Produtos.Where(x => x.produto_id != null);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Produto BuscaProdutoPorId(int id)
        {
            try
            {
                var produtoExistente = _netbullDBContext.Produtos.Where(x => x.produto_id == id).FirstOrDefault();
                if (produtoExistente != null)
                    return produtoExistente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public Produto AtualizaProduto(Produto produto)
        {
            try
            {
                var produtoExistente = _netbullDBContext.Produtos.Where(x => x.produto_id == produto.produto_id).FirstOrDefault();
                        
                
                
                    _netbullDBContext.Update(produtoExistente);
                    _netbullDBContext.SaveChanges();
                
                return produto;
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
                var produtoExistente = _netbullDBContext.Produtos.Where(t => t.produto_id == id).FirstOrDefault();
                if (produtoExistente == null)
                    throw new Exception("Produto informado inexistente");
                _netbullDBContext.Remove(produtoExistente);
                _netbullDBContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }



}