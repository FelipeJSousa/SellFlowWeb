using ApiClient.Interfaces;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient
{
    public class AnuncioClient : BaseClient<AnuncioModel>, IAnuncioClient
    {
        public AnuncioClient() { }

        public async Task<ReturnModel<List<AnuncioModel>>> GetAll(long usuario)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(AnuncioModel).Name.Replace("Model", "")), $"idUsuario={usuario}");
            return await GetAsync<List<AnuncioModel>>(requestUrl);
        }

        public async Task<ReturnModel<List<AnuncioModel>>> Get(long id, long usuario)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(AnuncioModel).Name.Replace("Model", "")), $"id={id}&idUsuario={usuario}");
            return await GetAsync<List<AnuncioModel>>(requestUrl);
        }

        public async Task<ReturnModel<List<AnuncioModel>>> GetPublicados()
        {
            var requestUrl = CreateRequestUri("Anuncio/Situacao", $"idSituacao=2");
            return await GetAsync<List<AnuncioModel>>(requestUrl);
        }
    }
}
