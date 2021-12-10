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
        /// Busca lista de endereços do cliente informado.
        /// </summary>
        /// <param name="idPessoa">Indica qual cliente está sendo consultado.</param>

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
        /// Inclusão de novo endereço do cliente.
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
                    Notificar("Endereço vazio.");
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
        /// Atualização de um endereço do cliente.
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
                    Notificar("Endereço vazio.");
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
        /// Atualização de um endereço do cliente (PATCH).
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
                    Notificar("Id do Endereço ou o logradouro está vazio.");
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
        /// Remove um endereço do cliente.
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
                    return NotFound(new { mensagem = "O idEndereço não foi informado." , sucesso = false});

                var resp = await neEndereco.ApagaEndereco(idEndereco);
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
        }
    };


}