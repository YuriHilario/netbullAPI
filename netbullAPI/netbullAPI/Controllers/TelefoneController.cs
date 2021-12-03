using Microsoft.AspNetCore.Mvc;
using netbullAPI.Entidade;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace netbullAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefoneController : ControllerBase
    {
        private NE_Telefone NE_Telefone;
        public TelefoneController(netbullDBContext netbullDBContext)
        {
            NE_Telefone = new NE_Telefone(netbullDBContext);
        }
        
        // GET api/<TelefoneController>/5
        [HttpGet("{id}")]
        public IEnumerable<Telefone> GetPorId(int id)
        {
            try
            {
                return NE_Telefone.BuscaTelefoneCliente(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        public bool Post([FromBody] Telefone telefone)
        {
            try
            {
                return NE_Telefone.AdicionaTelefone(telefone);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut("{id}")]
        public void Put([FromBody] Telefone telefone)
        {

        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
