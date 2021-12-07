
using DAO;
using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.Repository;

namespace Negocio
{
    public class NE_Endereco
    {
        private DAO_Endereco _daoEndereco;
        public NE_Endereco(DAO_Endereco daoEndereco)
        {
            _daoEndereco = daoEndereco;
        }

        public IEnumerable<Endereco> BuscaEnderecosPessoa(int idPessoa)
        {
            return _daoEndereco.BuscaEnderecosPessoa(idPessoa);
        }

        public bool CadastraNovoEndereco(Endereco endereco)
        {
            return _daoEndereco.CadastrarNovoEndereco(endereco);
            }
            catch(Exception ex)
            {
                return false;
            }
            
        public bool AtualizaEndereco(Endereco endereco)
        {
           return _daoEndereco.AtualizaEndereco(endereco);
        }

        public bool AtualizaEnderecoPatch(int idEndereco, Endereco logradouro)
        {
            return _daoEndereco.AtualizaEnderecoPatch(idEndereco, logradouro); 
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
            return _daoEndereco.ApagaEndereco(idEndereco);
        }
    }
}