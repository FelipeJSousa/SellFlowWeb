using ApiClient.Interfaces;
using Models;

namespace ApiClient
{
    public class PessoaClient : BaseClient<PessoaModel>, IPessoaClient
    {
        public PessoaClient()
        {
        }

    }
}
