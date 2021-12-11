using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Entidade;
using netbullAPI.Security.ViewModels;
using netbullAPI_Testes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public class Telefone_Controller_Testes
    {
        /// <summary>
        /// Teste integração de busca de telefones para um cliente inválido
        /// id_pessoa_invalido = 0
        /// </summary>
        /// <returns></returns>
        [Fact]
        [TestCategory("Controller")]
        public async Task TestarTelefoneByClienteInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                LoginUserViewModel usu = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var jsonCorpo = JsonConvert.SerializeObject(usu);
                //Região de login para pegar o token
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Conta/login"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };
                var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoLogin = JsonConvert.DeserializeObject<RetornoLogin>(responseBody);

                // Requisição para pegar os telefones
                var requestTelefone = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://localhost:7035/api/Telefone/{0}"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retornoLogin.Token);

                var responseTelefone = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBodytelefone = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoTelefone = JsonConvert.DeserializeObject<RetornoNotFound>(responseBodytelefone);

                if (response.StatusCode != HttpStatusCode.NotFound)
                    Assert.Fail();
                

                Assert.AreNotEqual(0, retornoTelefone.Erros.Count); 
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste integração de busca de telefones para um cliente válido
        /// id_pessoa_valido = 1
        /// </summary>
        /// <returns></returns>
        [Fact]
        [TestCategory("Controller")]
        public async Task TestarTelefoneByClienteValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                LoginUserViewModel usu = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var jsonCorpo = JsonConvert.SerializeObject(usu);
                //Região de login para pegar o token
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Conta/login"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };
                var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoLogin = JsonConvert.DeserializeObject<RetornoLogin>(responseBody);

                // Requisição para pegar os telefones
                var requestTelefone = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://localhost:7035/api/Telefone/{1}"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retornoLogin.Token);

                var responseTelefone = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBodytelefone = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoTelefone = JsonConvert.DeserializeObject<RetornoGetTelefone>(responseBodytelefone);

                if (response.StatusCode == HttpStatusCode.NotFound)
                    Assert.Fail();

                Assert.AreNotEqual(0, retornoTelefone);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste intregração de post de novos telefones
        /// telefone_invalido = 0
        /// telefone_invalido = 12345
        /// id_pessoa_invalido = 0
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestCategory("Controller")]
        [DataRow(0,1)]
        [DataRow(12345, 1)]
        [DataRow(123456789, 0)]
        public async Task TestarPostInvalidoAsync(int num_telefone, int pessoaId)
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                LoginUserViewModel usu = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var jsonCorpo = JsonConvert.SerializeObject(usu);
                //Região de login para pegar o token
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Conta/login"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };
                var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoLogin = JsonConvert.DeserializeObject<RetornoLogin>(responseBody);

                // Requisição para pegar os telefones

                var jsonTelefone = JsonConvert.SerializeObject(new Telefone() {
                                                                 telefone_idPessoa = pessoaId,
                                                                 telefone_numero = num_telefone,
                                                               });

                var requestTelefone = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://localhost:7035/api/Telefone/"),
                    Content = new StringContent(jsonTelefone, Encoding.UTF8, "application/json"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retornoLogin.Token);

                var responseTelefone = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBodytelefone = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoTelefone = JsonConvert.DeserializeObject<RetornoNotFound>(responseBodytelefone);

                if (response.StatusCode != HttpStatusCode.NotFound && response.StatusCode != HttpStatusCode.BadRequest)
                    Assert.Fail();

                Assert.AreNotEqual(0, retornoTelefone.Erros.Count);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste de integração deleção de telefone inválido
        /// id_telefone_invalido
        /// </summary>
        /// <returns></returns>
        [Fact]
        [TestCategory("Controller")]
        public async Task TestarDeleteInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                LoginUserViewModel usu = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var jsonCorpo = JsonConvert.SerializeObject(usu);
                //Região de login para pegar o token
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Conta/login"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };
                var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoLogin = JsonConvert.DeserializeObject<RetornoLogin>(responseBody);

                // Requisição para pegar os telefones
                var requestTelefone = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"https://localhost:7035/api/Telefone/{0}"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retornoLogin.Token);

                var responseTelefone = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBodytelefone = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoTelefone = JsonConvert.DeserializeObject<RetornoNotFound>(responseBodytelefone);

                if (response.StatusCode != HttpStatusCode.NotFound)
                    Assert.Fail();


                Assert.AreNotEqual(0, retornoTelefone.Erros.Count);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste de integração deleção de telefone válido
        /// id_telefone_valido
        /// </summary>
        /// <returns></returns>
        [Fact]
        [TestCategory("Controller")]
        public async Task TestarDeleteValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                LoginUserViewModel usu = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var jsonCorpo = JsonConvert.SerializeObject(usu);
                //Região de login para pegar o token
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Conta/login"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };
                var response = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoLogin = JsonConvert.DeserializeObject<RetornoLogin>(responseBody);

                // Requisição para pegar os telefones
                var requestTelefone = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"https://localhost:7035/api/Telefone/{0}"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", retornoLogin.Token);

                var responseTelefone = await httpClient.SendAsync(request).ConfigureAwait(false);
                var responseBodytelefone = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retornoTelefone = JsonConvert.DeserializeObject<RetornoNotFound>(responseBodytelefone);

                if (response.StatusCode != HttpStatusCode.NotFound)
                    Assert.Fail();


                Assert.AreNotEqual(0, retornoTelefone.Erros.Count);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
    }
}