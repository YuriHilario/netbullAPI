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
    {
        public EnderecoController(INotificador notificador) : base(notificador)
        {
        }

        //GET: api/<EnderecoController>
        [HttpGet("{idPessoa}")]
        public IActionResult /*IEnumerable<Endereco>*/ Get([FromServices] NE_Endereco neEndereco, int idPessoa)
        {
            IEnumerable<Endereco> listaEnderecos = neEndereco.BuscaEnderecosPessoa(idPessoa);

            if (!listaEnderecos.Any())
                return NotFound(HttpStatusCode.NotFound);

            return Created("Lista obtida", listaEnderecos);
        }


        [HttpPost]
        // POST api/<EnderecoController>
        public IActionResult CadastrarNovoEndereco([FromServices] NE_Endereco neEndereco, [FromBody] Endereco endereco)
        {
            try
            {
            if (endereco == null)
                {
                    Notificar("Endereço vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }

                return Ok(neEndereco.CadastraNovoEndereco(endereco) 
                    ? new { mensagem = "Inserido com sucesso.", sucesso = true } 
                : new { mensagem = "Problema ao inserir.", sucesso = false });
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
        {
                    Notificar("Id do Endereço ou o logradouro está vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }

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
        }

        [HttpDelete("{idEndereco}")]
        // DELETE api/<EnderecoController>
        public IActionResult ApagaEndereco([FromServices] NE_Endereco neEndereco, int idEndereco)
        {
            try
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
            
            return neEndereco.ApagaEndereco(idEndereco) ?  Ok() : BadRequest();
        }
    };


}