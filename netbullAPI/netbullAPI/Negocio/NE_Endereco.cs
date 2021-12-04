
using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace Negocio
{
    public class NE_Endereco
    {
        private netbullDBContext _EnderecoContexto;
        public NE_Endereco(netbullDBContext EnderecoContexto)
        {
            _EnderecoContexto = EnderecoContexto;
        }

        public IEnumerable<Endereco> BuscaEnderecosPessoa(int idPessoa)
        {
            return _EnderecoContexto.Enderecos.Where(x => x.endereco_idpessoa == idPessoa);
        }

        public bool CadastraNovoEndereco(Endereco endereco)
        {
            //verificar se enderçeo já existe
            try
            {
                int encontrouEndereco = (from end in _EnderecoContexto.Enderecos
                                     where end.endereco_idpessoa == endereco.endereco_idpessoa
                                     && end.endereco_logradouro == endereco.endereco_logradouro
                                     && end.endereco_numero == endereco.endereco_numero
                                     && end.endereco_complemento == endereco.endereco_complemento
                                     select end).Count();
                
                if (encontrouEndereco != 0)
                    return false;
                using (_EnderecoContexto)
                {
                    _EnderecoContexto.Enderecos.Add(endereco);
                    _EnderecoContexto.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public bool AtualizaEndereco(int idEndereco, string? logradouro, int? numero, string? compl)
        {
            try
            {
                Endereco endereco = _EnderecoContexto.Enderecos.Where(x => x.endereco_id == idEndereco).FirstOrDefault();
                if (endereco == null)
                    return false;
                else
                {
                    using (_EnderecoContexto)
                    {
                        endereco.endereco_logradouro = logradouro == null ? endereco.endereco_logradouro : logradouro;
                        endereco.endereco_numero = (int)(numero == null ? endereco.endereco_numero : numero);
                        endereco.endereco_complemento = (compl == null ? endereco.endereco_complemento : compl);
                        _EnderecoContexto.SaveChanges();
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public bool AtualizaLogradouro(int idEndereco, string logradouro)
        {
            try
            {
                Endereco endereco = _EnderecoContexto.Enderecos.Where(x => x.endereco_id == idEndereco).FirstOrDefault();
                if (endereco == null)
                    return false;
                else
                {
                    using (_EnderecoContexto)
                    {
                        endereco.endereco_logradouro = logradouro == null ? endereco.endereco_logradouro : logradouro;
                        _EnderecoContexto.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AtualizaNumero(int idEndereco, int numero)
        {
            try
            {
                Endereco endereco = _EnderecoContexto.Enderecos.Where(x => x.endereco_id == idEndereco).FirstOrDefault();
                if (endereco == null)
                    return false;
                else
                {
                    using (_EnderecoContexto)
                    {
                        endereco.endereco_numero = numero;
                        _EnderecoContexto.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ApagaEndereco(int idEndereco)
        {
            try
            {
                Endereco endereco = _EnderecoContexto.Enderecos.Where(x => x.endereco_id == idEndereco).FirstOrDefault();
                if (endereco == null)
                    return false;
                else
                {
                    _EnderecoContexto.Enderecos.Remove(endereco);
                    _EnderecoContexto.SaveChanges();
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}