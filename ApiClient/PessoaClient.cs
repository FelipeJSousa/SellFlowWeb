using ApiClient.Interfaces;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient
{
    public class PessoaClient : BaseClient<PessoaModel>, IPessoaClient
    {
        public PessoaClient()
        {
        }

        public async Task<ReturnModel<List<PessoaModel>>> GetPessoaByUsuario(long idUsuario)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Pessoa/ObterPorUsuario"), $"idUsuario={idUsuario}");
            return await GetAsync<List<PessoaModel>>(requestUrl);
        }

    }
}
