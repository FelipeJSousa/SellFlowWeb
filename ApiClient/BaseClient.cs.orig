using ApiClient.Interfaces;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient
{
<<<<<<< Updated upstream
    public class BaseClient<T> : ApiBase, IBaseClient<T>
=======
    public abstract class BaseClient<T> : ApiBase, IBaseClient<T> where T : Model
>>>>>>> Stashed changes
    {
        public BaseClient ()
        {
        }
         
        public async Task<ReturnModel<List<T>>> GetAll()
        {
<<<<<<< Updated upstream
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name));
=======
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name.Replace("Model", "")));
>>>>>>> Stashed changes
            return await GetAsync<List<T>>(requestUrl);
        }

        public async Task<ReturnModel<List<T>>> Get(long id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name.Replace("Model","")), $"id={id}");
            return await GetAsync<List<T>>(requestUrl);
        }

<<<<<<< Updated upstream
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
=======
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

        public async Task<ReturnModel<List<T>>> Delete(long id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, typeof(T).Name.Replace("Model", "")), $"id={id}");
            return await DeleteAsync<List<T>>(requestUrl);
>>>>>>> Stashed changes
        }

    }
}
