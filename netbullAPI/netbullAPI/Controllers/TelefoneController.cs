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
        private NE_Telefone NE_Telefone;
        public TelefoneController(INotificador notificador, netbullDBContext netbullDBContext) : base(notificador)
        {
            NE_Telefone = new NE_Telefone(netbullDBContext);
        }
        
        // GET api/<TelefoneController>/5
        [HttpGet("{id}")]
        public IActionResult GetPorId(int id)
        {
            try
            {
                return Ok(NE_Telefone.BuscaTelefoneCliente(id));
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
        public IActionResult Post([FromBody] Telefone telefone)
        {
            try
            {
                return Ok(NE_Telefone.AdicionaTelefone(telefone)) ;
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
        public IActionResult Put([FromBody] Telefone telefone)
        {
            try
            {
                return Ok(NE_Telefone.AtualizaTelefone(telefone));

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
        public IActionResult Delete(int id)
        {
            try
            {
                var resp = NE_Telefone.DeletaTelefone(id);
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
