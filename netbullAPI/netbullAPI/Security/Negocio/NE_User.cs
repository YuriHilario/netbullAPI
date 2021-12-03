using netbullAPI.Security.Models;
using netbullAPI.Security.Persistencia;
using netbullAPI.Security.Util;

namespace netbullAPI.Security.Negocio
{
    public class NE_User
    {
        private UserDAO daoUser;

        public NE_User()
        {
            daoUser = new UserDAO();
        }
        internal User CadastroDeUser(User usu)
        {
            usu.user_accessKey = Utilitarios.HashValue(usu.user_accessKey);

            usu = daoUser.CadastroDeUser(usu);

            return usu; 
        }

        public User RecuperarUsuario(User usu)
        {
            usu = daoUser.RecuperarUsuario(usu);
            return usu;
        }

        public User VerificarUsuarioSenha(User usu, out bool usuarioSenhaOK)
        {
            usu.user_accessKey = Utilitarios.HashValue(usu.user_accessKey);

            var usuConsulta = daoUser.RecuperarUsuario(usu);
            if (usuConsulta.user_accessKey == usu.user_accessKey)
                usuarioSenhaOK = true;
            else
                usuarioSenhaOK = false;

            return usuConsulta;

        }

    }
}
