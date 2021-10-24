using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClient.Interfaces
{
    public interface IUsuarioClient : IBaseClient<UsuarioModel>
    {
        Task<ReturnModel<PessoaModel>> Validar(UsuarioModel obj);
    }
}
