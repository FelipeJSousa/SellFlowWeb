using ApiClient.Interfaces;
using Models;
using System.Threading.Tasks;

namespace ApiClient
{
    public class PermissaoPaginaClient : BaseClient<PermissaoPaginaModel>, IPermissaoPaginaClient
    {
        public PermissaoPaginaClient()
        {
        }

        public async Task<ReturnModel<PermissaoPaginaModel>> ValidarPermissao(string caminhoPagina, long idPermissao)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(PermissaoPaginaModel).Name.Replace("Model", "")), $"caminhoPagina={caminhoPagina}&idPermissao={idPermissao}");
            return await GetAsync<PermissaoPaginaModel>(requestUrl);
        }


    }
}
