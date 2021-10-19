using ApiClient.Interfaces;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient
{
    public class BaseClient<T> : ApiBase, IBaseClient<T>
    {
        public BaseClient ()
        {
        }
         
        public async Task<ReturnModel<List<T>>> GetAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name));
            return await GetAsync<List<T>>(requestUrl);
        }

        public async Task<ReturnModel<List<T>>> Get(long id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name.Replace("Model","")), $"id={id}");
            return await GetAsync<List<T>>(requestUrl);
        }

        public async Task<ReturnModel<T>> Save(object obj)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name));
            return await PostAsync<T, object>(requestUrl, obj);
        }

        public async Task<ReturnModel<T>> Update(object obj)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name));
            return await PutAsync<T, object>(requestUrl, obj);
        }

        public async Task<ReturnModel<List<T>>> Delete(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name), $"id={id}");
            return await GetAsync<List<T>>(requestUrl);
        }

    }
}
