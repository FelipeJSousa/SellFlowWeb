using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IProdutoClient : IBaseClient<ProdutoModel>
    {
        Task<ReturnModel<List<ProdutoModel>>> GetAll(long usuario);

        Task<ReturnModel<List<ProdutoModel>>> Get(long id, long usuario);
        Task<ReturnModel<ProdutoModel>> Save(ProdutoModel obj);
    }
}
