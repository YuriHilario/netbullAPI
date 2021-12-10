using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Util;
using System.Net;

namespace netbullAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : BaseController
    {
        public PedidoController(INotificador notificador) : base(notificador) { }


        /// <summary>
        /// Busca lista de telefones atribuidas ao cliente informado
        /// </summary>
        /// <param name="ne_pedido"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetPorId([FromServices] NE_Pedido ne_pedido, int id)
        {
            try
            {
                var pedidos = ne_pedido.BuscaPedidosCliente(id);
                if (pedidos == null)
                    return NotFound(new
                    {
                        status = HttpStatusCode.NotFound,
                        Error = Notificacoes()
                    });
                else return Ok(new
                {
                    pedidos = ne_pedido.BuscaPedidosCliente(id),
                    status = HttpStatusCode.OK,
                    Error = Notificacoes()
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    Error = Notificacoes()
                });
            }
        }

        /// <summary>
        /// Busca lista de telefones atribuidas ao cliente informado
        /// </summary>
        /// <param name="ne_pedido"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult post([FromServices] NE_Pedido ne_pedido, [FromBody] Pedido pedido)
        {
            try
            {
                var new_pedido = ne_pedido.AdicionaPedido(pedido);
                if (new_pedido == null)
                    return Created($"/{new_pedido.pedido_id}", new_pedido);
                else
                    return NotFound(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        Error = Notificacoes()
                    });
            }
            catch(Exception e)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    Error = Notificacoes()
                });
            }
        }

        /// <summary>
        /// Busca lista de telefones atribuidas ao cliente informado
        /// </summary>
        /// <param name="ne_pedido"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromServices] NE_Pedido ne_pedido, [FromBody] Pedido pedido)
        {
            try
            {
                if (ne_pedido.AlteraStatusPedido(pedido))
                    return Ok(new
                    {
                        pedido = pedido,
                        status = HttpStatusCode.OK,
                        Error = Notificacoes()
                    });
                else
                    return NotFound(new
                    {
                        status = HttpStatusCode.BadRequest,
                        Error = Notificacoes()
                    });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    Error = Notificacoes()
                });
            }
        }
    }
}
