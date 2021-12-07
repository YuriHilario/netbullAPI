
using netbullAPI.Entidade;
using netbullAPI.Interfaces;
using netbullAPI.Negocio;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace DAO
{
    public class DAO_Endereco : DaoBase
    {
        private netbullDBContext _netbullDBContext;
        public DAO_Endereco(INotificador notificador, IConfiguration configuration, netbullDBContext netbullDBContext) : base(notificador, configuration)
        {
            _netbullDBContext = netbullDBContext;
        }

        public IEnumerable<Endereco> BuscaEnderecosPessoa(int idPessoa)
        {
            return _netbullDBContext.Enderecos.Where(x => x.endereco_idpessoa == idPessoa);
        }

        public bool CadastrarNovoEndereco(Endereco novoEndereco)
        {
            //verificar se pessoa e endereço existem
            try
            {
                //var pessoa = (from p in _netbullDBContext.Pessoas
                //              where p.pessoa_id == novoEndereco.endereco_idpessoa
                //              select p).FirstOrDefault();
                //if (pessoa == null)
                //    Notificar("Cliente informado inexistente.");

                int encontrouEndereco = (from end in _netbullDBContext.Enderecos
                                         where end.endereco_idpessoa == novoEndereco.endereco_idpessoa
                                         && end.endereco_logradouro == novoEndereco.endereco_logradouro
                                         && end.endereco_numero == novoEndereco.endereco_numero
                                         && end.endereco_complemento == novoEndereco.endereco_complemento
                                         select end).Count();

                if (encontrouEndereco != 0)
                {
                    Notificar("Endereço já cadastrado.");
                    return false;
                }
                using (_netbullDBContext)
                {
                    _netbullDBContext.Enderecos.Add(novoEndereco);
                    _netbullDBContext   .SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AtualizaEndereco(Endereco attEndereco)
        {
            try
            {
                //tem uma shadow index fk na entidade Pessoa que impede de fazer consulta nela
                //if(attEndereco.endereco_idpessoa == 0)
                //{
                //    var pessoa = _netbullDBContext.Pessoas.FirstOrDefault();
                //    if (pessoa == null)
                //    {
                //        Notificar("Cliente informado inexistente.");
                //        return attEndereco;
                //    }
                //} 

                //var enderecoExistente = _netbullDBContext.Enderecos.Where(x => x.endereco_id == attEndereco.endereco_id).FirstOrDefault();
                var enderecoExistente = (from e in _netbullDBContext.Enderecos
                                         where e.endereco_id == attEndereco.endereco_id
                                         select e).FirstOrDefault();
                if (enderecoExistente == null)
                {
                    _netbullDBContext.Enderecos.Add(attEndereco);
                    _netbullDBContext.SaveChanges();
                    Notificar("Endereço informado não existe.Criando um novo");
                    return false;
                }
                else
                {
                    using (_netbullDBContext)
                    {
                        enderecoExistente.endereco_logradouro = attEndereco.endereco_logradouro == null ? enderecoExistente.endereco_logradouro : attEndereco.endereco_logradouro;
                        enderecoExistente.endereco_numero = (attEndereco.endereco_numero == 0 ? enderecoExistente.endereco_numero : attEndereco.endereco_numero);
                        enderecoExistente.endereco_complemento = (attEndereco.endereco_complemento == null ? enderecoExistente.endereco_complemento : attEndereco.endereco_complemento);
                        _netbullDBContext.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AtualizaEnderecoPatch(int idEndereco, Endereco endereco)
        {
            try
            {
                var enderecoExistente = (from e in _netbullDBContext.Enderecos
                                         where e.endereco_id == idEndereco
                                         select e).FirstOrDefault();
                if (enderecoExistente == null)
                {
                    Notificar("Endereço informado não existe.");
                    return false;
                }

                using (_netbullDBContext)
                {
                    enderecoExistente.endereco_logradouro = endereco.endereco_logradouro == null || endereco.endereco_logradouro == "string" ? enderecoExistente.endereco_logradouro : endereco.endereco_logradouro;
                    enderecoExistente.endereco_numero = (endereco.endereco_numero == 0 ? enderecoExistente.endereco_numero : endereco.endereco_numero);
                    enderecoExistente.endereco_complemento = (endereco.endereco_complemento == null || endereco.endereco_complemento == "string" ? enderecoExistente.endereco_complemento : endereco.endereco_complemento);
                    _netbullDBContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AtualizaNumero(int idEndereco, Endereco endereco)
        {
            try
            {
                var enderecoExistente = (from e in _netbullDBContext.Enderecos
                                         where e.endereco_id == idEndereco
                                         select e).FirstOrDefault();
                if (enderecoExistente == null)
                {
                    Notificar("Endereço informado não existe.");
                    return false;
                }

                using (_netbullDBContext)
                {
                    enderecoExistente.endereco_numero = endereco.endereco_numero;
                    _netbullDBContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AtualizaComplemento(int idEndereco, Endereco endereco)
        {
            try
            {
                var enderecoExistente = (from e in _netbullDBContext.Enderecos
                                         where e.endereco_id == idEndereco
                                         select e).FirstOrDefault();
                if (enderecoExistente == null)
                {
                    Notificar("Endereço informado não existe.");
                    return false;
                }

                using (_netbullDBContext)
                {
                    enderecoExistente.endereco_complemento = endereco.endereco_complemento;
                    _netbullDBContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ApagaEndereco(int idEndereco)
        {
            try
            {
                Endereco endereco = _netbullDBContext.Enderecos.Where(x => x.endereco_id == idEndereco).FirstOrDefault();
                if (endereco == null)
                {
                    Notificar("Endereço não encontrado.");
                    return false;
                }
                else
                {
                    _netbullDBContext.Enderecos.Remove(endereco);
                    _netbullDBContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}