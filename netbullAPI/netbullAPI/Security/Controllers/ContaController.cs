using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Security.Models;
using netbullAPI.Security.Negocio;
using netbullAPI.Security.Service;

namespace netbullAPI.Security.Controllers
{
    [ApiController]
    public class ContaController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("v1/registar")]
        public IActionResult Register([FromServices] NE_User neUser, User user)
        {
            user = neUser.CadastroDeUser(user);

            if (user.user_id == 0)
            {
                return BadRequest();
            }
            return Created($"/{user.user_id}", user);
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
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
