using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Interfaces;
using netbullAPI.Security.Controllers;
using netbullAPI.Security.Models;
using netbullAPI.Security.Negocio;
using netbullAPI.Security.Persistencia;
using netbullAPI.Util;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace netbullAPI_Testes.DAO
{
    [TestClass]
    public class User_DAO_Testes 
    {
        private UserDAO _userDao;
        public User_DAO_Testes(UserDAO userDao) 
        {
            _userDao = userDao;
        }

        [Fact]
        [TestCategory("DAO")]
        public async Task Testar()
        {
            User usu = new User ()
            {
                user_nome = "caca",
                user_email = "cassiano@cassiano.com",
                user_accessKey ="123456"
            };

            usu = await _userDao.CadastroDeUser(usu);

            Assert.AreNotEqual(0, usu.user_id) ;
        }
    }
}