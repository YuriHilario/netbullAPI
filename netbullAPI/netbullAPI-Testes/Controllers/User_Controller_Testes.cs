using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Interfaces;
using netbullAPI.Security.Controllers;
using netbullAPI.Security.Negocio;
using netbullAPI.Security.Persistencia;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace netbullAPI_Testes
{
    [TestClass]
    public class User_Controller_Testes
    {
        private readonly INotificador _notificador;
        private UserDAO _userDao;

        protected NE_User _neUser;
        protected IConfiguration _configuration;

        public User_Controller_Testes(INotificador notificador, IConfiguration configuration)
        {
            _notificador = notificador;
            _configuration = configuration;
            _userDao = new UserDAO(notificador,_configuration);
            _neUser = new NE_User(_userDao);
        }

        [TestMethod]
        [TestCategory("Controller")]
        public async Task getAllUsers()
        {
            ContaController _controller = new ContaController(_notificador);

            // Act
            var okResult = _controller.getAllUsers(_neUser);
            // Assert

            //Assert.AreNotEqual(0, okResult.Count);
        }
    }
}