using ApiClient.Interfaces;
using Models;

namespace ApiClient
{
    public class CategoriaClient : BaseClient<CategoriaModel>, ICategoriaClient
    {
        public CategoriaClient() { }
    }
}
