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
    public class TelefoneController : BaseController
    {
        public TelefoneController(INotificador notificador) : base(notificador)
        {
        }
        
        // GET api/<TelefoneController>/5
        [HttpGet("{id}")]
        public IActionResult GetPorId([FromServices] NE_Telefone ne_Telefone, int id)
        {
            try
            {
                return Ok(ne_Telefone.BuscaTelefoneCliente(id));
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
        public IActionResult Post([FromServices] NE_Telefone ne_Telefone, [FromBody] Telefone telefone)
        {
            try
            {
                return Ok(ne_Telefone.AdicionaTelefone(telefone)) ;
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
        public IActionResult Put([FromServices] NE_Telefone ne_Telefone,[FromBody] Telefone telefone)
        {
            try
            {
                return Ok(ne_Telefone.AtualizaTelefone(telefone));

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
        public IActionResult Delete([FromServices] NE_Telefone ne_Telefone,int id)
        {
            try
            {
                var resp = ne_Telefone.DeletaTelefone(id);
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
