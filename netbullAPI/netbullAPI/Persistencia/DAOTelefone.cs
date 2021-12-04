using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;

namespace netbullAPI.Persistencia
{
    public class DAOTelefone : DaoBase
    {
        private netbullDBContext _netbullDBContext;
        public DAOTelefone(INotificador notificador, IConfiguration configuration, netbullDBContext netbullDBContext) : base(notificador, configuration)      
        {
            _netbullDBContext = netbullDBContext;
        }

        public IEnumerable<Telefone> BuscaTelefoneCliente(int id)
        {
            try
            {
                var tels = from telefone in _netbullDBContext.Telefones
                           where telefone.telefone_idPessoa == id
                           select telefone;
                return tels;
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
                var pessoa = _netbullDBContext.Pessoas.Where(p => p.pessoa_id == telefone.telefone_idPessoa);
                //Verifica se o cliente informado ja existe no banco
                if (pessoa == null)
                    Notificar("Cliente informado inexistente");

                var novoTelefone = new Telefone()
                {
                    //Criado id dessa forma pois não tem autoincremento implementado
                    telefone_id = _netbullDBContext.Telefones.Max(m => m.telefone_id) + 1,
                    telefone_idPessoa = telefone.telefone_idPessoa,
                    telefone_numero = telefone.telefone_numero
                };

                _netbullDBContext.Add(novoTelefone);
                _netbullDBContext.SaveChanges();
                return novoTelefone;
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
                var pessoa = _netbullDBContext.Pessoas.Where(x => x.pessoa_id == telefone.telefone_idPessoa).FirstOrDefault();
                //Verifica se o cliente informado ja existe no banco
                if (pessoa == null)
                    throw new Exception("Cliente informado inexistente");
                var telefoneExistente = _netbullDBContext.Telefones.Where(t => t.telefone_id == telefone.telefone_id).FirstOrDefault();
                if (telefoneExistente == null)
                    throw new Exception("Telefone informado inexistente");

                if (telefoneExistente.telefone_idPessoa != telefone.telefone_idPessoa)
                    throw new Exception("Pessoa registrada para este telefone é diferente da informada");

                ValidaTelefone(telefone.telefone_numero);

                telefoneExistente.telefone_numero = telefone.telefone_numero;
                _netbullDBContext.Update(telefoneExistente);
                _netbullDBContext.SaveChanges();
                return telefone;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ValidaTelefone(int telefone)
        {
            if (telefone == 0)
                throw new Exception("Valor de telefone informado inválido");
            else if (telefone.ToString().Length < 8)
                throw new Exception("Formatação de telefone informada é inválida");
        }

        public bool DeletaTelefone(int id)
        {
            try
            {
                var telefoneExistente = _netbullDBContext.Telefones.Where(t => t.telefone_id == id).FirstOrDefault();
                if (telefoneExistente == null)
                    throw new Exception("Telefone informado inexistente");
                _netbullDBContext.Remove(telefoneExistente);
                _netbullDBContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
