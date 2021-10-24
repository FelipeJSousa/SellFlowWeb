using ApiClient.Interfaces;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ProdutoClient : BaseClient<ProdutoModel>, IProdutoClient
    {

        public ProdutoClient()
        {
        }

        public async Task<ReturnModel<List<ProdutoModel>>> GetAll(long usuario)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(ProdutoModel).Name.Replace("Model", "")), $"idUsuario={usuario}");
            return await GetAsync<List<ProdutoModel>>(requestUrl);
        }

        public async Task<ReturnModel<List<ProdutoModel>>> Get(long id, long usuario)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(ProdutoModel).Name.Replace("Model", "")), $"id={id}&idUsuario={usuario}");
            return await GetAsync<List<ProdutoModel>>(requestUrl);
        }

    }
}
