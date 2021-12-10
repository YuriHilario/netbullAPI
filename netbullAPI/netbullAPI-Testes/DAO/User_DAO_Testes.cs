using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Interfaces;
using netbullAPI.Security.Controllers;
using netbullAPI.Security.Negocio;
using netbullAPI.Security.Persistencia;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace netbullAPI_Testes.DAO
{
    [TestClass]
    public class User_DAO_Testes
    {
        private readonly INotificador _notificador;
        private readonly IConfiguration _configuration;
        public User_DAO_Testes(INotificador notificador, IConfiguration configuration)
        {
            _notificador = notificador;
            _configuration = configuration; 
        }

        [TestMethod]
        [TestCategory("Controller")]
        public async Task getAllUsers()
        {
            UserDAO _dao = new UserDAO(_notificador, _configuration);

            // Act
            //var okResult = _dao.();
            // Assert

            //Assert.AreNotEqual(0, okResult.Count);
        }
    }
}