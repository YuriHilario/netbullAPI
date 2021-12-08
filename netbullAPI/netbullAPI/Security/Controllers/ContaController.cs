using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Interfaces;
using netbullAPI.Security.Models;
using netbullAPI.Security.Negocio;
using netbullAPI.Security.Service;
using netbullAPI.Security.ViewModels;
using netbullAPI.Util;
using System.Net;

namespace netbullAPI.Security.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : BaseController
    {
        public ContaController(INotificador notificador) : base(notificador) { }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> getAllUsers([FromServices] NE_User neUser)
        {
            try
            {
                var listaUsu = await neUser.getAllUsers();
                if (listaUsu == null)
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
                    return Ok(listaUsu);
                }
            }
            catch(Exception ex)
            {
                Notificar(ex.Message);
                return BadRequest(Notificacoes());
            }
           
        }

        [AllowAnonymous]
        [HttpPost("v1/registrar")]
        public async Task<IActionResult> Register([FromServices] NE_User neUser, 
                                      [FromBody] RegistrarUserViewModel viewModel)
        {
            try
            {
                User usu = new User()
                {
                    user_id = 0,
                    user_nome = viewModel.user_nome,
                    user_email = viewModel.user_email,
                    user_accessKey = viewModel.user_accessKey
                };

                usu = await neUser.CadastroDeUser(usu);
                if (usu.user_id == 0)
                {
                    return BadRequest(
                       new
                       {
                           status = HttpStatusCode.BadRequest,
                           Error = Notificacoes()
                       });
                }
                else
                {
                    return Created($"/{usu.user_id}", usu);
                }
            }
            catch(Exception ex)
            {
                Notificar(ex.Message);
                return BadRequest(Notificacoes());
            }
            
        }

        [AllowAnonymous]
        [HttpPost("v1/login")]
        public async Task<IActionResult> Login([FromServices] TokenService _tokenService,
                                  [FromServices] NE_User neUsuario,
                                  [FromBody] LoginUserViewModel viewModel)
        {
            try
            {
                User usu = new User()
                {
                    user_id = 0,
                    user_nome = viewModel.user_nome,
                    user_email = null,
                    user_accessKey = viewModel.user_accessKey
                };

                var usuConsulta = await neUsuario.VerificarUsuarioSenha(usu);

                if (usuConsulta != null)
                {
                    var token = await _tokenService.GenerateToken(usuConsulta);
                    return Ok(
                        new
                        {
                            status = HttpStatusCode.OK,
                            Token = token
                        });
                }
                else
                {
                    return Unauthorized(
                        new
                        {
                            status = HttpStatusCode.Unauthorized,
                            Error = Notificacoes()
                        });
                }
            }
            catch(Exception ex)
            {
                Notificar(ex.Message);
                return BadRequest(Notificacoes());
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromServices] NE_User neUser, int id)
        {
            try
            {
                var sucess = await neUser.DeleteUser(id);

                if (!sucess)
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
                    return Ok();
                }
            }
            catch(Exception ex)
            {
                Notificar(ex.Message);
                return BadRequest(Notificacoes());
            }       
        }
    }
}
