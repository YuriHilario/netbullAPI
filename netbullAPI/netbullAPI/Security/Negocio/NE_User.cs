using netbullAPI.Interfaces;
using netbullAPI.Security.Models;
using netbullAPI.Security.Persistencia;
using netbullAPI.Security.ViewModels;
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

        public async Task <List<RetornarUserViewModel>> getAllUsers()
        {
            return await _userDao.getAllUsers();
        }

        internal async Task<User> CadastroDeUser(User usu)
        {
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

            var verificado = await _userDao.VerificarUsuarioSenha(usu);
            
            return verificado;
        }

        internal async Task<bool> DeleteUser(int id)
        {
            return await _userDao.DeleteUser(id);
        }

        internal async Task<bool> alterarSenha(User usu)
        {
            usu.user_accessKey = Criptografia.HashValue(usu.user_accessKey);
            return await _userDao.alterarSenha(usu);
        }
    }
}
