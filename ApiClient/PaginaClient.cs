using ApiClient.Interfaces;
using Models;

namespace ApiClient
{
    public class PaginaClient : BaseClient<PaginaModel>, IPaginaClient
    {
        public PaginaClient()
        {
        }
    }
}
