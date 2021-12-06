using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;
using netbullAPI.Util;

namespace netbullAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : BaseController
    {
        private List<Item> listaItens;
        public ItemController(INotificador notificador) : base(notificador)
        {
        }

        //GET: api/<ItemController>
        [HttpGet]
        public IEnumerable<Item> Get([FromServices] NE_Item NE_Item)
        {
            return NE_Item.BuscaItens();
        }

        [HttpGet("{id}")]
        //GET: api/<ItemController>(id)
        public Item GetPorId([FromServices] NE_Item NE_Item, int id)
        {
            return NE_Item.BuscaItemPorId(id);
        }
    };
}

