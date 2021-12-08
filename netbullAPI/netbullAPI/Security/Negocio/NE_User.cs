using netbullAPI.Interfaces;
using netbullAPI.Security.Models;
using netbullAPI.Security.Persistencia;
using netbullAPI.Util;
using System.Data;

namespace netbullAPI.Security.Negocio
{
    public class NE_User
    {
        private UserDAO _userDao;

        public NE_User(UserDAO userDao)
        {
            _userDao = userDao;
        }

        public async Task <List<User>> getAllUsers()
        {
            return await _userDao.getAllUsers();
        }

        internal async Task<User> CadastroDeUser(User usu)
        {
            //UserDAO _userDAO = new UserDAO();
            usu.user_accessKey = Criptografia.HashValue(usu.user_accessKey);

            usu = await _userDao.CadastroDeUser(usu);

            return usu;
        }

        public async Task<User> RecuperarUsuario(User usu)
        {
            usu =  await _userDao.RecuperarUsuario(usu);
            return usu;
        }

        public async Task<User> VerificarUsuarioSenha(User usu)
        {
            usu.user_accessKey = Criptografia.HashValue(usu.user_accessKey);

            var usuConsulta =  await _userDao.RecuperarUsuario(usu);
            if(usuConsulta != null)
            {
                if (usuConsulta.user_accessKey == usu.user_accessKey)
                {
                    return usuConsulta;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        internal async Task<bool> DeleteUser(int id)
        {
            return await _userDao.DeleteUser(id);
        }
    }
}
