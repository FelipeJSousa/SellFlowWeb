using ApiClient.Interfaces;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<ReturnModel<ProdutoModel>> Save(ProdutoModel obj)
        {
            if (obj.id > 0)
            {
                var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(ProdutoModel).Name.Replace("Model", "")));
                return await PutAsync<ProdutoModel, object>(requestUrl, content: MultiPartHttpContent(obj));
            }
            else
            {
                var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(ProdutoModel).Name.Replace("Model", "")));
                return await PostAsync<ProdutoModel, object>(requestUrl, content: MultiPartHttpContent(obj));
            }
        }

        public HttpContent MultiPartHttpContent(ProdutoModel obj)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(obj.imagemArquivo.OpenReadStream())
            {
                Headers =
                {
                    ContentLength = obj.imagemArquivo.Length,
                    ContentType = new MediaTypeHeaderValue(obj.imagemArquivo.ContentType)
                }
            };
            content.Add(fileContent, "imagem", obj.imagemArquivo.FileName);          // file
            obj.imagemArquivo = null;
            content.Add(new StringContent(JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            })), "json");   // form input
            return content;
        }
    }
}
