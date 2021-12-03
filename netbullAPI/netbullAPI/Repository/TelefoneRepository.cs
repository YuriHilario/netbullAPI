using Microsoft.EntityFrameworkCore;
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
                var pessoa = _netbullDBContext.Pessoas.Where(p => p.pessoa_id == telefone.telefone_idPessoa);
                //Verifica se o cliente informado ja existe no banco
                if (pessoa == null)
                    return false;

                var novoTelefone = new Telefone()
                {
                    //Criado id dessa forma pois não tem autoincremento implementado
                    telefone_id = _netbullDBContext.Telefones.Max(m => m.telefone_id) + 1,
                    telefone_idPessoa = telefone.telefone_idPessoa,
                    telefone_numero = telefone.telefone_numero
                };

                _netbullDBContext.Add(novoTelefone);
                _netbullDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AtualizaTelefone(Telefone telefone)
        {
            try
            {
                var pessoa = _netbullDBContext.Pessoas.Where(p => p.pessoa_id == telefone.telefone_idPessoa);
                //Verifica se o cliente informado ja existe no banco
                if (pessoa == null)
                    return false;
                var telefoneExistente = _netbullDBContext.Telefones.Where(t => t.telefone_id == telefone.telefone_id)?.First();
                if (telefoneExistente == null)
                    return false;
                telefoneExistente.telefone_numero = telefone.telefone_numero;
                _netbullDBContext.Update(telefoneExistente);
                _netbullDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletaTelefone(int id)
        {
            try
            {
                var telefoneExistente = _netbullDBContext.Telefones.Where(t => t.telefone_id == id)?.First();
                if (telefoneExistente == null)
                    return false;
                _netbullDBContext.Remove(telefoneExistente);
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
