using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;
using netbullAPI.Util;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace netbullAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : BaseController
    {
        public ProdutoController(INotificador notificador) : base(notificador)
        {
        }

        // GET api/<TelefoneController>/5
        [HttpGet("{id}")]
        public IActionResult GetPorId([FromServices] NE_Produto ne_Produto, int id)
        {
            try
            {
                return Ok(ne_Produto.BuscaProdutoPorId(id));
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

        [HttpPost]
        public IActionResult Post([FromServices] NE_Produto ne_Produto, [FromBody] Produto produto)
        {
            try
            {
                return Ok(ne_Produto.AdicionaProduto());
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

        [HttpPut]
        public IActionResult Put([FromServices] NE_Produto ne_Produto, [FromBody] Produto produto)
        {
            try
            {
                return Ok(ne_Produto.AtualizaProduto(produto));

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


        [HttpDelete("{id}")]
        public IActionResult Delete([FromServices] NE_Produto ne_Produto, int id)
        {
            try
            {
                var resp = ne_Produto.DeletaProduto(id);
                if (resp)
                    return Ok(
                        new
                        {
                            mensagem = "Registro deletado com sucesso",
                            sucesso = true
                        });
                else
                    return BadRequest(
                        new
                        {
                            mensagem = "Não foi possível deletar registro",
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
    }
}
