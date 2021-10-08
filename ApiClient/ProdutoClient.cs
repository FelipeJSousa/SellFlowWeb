using ApiClient.Interfaces;
using Models;
using System;

namespace ApiClient
{
    public class ProdutoClient : BaseClient<ProdutoModel>, IProdutoClient
    {

        public ProdutoClient()
        {
        }

    }
}
