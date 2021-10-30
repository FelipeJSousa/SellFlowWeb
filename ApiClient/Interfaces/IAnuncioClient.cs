using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IAnuncioClient : IBaseClient<AnuncioModel>
    {
        Task<ReturnModel<List<AnuncioModel>>> GetAll(long usuario);

        Task<ReturnModel<List<AnuncioModel>>> Get(long id, long usuario);

        Task<ReturnModel<List<AnuncioModel>>> GetPublicados();
    }
}
