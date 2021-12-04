using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace netbullAPI.Negocio
{
    public class NE_Telefone
    {
        private DAOTelefone _daoTelefone;
        public NE_Telefone(DAOTelefone daoTelefone)
        {
            _daoTelefone = daoTelefone;
        }

        public IEnumerable<Telefone>  BuscaTelefoneCliente(int id)
        {
            try
            {
                return _daoTelefone.BuscaTelefoneCliente(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Telefone AdicionaTelefone(Telefone telefone)
        {
            try
            {
                return _daoTelefone.AdicionaTelefone(telefone);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Telefone AtualizaTelefone(Telefone telefone)
        {
            try
            {
                return _daoTelefone.AtualizaTelefone(telefone);
            }
            catch (Exception e)
            {
                throw e;
            }        
        }

        public bool DeletaTelefone(int id)
        {
            try
            {
                return _daoTelefone.DeletaTelefone(id);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
