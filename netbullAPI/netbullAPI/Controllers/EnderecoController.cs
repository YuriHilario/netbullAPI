using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Busca lista de endere�os do cliente informado.
        /// </summary>
        /// <param name="idPessoa">Indica qual cliente est� sendo consultado.</param>

        [Authorize]
        [HttpGet("{idPessoa}")]
        public async Task<IActionResult> /*IEnumerable<Endereco>*/ Get([FromServices] NE_Endereco neEndereco, int idPessoa)
        {
            IEnumerable<Endereco> listaEnderecos = await neEndereco.BuscaEnderecosPessoa(idPessoa);

            if (!listaEnderecos.Any())
                return NotFound(HttpStatusCode.NotFound);

            return Created("Lista obtida", listaEnderecos);
        }

        /// <summary>
        /// Inclus�o de novo endere�o do cliente.
        /// </summary>
        /// <param name="endereco"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        // POST api/<EnderecoController>
        public async Task<IActionResult> CadastrarNovoEndereco([FromServices] NE_Endereco neEndereco, [FromBody] Endereco endereco)
        {
            try
            {
            if (endereco == null)
                {
                    Notificar("Endere�o vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }

                return Ok(await neEndereco.CadastraNovoEndereco(endereco) 
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

        /// <summary>
        /// Atualiza��o de um endere�o do cliente.
        /// </summary>
        /// <param name="endereco"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        // PUT api/<EnderecoController>
        public async Task<IActionResult> AtualizaEndereco([FromServices] NE_Endereco neEndereco, Endereco endereco)
        {
            try
            {
                if (endereco == null)
                {
                    Notificar("Endere�o vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }

                return Ok(await neEndereco.AtualizaEndereco(endereco) 
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

        /// <summary>
        /// Atualiza��o de um endere�o do cliente (PATCH).
        /// </summary>
        /// <param name="idEndereco"></param>
        /// <param name="endereco"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("{idEndereco}")]
        // PATCH api/<EnderecoController>
        public async Task<IActionResult> AtualizaEnderecoPatch([FromServices] NE_Endereco neEndereco, int idEndereco, [FromBody] Endereco endereco)
        {
            try
            {
                if (idEndereco == 0)
        {
                    Notificar("Id do Endere�o ou o logradouro est� vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }

                return Ok(await neEndereco.AtualizaEnderecoPatch(idEndereco, endereco) 
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

        /// <summary>
        /// Remove um endere�o do cliente.
        /// </summary>
        /// <param name="idEndereco"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{idEndereco}")]
        // DELETE api/<EnderecoController>
        public async Task<IActionResult> ApagaEndereco([FromServices] NE_Endereco neEndereco, int idEndereco)
        {
            try
        {
            if (idEndereco == 0)
                    return NotFound(new { mensagem = "O idEndere�o n�o foi informado." , sucesso = false});

                var resp = await neEndereco.ApagaEndereco(idEndereco);
                if (resp)
                    return Ok(
                        new
                        {
                            mensagem = "Endere�o deletado com sucesso",
                            sucesso = true
                        });
                else
                    return NotFound(
                        new
                        {
                            mensagem = "N�o foi poss�vel deletar endere�o.",
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
        }
    };


}