using netbullAPI.Entidade;
using netbullAPI.Persistencia;
using netbullAPI.ViewModels;

namespace netbullAPI.Negocio
{
    public class NE_Endereco
    {
        private DAO_Endereco _daoEndereco;
        public NE_Endereco(DAO_Endereco daoEndereco)
        {
            _daoEndereco = daoEndereco;
        }

        public async Task<IEnumerable<Endereco>> BuscaEnderecosPessoa(int idPessoa)
        {
            return await _daoEndereco.BuscaEnderecosPessoa(idPessoa);
        }

        public async Task<bool> CadastraNovoEndereco(RegistrarEnderecoViewModel endereco)
        {
            return await _daoEndereco.CadastrarNovoEndereco(endereco);
        }
        public async Task<bool> AtualizaEndereco(AlterarEnderecoViewModel endereco, int idEndereco)
        {
            return await _daoEndereco.AtualizaEndereco(endereco, idEndereco);
        }

        public async Task<bool> AtualizaEnderecoPatch(int idEndereco, AlterarEnderecoViewModel logradouro)
        {
            return await _daoEndereco.AtualizaEnderecoPatch(idEndereco, logradouro);
        }

        public async Task<bool> ApagaEndereco(int idEndereco)
        {
            return await _daoEndereco.ApagaEndereco(idEndereco);
        }
    }
}