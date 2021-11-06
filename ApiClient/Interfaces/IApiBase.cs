﻿using Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IApiBase
    {
        Task<ReturnModel<T>> GetAsync<T>(Uri requestUrl);

        Task<ReturnModel<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 obj = default, HttpContent content = null);

        Task<ReturnModel<T1>> PutAsync<T1, T2>(Uri requestUrl, T2 obj = default, HttpContent content = null);

        Task<ReturnModel<T>> DeleteAsync<T>(Uri requestUrl);

        Uri CreateRequestUri(string relativePath, string queryString = "");

        HttpContent JsonHttpContent<T>(T content);

    }
}
