using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Entidade;
using netbullAPI.Persistencia;
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

namespace netbullAPI_Testes
{
    [TestClass]
    public class Endereco_Controller_Testes
    {
        public LoginUserViewModel login = new LoginUserViewModel() { user_nome = "ale", user_accessKey = "123456" };

        /// <summary>
        /// Teste integração de busca de endereços para um cliente válido
        /// responseEndereco.StatusCode == HttpStatusCode.OK
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Controller", "Válido")]
        public async Task TestarEnderecoGetPorIdPessoaValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                var requestEndereco = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://localhost:7035/api/Endereco/{1}"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responseEndereco = await httpClient.SendAsync(requestEndereco).ConfigureAwait(false);
                var responseBodytelefone = await responseEndereco.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseEndereco.StatusCode != HttpStatusCode.OK)
                    Assert.Fail();

                Assert.AreEqual(HttpStatusCode.OK, responseEndereco.StatusCode);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste integração de busca de endereços para um cliente Inválido
        /// responseEndereco.StatusCode == HttpStatusCode.NotFound
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Controller", "Inválido")]
        public async Task TestarEnderecoGetPorIdPessoaInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                var requestEndereco = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://localhost:7035/api/Endereco/{0}"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responseEndereco = await httpClient.SendAsync(requestEndereco).ConfigureAwait(false);
                var responseBodytelefone = await responseEndereco.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseEndereco.StatusCode != HttpStatusCode.NotFound)
                    Assert.Fail();

                Assert.AreEqual(HttpStatusCode.NotFound, responseEndereco.StatusCode);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

        /// <summary>
        /// Teste integração de cadastro de endereço válido
        /// responseEndereco.StatusCode == HttpStatusCode.Created
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Controller", "Válido")]
        public async Task TestarCadastrarNovoEnderecoValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = config.GetConnectionString("NetBullConnection");

                RegistrarEnderecoViewModel endereco = new RegistrarEnderecoViewModel()
                {
                    endereco_logradouro = "LOGRADOUTO_TESTE",
                    endereco_numero = 111,
                    endereco_complemento = "CACA",
                    endereco_idpessoa = 1
                };

                var jsonCorpo = JsonConvert.SerializeObject(endereco);

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                var requestEndereco = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://localhost:7035/api/Endereco"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responseEndereco = await httpClient.SendAsync(requestEndereco).ConfigureAwait(false);
                var responseBodytelefone = await responseEndereco.Content.ReadAsStringAsync().ConfigureAwait(false);

                // DELETANDO TESTE CRIADO
                var usuCreated = JsonConvert.DeserializeObject<Endereco>(responseBodytelefone);

                var contextOptions = new DbContextOptionsBuilder<netbullDBContext>()
                                .UseNpgsql(connectionString)
                                .Options;

                using var context = new netbullDBContext(contextOptions);

                var enderecoCriado = context.Enderecos.Where(end => end.endereco_logradouro == endereco.endereco_logradouro &&
                                                                              end.endereco_numero == endereco.endereco_numero &&
                                                                              end.endereco_complemento == endereco.endereco_complemento &&
                                                                              end.endereco_idpessoa == endereco.endereco_idpessoa).FirstOrDefault();

                usuCreated.endereco_id = enderecoCriado.endereco_id;

                var result = httpClient.DeleteAsync($"api/Endereco/{usuCreated.endereco_id}").GetAwaiter().GetResult();

                if (responseEndereco.StatusCode != HttpStatusCode.Created)
                    Assert.Fail();

                Assert.AreEqual(HttpStatusCode.Created, responseEndereco.StatusCode);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }


        /// <summary>
        /// Teste integração de cadastro de endereço Inválido
        /// responseEndereco.StatusCode == HttpStatusCode.BadRequest
        /// </summary>
        /// <returns></returns>
        [Fact]
        [Trait("Controller", "Inválido")]
        public async Task TestarCadastrarNovoEnderecoInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                RegistrarEnderecoViewModel endereco = new RegistrarEnderecoViewModel()
                {
                    endereco_logradouro = "",
                    endereco_numero = 0,
                    endereco_complemento = "",
                    endereco_idpessoa = 0
                };

                var jsonCorpo = JsonConvert.SerializeObject(endereco);

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                var requestEndereco = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://localhost:7035/api/Endereco"),
                    Content = new StringContent(jsonCorpo, Encoding.UTF8, "application/json"),
                };

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responseEndereco = await httpClient.SendAsync(requestEndereco).ConfigureAwait(false);
                var responseBodytelefone = await responseEndereco.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseEndereco.StatusCode != HttpStatusCode.BadRequest)
                    Assert.Fail();

                Assert.AreEqual(HttpStatusCode.BadRequest, responseEndereco.StatusCode);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }

    }
}