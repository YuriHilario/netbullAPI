using Microsoft.AspNetCore.Mvc;
using Negocio;
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Persistencia;
using netbullAPI.Util;
using System.Net;

namespace netbullAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : BaseController
    public class EnderecoController : ControllerBase
    {
        private List<Pessoa> listaPessoas;
        private NE_Endereco neEndereco;
        public EnderecoController(INotificador notificador) : base(notificador)
        public EnderecoController(netbullDBContext EnderecoContexto)
        {
            neEndereco = new NE_Endereco(EnderecoContexto);
        }

        //GET: api/<EnderecoController>
        [HttpGet("{idPessoa}")]
        public IActionResult /*IEnumerable<Endereco>*/ Get([FromServices] NE_Endereco neEndereco, int idPessoa)
        public IActionResult /*IEnumerable<Endereco>*/ Get(int idPessoa)
        {
            IEnumerable<Endereco> listaEnderecos = neEndereco.BuscaEnderecosPessoa(idPessoa);

            if (!listaEnderecos.Any())
                return NotFound(HttpStatusCode.NotFound);
                return BadRequest(HttpStatusCode.NotFound);

            return Created("Lista obtida", listaEnderecos);
        }


        [HttpPost]
        // POST api/<EnderecoController>
        public IActionResult CadastrarNovoEndereco([FromServices] NE_Endereco neEndereco, [FromBody] Endereco endereco)
        public IActionResult CadastrarNovoEndereco([FromBody] Endereco endereco)
        {
            try
            {
            if (endereco == null)
                {
                    Notificar("Endereço vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }
                return BadRequest(HttpStatusCode.NoContent);

                return Ok(neEndereco.CadastraNovoEndereco(endereco) 
                    ? new { mensagem = "Inserido com sucesso.", sucesso = true } 
                : new { mensagem = "Problema ao inserir.", sucesso = false });
            if (neEndereco.CadastraNovoEndereco(endereco))
                return Ok();
            else
                return BadRequest(HttpStatusCode.BadRequest);
        }
           catch(Exception ex)
            {
               return BadRequest(
               new
               {
                   mensagem = ex.Message,
                   sucesso = false
               });
            }

        }

        [HttpPut]
        // PUT api/<EnderecoController>
        public IActionResult AtualizaEndereco([FromServices] NE_Endereco neEndereco, Endereco endereco)
        {
            try
            {
                if (endereco == null)
                {
                    Notificar("Endereço vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }

                return Ok(neEndereco.AtualizaEndereco(endereco) 
                    ? new { mensagem = "Atualizado com sucesso.", sucesso = true}
                : new { mensagem = "Problema ao atualizar.", sucesso = false });
            }
            catch (Exception ex)
            {
                return BadRequest(
                new
                {
                    mensagem = ex.Message,
                    sucesso = false
                });
            }
        }

        [HttpPatch("{idEndereco}")]
        // PATCH api/<EnderecoController>
        public IActionResult AtualizaEnderecoPatch([FromServices] NE_Endereco neEndereco, int idEndereco, [FromBody] Endereco endereco)
        {
            try
            {
                if (idEndereco == 0)
        public IActionResult AtualizaEndereco([FromBody] Endereco endereco)
        {
                    Notificar("Id do Endereço ou o logradouro está vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }
            if (endereco.endereco_id == 0 || (endereco.endereco_logradouro == null && endereco.endereco_numero == 0 && endereco.endereco_complemento == null))
                return BadRequest();

                return Ok(neEndereco.AtualizaEnderecoPatch(idEndereco, endereco) 
                    ? new { mensagem = "Atualizado com sucesso.", sucesso = true}
                    : new { mensagem = "Problema ao inserir.", sucesso = false });
            }
            catch (Exception ex)
            {
                return BadRequest(
                new
                {
                    mensagem = ex.Message,
                    sucesso = false
                });
            }
            if (neEndereco.AtualizaEndereco(endereco.endereco_id, endereco.endereco_logradouro, endereco.endereco_numero, endereco.endereco_complemento))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{idEndereco}")]
        [HttpDelete]
        // DELETE api/<EnderecoController>
        public IActionResult ApagaEndereco([FromServices] NE_Endereco neEndereco, int idEndereco)
        {
            try
        public IActionResult ApagaEndereco(int idEndereco)
        {
            if (idEndereco == 0)
                    return NotFound(new { mensagem = "O idEndereço não foi informado." , sucesso = false});

                var resp = neEndereco.ApagaEndereco(idEndereco);
                if (resp)
                    return Ok(
                        new
                        {
                            mensagem = "Endereço deletado com sucesso",
                            sucesso = true
                        });
                else
                    return NotFound(
                        new
                        {
                            mensagem = "Não foi possível deletar endereço.",
                            sucesso = false
                        });
            }
            catch (Exception e)
            {
                return BadRequest(
                    new
                    {
                        mensagem = e.Message,
                        sucesso = false
                    });
            }
                return NoContent();
            
            return neEndereco.ApagaEndereco(idEndereco) ?  Ok() : BadRequest();
        }
    };


}