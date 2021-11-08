using ApiClient.Interfaces;
using Models;

namespace ApiClient
{
    public class PermissaoClient : BaseClient<PermissaoModel>, IPermissaoClient
    {
        public PermissaoClient()
        {
        }
    }
}
