using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;
using netbullAPI.Util;

namespace netbullAPI.Repository
{

    public class DAOProduto : DaoBase
    {
        private netbullDBContext _netbullDBContext;
        public DAOProduto(INotificador notificador, IConfiguration configuration, netbullDBContext netbullDBContext) : base(notificador, configuration)
        {
            _netbullDBContext = netbullDBContext;
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            List<Produto> context = null;
            try
            {                
                context = _netbullDBContext.Produtos.Where(x => x.produto_id != null).ToList();
                if (context == null)
                {
                    Notificar("Produtos não encontrados");
                }

                return context;

            }
            catch (Exception e)
            {
                Notificar("Não foi possível encontrar produtos.");
                return context;
            }
        }

        internal Task<Produto> AdicionaProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public async Task<Produto> GetPorIdAsync(int id)
        {
            try
            {
                var produtoExistente = _netbullDBContext.Produtos.Where(x => x.produto_id == id).FirstOrDefault();
                if (produtoExistente == null)
                {
                    Notificar("Produto não existente");
                }
                return produtoExistente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Produto> AtualizaProduto(Produto produto)
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

        public async Task<bool> DeletaProduto(int id)
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