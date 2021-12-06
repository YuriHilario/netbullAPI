using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;


namespace netbullAPI.Controllers
{
    
    [Authorize]
        [ApiController]
        [Route("api/[controller]")]
        public class ItemController : ControllerBase
        {
            private List<Item> listaItens;
            private NE_Item NE_Item;
            public ItemController(netbullDBContext ItemContexto)
            {
                NE_Item = new NE_Item(ItemContexto);
            }

            private NE_Item neItem;

            //GET: api/<ItemController>
            [HttpGet]
            public IEnumerable<Item> Get()
            {
                return NE_Item.BuscaItens();
            }

            [HttpGet("{id}")]
            //GET: api/<ItemController>(id)
            public Item GetPorId(int id)
            {
                return NE_Item.BuscaItemPorId(id);
            }
        };
    }

