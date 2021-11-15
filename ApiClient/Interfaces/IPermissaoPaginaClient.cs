using Models;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IPermissaoPaginaClient : IBaseClient<PermissaoPaginaModel>
    {
        Task<ReturnModel<PermissaoPaginaModel>> ValidarPermissao(string nomePagina, long idPermissao);
    }
}
