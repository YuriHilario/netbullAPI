using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Persistencia;

namespace netbullAPI.Repository
{

    public class PessoaRepository
    {
        private netbullDBContext _PessoaContexto;
        public PessoaRepository(netbullDBContext PessoaContexto)
        {
            _PessoaContexto = PessoaContexto;
        }

        public IEnumerable<Pessoa> BuscaPessoas()
        {
            return _PessoaContexto.Pessoas.AsEnumerable();
        }

        public Pessoa BuscaPessoaPorId(int id)
        {
            return _PessoaContexto.Pessoas.Where(x => x.pessoa_id == id).FirstOrDefault();
        }
        
        public Pessoa BuscaPessoaPorDocumento(int documento)
        {
            return _PessoaContexto.Pessoas.Where(x => x.pessoa_documento == documento).FirstOrDefault();
        }
        public IEnumerable<Pessoa> BuscaPessoasPorNome(string nome)
        {
            return _PessoaContexto.Pessoas.Where(x => x.pessoa_nome.Contains(nome)).AsEnumerable();
        }
    };


}