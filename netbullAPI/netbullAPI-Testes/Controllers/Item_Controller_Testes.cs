using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netbullAPI.Entidade;
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

namespace netbullAPI_Testes.Controllers
{
    public class Item_Controller_Testes
    {
        public LoginUserViewModel login = new LoginUserViewModel() { user_nome = "cassiano", user_accessKey = "123456" };

        [Fact]
        [Trait("Controller", "Válido")]
        public async Task TestarGetItemByIdValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);
                                
                var requestItem = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://localhost:7035/api/Item/{1}"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responseItem = await httpClient.SendAsync(requestItem).ConfigureAwait(false);


                if (responseItem.StatusCode == HttpStatusCode.NotFound)
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.AreEqual(responseItem.StatusCode, HttpStatusCode.OK);
                }

            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
        [Fact]
        [Trait("Controller", "Inválido")]
        public async Task TestarGetItemByIdInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);
                                
                var requestItem = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://localhost:7035/api/Item/{0}"), //Falta trocar request
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responseItem = await httpClient.SendAsync(requestItem).ConfigureAwait(false);


                if (responseItem.StatusCode != HttpStatusCode.NotFound)
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.AreEqual(responseItem.StatusCode, HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
        [Fact]
        [Trait("Controller", "Válido")] //Falta debugar
        public async Task TestarPostItemValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                var jsonItem = JsonConvert.SerializeObject(new
                {
                    item_id = 0,
                    item_valor = 0,
                    item_qtdproduto = 0,
                    item_idPedido = 0,
                    item_idProduto = 0,
                });

                var requestItem = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://localhost:7035/api/Item"),
                    Content = new StringContent(jsonItem, Encoding.UTF8, "application/json"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responseItem = await httpClient.SendAsync(requestItem).ConfigureAwait(false);

                if (responseItem.StatusCode != HttpStatusCode.OK)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(HttpStatusCode.OK, responseItem.StatusCode);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
        [Fact]
        [Trait("Controller", "Inválido")] //Falta debugar
        public async Task TestarPostItemInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                var jsonItem = JsonConvert.SerializeObject(new
                {
                    item_id = 0,
                    item_valor = 0,
                    item_qtdproduto = 0,
                    item_idPedido = 0,
                    item_idProduto = 0,
                });

                var requestPessoa = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://localhost:7035/api/Item"),
                    Content = new StringContent(jsonItem, Encoding.UTF8, "application/json"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responsePessoa = await httpClient.SendAsync(requestPessoa).ConfigureAwait(false);

                if (responsePessoa.StatusCode != HttpStatusCode.BadRequest)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(HttpStatusCode.BadRequest, responsePessoa.StatusCode);
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
        [Fact]
        [Trait("Controller", "Válido")]
        public async Task TestarPutItemValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                
                var jsonItem = JsonConvert.SerializeObject(new
                {
                    item_id = 0,
                    item_valor = 0,
                    item_qtdproduto = 0,
                    item_idPedido = 0,
                    item_idProduto = 0,
                });

                var requestPessoa = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://localhost:7035/api/Pessoa"),
                    Content = new StringContent(jsonItem, Encoding.UTF8, "application/json"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responsePessoa = await httpClient.SendAsync(requestPessoa).ConfigureAwait(false);


                if (responsePessoa.StatusCode == HttpStatusCode.NotFound)
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.AreEqual(HttpStatusCode.OK, responsePessoa.StatusCode);
                }
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
        [Fact]
        [Trait("Controller", "Inválido")]
        public async Task TestarPutItemInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                var jsonItem = JsonConvert.SerializeObject(new
                {
                    item_id = 0,
                    item_valor = 0,
                    item_qtdproduto = 0,
                    item_idPedido = 0,
                    item_idProduto = 0,
                });

                var requestPessoa = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://localhost:7035/api/Pessoa"),
                    Content = new StringContent(jsonItem, Encoding.UTF8, "application/json"),
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responsePessoa = await httpClient.SendAsync(requestPessoa).ConfigureAwait(false);


                if (responsePessoa.StatusCode != HttpStatusCode.BadRequest)
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.AreEqual(HttpStatusCode.BadRequest, responsePessoa.StatusCode);
                }
            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
        [Fact]
        [Trait("Controller", "Válido")]
        public async Task TestarDeleteItemByIdValidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                var requestItem = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"https://localhost:7035/api/Item/{1}"), //Falta trocar request
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responseItem = await httpClient.SendAsync(requestItem).ConfigureAwait(false);


                if (responseItem.StatusCode == HttpStatusCode.NotFound)
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.AreEqual(responseItem.StatusCode, HttpStatusCode.OK);
                }

            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
        [Fact]
        [Trait("Controller", "Inválido")]
        public async Task TestarDeleteItemByIdInvalidoAsync()
        {
            try
            {
                var application = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder => { });

                var httpClient = application.CreateClient();

                var usuario = await new RequestLoginTeste().RetornaUsuLoginAsync(login);

                var requestItem = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"https://localhost:7035/api/Item/{0}"), //Falta trocar request
                };
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuario.Token);

                var responseItem = await httpClient.SendAsync(requestItem).ConfigureAwait(false);


                if (responseItem.StatusCode != HttpStatusCode.NotFound)
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.AreEqual(responseItem.StatusCode, HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                string menssage = ex.Message;
            }
        }
    }
}
