using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace netbullAPI.Negocio
{
    public class NE_Pessoa
    {
        private PessoaRepository PE_Aluno;

        public NE_Pessoa(netbullDBContext PessoaContexto)
        {
            PE_Aluno = new PessoaRepository(PessoaContexto);
        }

        public IEnumerable<Pessoa> BuscaPessoas()
        {
            return PE_Aluno.BuscaPessoas();
        }

        public Pessoa BuscaPessoaPorId(int id)
        {
            return PE_Aluno.BuscaPessoaPorId(id);
        }
    }
}