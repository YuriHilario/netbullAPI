﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;
using netbullAPI.Util;
using System.Net;

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

        /// <summary>
        /// Busca lista de telefones atribuidas ao cliente informado
        /// </summary>
        /// <param name="ne_Telefone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetPorId([FromServices] NE_Telefone ne_Telefone, int id)
        {
            try
            {
                var telefones = ne_Telefone.BuscaTelefoneCliente(id);
                if(telefones == null)
                    return NotFound(
                            new
                            {
                                status = HttpStatusCode.NotFound,
                                Error = Notificacoes()
                            });
                else
                    return Ok(
                        new
                        {
                            telefones = ne_Telefone.BuscaTelefoneCliente(id),
                            status = HttpStatusCode.OK,
                            Error = Notificacoes()
                        });               
            }
            catch (Exception e)
            {
                return BadRequest(
                    new
                    {
                        status = HttpStatusCode.BadRequest,
                        Error = Notificacoes()
                    });
            }
        }

        /// <summary>
        /// Inclusão de novo telefone ao cliente
        /// </summary>
        /// <param name="ne_Telefone"></param>
        /// <param name="telefone"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromServices] NE_Telefone ne_Telefone, [FromBody] Telefone telefone)
        {
            try
            {
                var nvTelefone = ne_Telefone.AdicionaTelefone(telefone);
                if (nvTelefone != null)
                    return Created($"/{nvTelefone.telefone_id}", nvTelefone);
                else
                    return NotFound(
                            new
                            {
                                status = HttpStatusCode.BadRequest,
                                Error = Notificacoes()
                            });
            }
            catch (Exception e)
            {
                return BadRequest(
                new
                {
                    status = HttpStatusCode.BadRequest,
                    Error = Notificacoes()
                });
            }
        }

        /// <summary>
        /// Atualização do número de telefone do cliente
        /// </summary>
        /// <param name="ne_Telefone"></param>
        /// <param name="telefone"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromServices] NE_Telefone ne_Telefone,[FromBody] Telefone telefone)
        {
            try
            {
                if(ne_Telefone.AtualizaTelefone(telefone))
                    return Ok(
                            new
                            {
                                telefone = telefone,
                                status = HttpStatusCode.OK,
                                Error = Notificacoes()
                            });
                else
                    return NotFound(
                            new
                            {
                                status = HttpStatusCode.BadRequest,
                                Error = Notificacoes()
                            });

            }
            catch (Exception e)
            {
                return BadRequest(
                new
                {
                    status = HttpStatusCode.BadRequest,
                    Error = Notificacoes()
                });
            }
        }

        /// <summary>
        /// Remoção do telefone informado do cliente
        /// </summary>
        /// <param name="ne_Telefone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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
                            status = HttpStatusCode.OK,
                            Error = Notificacoes(),
                        });
                else
                    return NotFound(
                        new
                        {
                            status = HttpStatusCode.BadRequest,
                            Error = Notificacoes()
                        });
            }
            catch (Exception e)
            {
                return BadRequest(
                    new
                    {
                        status = HttpStatusCode.BadRequest,
                        Error = Notificacoes()
                    });
            }
            
        }
    }
}
