using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Security.ViewModels;
using netbullAPI_Testes.Models;
using netbullAPI_Testes.Uitl;
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
        public async Task TestarLoginValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var _Client = application.CreateClient();

                RequestLoginTeste requestLoginTeste = new RequestLoginTeste();

                LoginUserViewModel usu = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var RetornoLogin = await requestLoginTeste.RetornaUsuLoginAsync(usu);

                if (RetornoLogin.status != HttpStatusCode.OK)
                {
                    Assert.Fail();
                }
                var teste = !string.IsNullOrEmpty(RetornoLogin.Token);

                Assert.AreEqual(teste, true);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        [Fact]
        [TestCategory("Controller")]
        public async Task TestarLoginInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

                var _Client = application.CreateClient();

                RequestLoginTeste requestLoginTeste = new RequestLoginTeste();

                LoginUserViewModel usu = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var RetornoLogin = await requestLoginTeste.RetornaUsuLoginAsync(usu);

                if (RetornoLogin.status != HttpStatusCode.OK)
                {
                    Assert.Fail();
                }
                var teste = string.IsNullOrEmpty(RetornoLogin.Token);

                Assert.AreEqual(teste, true);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        [Fact]
        [TestCategory("Controller")]
        public async Task TesteGetAllUserValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var _Client = application.CreateClient();

                var result = _Client.GetAsync("api/Conta").GetAwaiter().GetResult();
                var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    Assert.Fail();
                }

                List<RetornarUserViewModel> lista = JsonConvert.DeserializeObject<List<RetornarUserViewModel>>(resultContent);
                Assert.AreNotEqual(0, lista.Count);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        [Fact]
        [TestCategory("Controller")]
        public async Task TesteGetAllUserInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

                var _Client = application.CreateClient();

                var result = _Client.GetAsync("api/Conta").GetAwaiter().GetResult();
                var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (result.StatusCode != HttpStatusCode.BadRequest)
                {
                    Assert.Fail();
                }

                List<RetornarUserViewModel> lista = JsonConvert.DeserializeObject<List<RetornarUserViewModel>>(resultContent);
                Assert.AreEqual(0, lista.Count);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
    }
}