using Microsoft.AspNetCore.Mvc;
using Negocio;
using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using System.Net;

namespace netbullAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private List<Pessoa> listaPessoas;
        private NE_Endereco neEndereco;
        public EnderecoController(netbullDBContext EnderecoContexto)
        {
            neEndereco = new NE_Endereco(EnderecoContexto);
        }

        //GET: api/<EnderecoController>
        [HttpGet("{idPessoa}")]
        public IActionResult /*IEnumerable<Endereco>*/ Get(int idPessoa)
        {
            IEnumerable<Endereco> listaEnderecos = neEndereco.BuscaEnderecosPessoa(idPessoa);

            if (!listaEnderecos.Any())
                return BadRequest(HttpStatusCode.NotFound);

            return Created("Lista obtida", listaEnderecos);
        }


        [HttpPost]
        // POST api/<EnderecoController>
        public IActionResult CadastrarNovoEndereco([FromBody] Endereco endereco)
        {
            if (endereco == null)
                return BadRequest(HttpStatusCode.NoContent);

            if (neEndereco.CadastraNovoEndereco(endereco))
                return Ok();
            else
                return BadRequest(HttpStatusCode.BadRequest);
        }

        [HttpPut]
        // PUT api/<EnderecoController>
        public IActionResult AtualizaEndereco([FromBody] Endereco endereco)
        {
            if (endereco.endereco_id == 0 || (endereco.endereco_logradouro == null && endereco.endereco_numero == 0 && endereco.endereco_complemento == null))
                return BadRequest();

            if (neEndereco.AtualizaEndereco(endereco.endereco_id, endereco.endereco_logradouro, endereco.endereco_numero, endereco.endereco_complemento))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        // DELETE api/<EnderecoController>
        public IActionResult ApagaEndereco(int idEndereco)
        {
            if (idEndereco == 0)
                return NoContent();
            
            return neEndereco.ApagaEndereco(idEndereco) ?  Ok() : BadRequest();
        }
    };


}