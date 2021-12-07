using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Security.Models;
using netbullAPI.Security.Persistencia;
using netbullAPI.Util;
using System.Data;

namespace netbullAPI.Security.Negocio
{
    public class NE_User : NEBase

    {
        private UserDAO _userDao;

        public NE_User(INotificador notificador, UserDAO userDao) : base(notificador)
        {
            _userDao = userDao;
        }
        internal User CadastroDeUser(User usu)
        {
            //UserDAO _userDAO = new UserDAO();
            usu.user_accessKey = Criptografia.HashValue(usu.user_accessKey);

            usu = _userDao.CadastroDeUser(usu);

            return usu;
        }

        public User RecuperarUsuario(User usu)
        {
            usu = _userDao.RecuperarUsuario(usu);
            return usu;
        }

        public User VerificarUsuarioSenha(User usu, out bool usuarioSenhaOK)
        {
            var usuConsulta = _userDao.RecuperarUsuario(usu);

                if (usuConsulta == null)
                {
                    usuarioSenhaOK = false;
                    Notificar("Usuário não existe");
                    return usuConsulta;
                }
                else
                {
                    usu.user_accessKey = Criptografia.HashValue(usu.user_accessKey);

                    if (usuConsulta.user_accessKey == usu.user_accessKey)
                        usuarioSenhaOK = true;
                    else
                        usuarioSenhaOK = false;

                    return usuConsulta;
                }
            
        }

    }
}
