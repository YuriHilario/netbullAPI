using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Interfaces;
using netbullAPI.Security.Controllers;
using netbullAPI.Security.Models;
using netbullAPI.Security.Negocio;
using netbullAPI.Security.Persistencia;
using netbullAPI.Security.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace netbullAPI_Testes
{
    [TestClass]
    public class User_Controller_Testes
    {
        [Fact]
        [TestCategory("Controller")]
        public async Task TestarLoginInvalidoAsync()
        {
            var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

            var _Client = application.CreateClient();

            LoginUserViewModel usu = new LoginUserViewModel()
            {
                user_nome = "caca",
                user_accessKey = "123456"
            };

            var jsonCorpo = JsonConvert.SerializeObject(usu);
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7286/api/Aluno"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };

                var response = await _Client.SendAsync(request).ConfigureAwait(false);
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.NotFound)
                {
                    Assert.Fail();
                }
                //LoginUserViewModel usuario = JsonConvert.DeserializeObject<LoginUserViewModel>(resultContent);
                Assert.AreEqual("", responseBody); // TEM RETORNAR TOKEN
            }
            catch (Exception ex)
            {

            }
        }

        [Fact]
        [TestCategory("Controller")]
        public async Task TesteGetAllUserValidoAsync()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>{});


            var _Client = application.CreateClient();

            var result = _Client.GetAsync("/Conta").GetAwaiter().GetResult();
            var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (result.StatusCode != HttpStatusCode.OK)
            {
                Assert.Fail();
            }

            List<RetornarUserViewModel> lista = JsonConvert.DeserializeObject<List<RetornarUserViewModel>>(resultContent);
            Assert.AreNotEqual(0, lista.Count);
        }
    }
}