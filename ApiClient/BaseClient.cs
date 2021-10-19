using ApiClient.Interfaces;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient
{
    public abstract class BaseClient<T> : ApiBase, IBaseClient<T> where T : Model
    {
        public BaseClient ()
        {
        }
         
        public async Task<ReturnModel<List<T>>> GetAll()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name.Replace("Model", "")));
            return await GetAsync<List<T>>(requestUrl);
        }

        public async Task<ReturnModel<List<T>>> Get(long id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name.Replace("Model","")), $"id={id}");
            return await GetAsync<List<T>>(requestUrl);
        }

        public async Task<ReturnModel<T>> Save(IModel obj)
        {
            if (obj.id > 0)
            {
                var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name.Replace("Model", "")));
                return await PutAsync<T, object>(requestUrl, obj);
            }
            else
            {
                var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name.Replace("Model", "")));
                return await PostAsync<T, object>(requestUrl, obj);
            }
        }

        public async Task<ReturnModel<T>> Delete(long id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name.Replace("Model", "")), $"id={id}");
            return await DeleteAsync<T>(requestUrl);
        }

    }
}
