using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Util;

namespace netbullAPI.Persistencia
{
    public class DAO_Pessoa : DaoBase
    {
        private netbullDBContext _PessoaContexto { get; set; }
        public DAO_Pessoa(INotificador notificador, IConfiguration configuration, netbullDBContext context) : base(notificador, configuration)
        {
            _PessoaContexto = context;
        }

        public async Task<IEnumerable<Pessoa>> BuscaPessoas()
        {
            try
            {
                var pessoas =  _PessoaContexto.Pessoas.AsEnumerable();
                return pessoas;
            }
            catch (Exception e)
            {
                Notificar(e.Message);
                throw;
            }
        }

        public async Task<Pessoa> BuscaPessoaPorId(int id)
        {
            try
            {
                var pessoa = _PessoaContexto.Pessoas.Where(p => p.pessoa_id == id).FirstOrDefault();
                return pessoa;
            }
            catch (Exception e)
            {
                Notificar(e.Message);
                throw;
            }

        }
        
        public async Task<bool> InserirPessoa(Pessoa pessoa)
        {
            try
            {
                _PessoaContexto.Pessoas.Add(pessoa);
                _PessoaContexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Notificar(e.Message);
                throw;
            }

        }
        public async Task<Pessoa> UpdatePessoa(Pessoa pessoa)
        {
            try
            {
                var pessoaSelecionada = _PessoaContexto.Pessoas.Where(p => p.pessoa_id == pessoa.pessoa_id).FirstOrDefault();
                pessoaSelecionada.pessoa_nome = pessoa.pessoa_nome;
                pessoaSelecionada.pessoa_tipopessoa = pessoa.pessoa_tipopessoa;
                pessoaSelecionada.pessoa_documento = pessoa.pessoa_documento;
                _PessoaContexto.SaveChanges();
                return pessoaSelecionada;
            }
            catch (Exception e)
            {
                Notificar(e.Message);
                throw;
            }

        }

        public async Task<bool> DeletarPessoa(int id)
        {
            try
            {
                var pessoaSelecionada = _PessoaContexto.Pessoas.Where(p => p.pessoa_id == id).FirstOrDefault();
                if (pessoaSelecionada == null)
                    throw new Exception("Pessoa informada inexistente");
                _PessoaContexto.Pessoas.Remove(pessoaSelecionada);
                _PessoaContexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Notificar(e.Message);
                throw;
            }

        }
    }
}
