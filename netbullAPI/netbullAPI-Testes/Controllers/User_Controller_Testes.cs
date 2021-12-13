using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Security.ViewModels;
using netbullAPI_Testes.Models;
using netbullAPI_Testes.Uitl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace netbullAPI_Testes
{
    [TestClass]
    public class User_Controller_Testes // TESTES DE INTEGRAÇÃO
    {
        [Fact]
        [TestCategory("Controller-Valido")]
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
        [TestCategory("Controller-Valido")]
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

        [Fact(Skip = "Teste ainda não disponível")] // implementar
        [TestCategory("Controller-Valido")]
        public async Task TestarRegisterValidoAsync()
        {
        }

        [Fact(Skip = "Teste ainda não disponível")] // implementar
        [TestCategory("Controller")]
        public async Task TestarRegisterInvalidoAsync()
        {
        }

        [Fact]
        [TestCategory("Controller-Valido")]
        public async Task TestarDeleteUserValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

                var _Client = application.CreateClient();

                LoginUserViewModel login = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                _Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var resultGetAll = _Client.GetAsync("api/Conta").GetAwaiter().GetResult();
                var resultContentGetall = resultGetAll.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                List<RetornarUserViewModel> listaUsus = JsonConvert.DeserializeObject<List<RetornarUserViewModel>>(resultContentGetall);

                RegistrarUserViewModel usuDelete = new RegistrarUserViewModel()
                {
                    user_nome = "cacacaca",
                    user_email = "caca@hotmail.com",
                    user_accessKey = "123456"
                };

               

                var usuExiste = listaUsus.Where(l => l.user_nome.Equals(usuDelete.user_nome)).FirstOrDefault();

                if (usuExiste != null)
                {
                    var result = _Client.DeleteAsync($"api/Conta/delete/{usuExiste.user_id}").GetAwaiter().GetResult();

                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
                else
                {
                    var jsonCorpo = JsonConvert.SerializeObject(usuDelete);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("https://localhost:7035/api/Conta/registrar"),
                        Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                    };

                    var response = await _Client.SendAsync(request).ConfigureAwait(false);

                    resultGetAll = _Client.GetAsync("api/Conta").GetAwaiter().GetResult();
                    resultContentGetall = resultGetAll.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    listaUsus = JsonConvert.DeserializeObject<List<RetornarUserViewModel>>(resultContentGetall);
                    usuExiste = listaUsus.Where(l => l.user_nome.Equals(usuDelete.user_nome)).FirstOrDefault();

                    var result = _Client.DeleteAsync($"api/Conta/delete/{usuExiste.user_id}").GetAwaiter().GetResult();

                    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        [Fact(Skip = "Teste ainda não disponível")] // implementar
        [TestCategory("Controller")]
        public async Task TestarDeleteUserInvalidoAsync()
        {
        }

        [Fact(Skip = "Teste ainda não disponível")] // implementar
        [TestCategory("Controller-Valido")]
        public async Task TestarAlterarSenhaValidoAsync()
        {
        }

        [Fact(Skip = "Teste ainda não disponível")] // implementar
        [TestCategory("Controller")]
        public async Task TestarAlterarSenhaInvalidoAsync()
        {
        }
    }
}