using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace netbullAPI.Negocio
{
    public class NE_Telefone
    {
        private TelefoneRepository _telefoneRepository;
        public NE_Telefone(netbullDBContext netbullDBContext)
        {
            _telefoneRepository = new TelefoneRepository(netbullDBContext);
        }

        public IEnumerable<Telefone> BuscaTelefoneCliente(int id)
        {
            try
            {
                return _telefoneRepository.BuscarTelefoneCliente(id).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AdicionaTelefone(Telefone telefone)
        {
            try
            {
                return _telefoneRepository.AdicionaTelefone(telefone);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AtualizaTelefone(Telefone telefone)
        {
            return _telefoneRepository.AtualizaTelefone(telefone);         
        }

        public bool DeletaTelefone(int id)
        {
            return _telefoneRepository.DeletaTelefone(id);
        }
    }
}
