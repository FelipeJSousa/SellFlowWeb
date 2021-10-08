﻿using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IBaseClient<T> : IApiBase
    {
        Task<ReturnModel<List<T>>> GetAll();

        Task<ReturnModel<List<T>>> Get(long id);

        Task<ReturnModel<T>> Save(object obj);

        Task<ReturnModel<T>> Update(object obj);

        Task<ReturnModel<List<T>>> Delete(int id);

    }
}
