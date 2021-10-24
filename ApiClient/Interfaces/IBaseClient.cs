using Models;
using Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IBaseClient<T> : IApiBase
    {
        Task<ReturnModel<List<T>>> GetAll();

        Task<ReturnModel<List<T>>> Get(long id);

        Task<ReturnModel<T>> Save(IModel obj);

        Task<ReturnModel<T>> Delete(long id);

    }
}
