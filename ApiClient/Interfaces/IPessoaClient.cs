using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IPessoaClient : IBaseClient<PessoaModel>
    {
        Task<ReturnModel<List<PessoaModel>>> GetPessoaByUsuario(long idUsuario);
    }
}
