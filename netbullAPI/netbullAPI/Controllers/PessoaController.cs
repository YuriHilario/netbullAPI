using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;

namespace netbullAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private List<Pessoa> listaPessoas;
        private NE_Pessoa NE_Aluno;
        public PessoaController(netbullDBContext PessoaContexto)
        {
            NE_Aluno = new NE_Pessoa(PessoaContexto);
        }

        private NE_Pessoa nePEssoa;

        //GET: api/<AlunosController>
        [HttpGet]
        public IEnumerable<Pessoa> Get()
        {
            return NE_Aluno.BuscaPessoas();
        }

        [HttpGet("{id}")]
        //GET: api/<AlunosController>(id)
        public Pessoa GetPorId(int id)
        {
            return NE_Aluno.BuscaPessoaPorId(id);
        }
    };     
}