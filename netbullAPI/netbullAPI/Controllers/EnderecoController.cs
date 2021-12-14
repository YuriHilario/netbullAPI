using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Extensions;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Util;
using netbullAPI.ViewModels;
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
        /// <param name="neEndereco"></param>
        /// <param name="idPessoa"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{idPessoa}")]
        public async Task<IActionResult> GetAsync([FromServices] NE_Endereco neEndereco, int idPessoa)
        {
            IEnumerable<Endereco> listaEnderecos = await neEndereco.BuscaEnderecosPessoaAsync(idPessoa);

            if (!listaEnderecos.Any())
            {
                Notificar("Endereço não encontrado.");
                return NotFound(
                    new
                    {
                        status = HttpStatusCode.NotFound,
                        Error = Notificacoes()
                    }); ;
            }

            return Ok(new
            {
                status = HttpStatusCode.OK,
                Lista = listaEnderecos
            });
        }

        /// <summary>
        /// Inclusão de novo endereço do cliente.
        /// </summary>
        /// <param name="neEndereco"></param>
        /// <param name="endereco"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        // POST api/<EnderecoController>
        public async Task<IActionResult> CadastrarNovoEnderecoAsync([FromServices] NE_Endereco neEndereco, [FromBody] RegistrarEnderecoViewModel endereco)
        {
            try
            {
                if (endereco == null)
                {
                    Notificar("Endereço vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }

                var enderecoRetorno = await neEndereco.CadastraNovoEnderecoAsync(endereco);

                if (enderecoRetorno == false)
                {
                    return NotFound(
                       new
                       {
                           status = HttpStatusCode.NotFound,
                           Error = Notificacoes()
                       });
                }
                return Created($"/{endereco}", new
                {
                    message = "Inserido com sucesso",
                    endereco,
                });
            }
            catch (Exception ex)
            {
                Notificar("Falha ao cadastrar novo endereço.");
                return StatusCode(500, Notificacoes());
            }

        }

        /// <summary>
        /// Atualização de um endereço do cliente.
        /// </summary>
        /// <param name="neEndereco"></param>
        /// <param name="endereco"></param>
        /// <param name="idEndereco"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{idEndereco}")]
        // PUT api/<EnderecoController>
        public async Task<IActionResult> AtualizaEnderecoAsync([FromServices] NE_Endereco neEndereco, AlterarEnderecoViewModel endereco, int idEndereco)
        {
            try
            {
                if (endereco == null)
                {
                    Notificar("Endereço vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }

                var enderecoRetorno = await neEndereco.AtualizaEnderecoAsync(endereco, idEndereco);

                if (enderecoRetorno == false)
                {
                    return NotFound(
                       new
                       {
                           status = HttpStatusCode.NotFound,
                           Error = Notificacoes()
                       });
                }
                else
                {
                    return Ok(
                        new
                        {
                            message = "Atualizado com sucesso.",
                            endereco,
                        });
                }
            }
            catch (Exception ex)
            {
                Notificar("Falha ao atualizar usuário.");
                return StatusCode(500, Notificacoes());
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
        public async Task<IActionResult> AtualizaEnderecoPatchAsync([FromServices] NE_Endereco neEndereco, int idEndereco, [FromBody] AlterarEnderecoViewModel endereco)
        {
            try
            {
                if (idEndereco == 0)
                {
                    Notificar("Id do Endereço ou o logradouro está vazio.");
                    return NotFound(HttpStatusCode.NoContent);
                }

                return Ok(await neEndereco.AtualizaEnderecoPatchAsync(idEndereco, endereco)
                    ? new { mensagem = "Atualizado com sucesso.", sucesso = true }
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
        public async Task<IActionResult> ApagaEnderecoAsync([FromServices] NE_Endereco neEndereco, int idEndereco)
        {
            try
            {
                if (idEndereco == 0)
                    return NotFound(new { mensagem = "O idEndereço não foi informado.", sucesso = false });

                var resp = await neEndereco.ApagaEnderecoAsync(idEndereco);
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