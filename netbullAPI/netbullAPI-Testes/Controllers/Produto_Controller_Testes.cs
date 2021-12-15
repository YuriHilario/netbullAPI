using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Entidade;
using netbullAPI.Security.ViewModels;
using netbullAPI.ViewModels;
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

namespace netbullAPI_Testes.Controllers
{
    [TestClass]
    public class Produto_Controller_Testes // TESTES DE INTEGRAÇÃO
    {
        /// <summary>
        /// Teste integração para buscar todos produtos para teste válido
        /// result.StatusCode == HttpStatusCode.OK && produtos.Count != 0
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Controller", "Válido")]
        public async Task TesteGetAllUserValidoAsync()
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

                var result = _Client.GetAsync("api/Produto").GetAwaiter().GetResult();
                var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(resultContent);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    Assert.Fail();
                }

                Assert.AreNotEqual(0, produtos.Count);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste integração para buscar produto por id para Válido
        /// result.StatusCode == HttpStatusCode.OK && produto != null
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Controller", "Válido")]
        public async Task TesteGetPorIdVálidoAsync()
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

                var result = _Client.GetAsync($"api/Produto/{1}").GetAwaiter().GetResult();
                var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                Produto produto = JsonConvert.DeserializeObject<Produto>(resultContent);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    Assert.Fail();
                }

                Assert.AreNotEqual(null, produto);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste integração para buscar produto por id para Inválido
        /// result.StatusCode == HttpStatusCode.NotFound && produto.error.Count == 0
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Controller", "Inválido")]
        public async Task TesteGetPorIdInválidoAsync()
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

                var result = _Client.GetAsync($"api/Produto/{0}").GetAwaiter().GetResult();
                var resultContent = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                RetornoNotFound produto = JsonConvert.DeserializeObject<RetornoNotFound>(resultContent);

                if (result.StatusCode != HttpStatusCode.NotFound)
                {
                    Assert.Fail();
                }

                Assert.AreNotEqual(0, produto.error.Count);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste integração para cadastrar um produto para válido
        /// result.StatusCode == HttpStatusCode.Created
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Controller", "Válido")]
        public async Task TestePostVálidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
                 .WithWebHostBuilder(builder => { });

                var _Client = application.CreateClient();

                RegistrarProdutoViewModel postProduto = new RegistrarProdutoViewModel()
                {
                    produto_nome = "camisa produto teste",
                    produto_valor = 10
                };

                var jsonCorpo = JsonConvert.SerializeObject(postProduto);

                LoginUserViewModel login = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                _Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Produto"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };

                var response = await _Client.SendAsync(request).ConfigureAwait(false);
                var resultContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (response.StatusCode != HttpStatusCode.Created)
                {
                    Assert.Fail();
                }

                // DELETANDO TESTE CRIADO
                var prodCreated = JsonConvert.DeserializeObject<Produto>(resultContent);
                var result = _Client.DeleteAsync($"api/Produto/{prodCreated.produto_id}").GetAwaiter().GetResult();
       
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste integração para cadastrar um produto para Inválido
        /// result.StatusCode == HttpStatusCode.BadRequest
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Controller", "Inválido")]
        public async Task TestePostInválidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
                 .WithWebHostBuilder(builder => { });

                var _Client = application.CreateClient();

                RegistrarProdutoViewModel postProduto = new RegistrarProdutoViewModel()
                {
                    produto_nome = "",
                    produto_valor = 10
                };

                var jsonCorpo = JsonConvert.SerializeObject(postProduto);

                LoginUserViewModel login = new LoginUserViewModel()
                {
                    user_nome = "cassiano",
                    user_accessKey = "123456"
                };

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                _Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7035/api/Produto"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };

                var response = await _Client.SendAsync(request).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.BadRequest)
                {
                    Assert.Fail();
                }

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
    }
}
