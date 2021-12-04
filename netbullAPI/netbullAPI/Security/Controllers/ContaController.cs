using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Interfaces;
using netbullAPI.Security.Models;
using netbullAPI.Security.Negocio;
using netbullAPI.Security.Service;
using netbullAPI.Util;
using System.Net;

namespace netbullAPI.Security.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : BaseController
    {
        public ContaController(INotificador notificador) : base(notificador) {}

        [AllowAnonymous]
        [HttpPost("v1/registrar")]
        public IActionResult Register([FromServices] NE_User neUser, [FromBody]User user)
        {
            user = neUser.CadastroDeUser(user);
            if (user.user_id == 0)
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
                return Created($"/{user.user_id}", user);
            }
        }

        [AllowAnonymous]
        [HttpPost("v1/login")]
        public IActionResult Login([FromServices] TokenService _tokenService,
                                  [FromServices] NE_User neUsuario,
                                  [FromBody] User usu)
        {
            bool loginSenhaOK;

            var usuConsulta = neUsuario.VerificarUsuarioSenha(usu, out loginSenhaOK);

            if (loginSenhaOK)
            {
                var token = _tokenService.GenerateToken(usuConsulta);
                return Ok (
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
    }
}
