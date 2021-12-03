using netbullAPI.Entidade;
using netbullAPI.Persistencia;

namespace netbullAPI.Repository
{
    public class TelefoneRepository
    {
        private netbullDBContext _netbullDBContext;
        public TelefoneRepository(netbullDBContext  netbullDBContext)
        {
            _netbullDBContext = netbullDBContext;
        }

        public IEnumerable<Telefone> BuscarTelefoneCliente(int id)
        {
            try
            {
                var tels = from telefone in _netbullDBContext.Telefones
                           where telefone.telefone_idPessoa == id
                           select telefone;
                return tels;
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
                var pessoa = _netbullDBContext.Pessoas.Where(t => t.pessoa_id == telefone.telefone_idPessoa);
                //Verifica se o cliente informado ja existe no banco
                if (pessoa == null)
                    return false;

                _netbullDBContext.Add(telefone);
                _netbullDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
