using ApiClient.Interfaces;
using Models;
using System.Threading.Tasks;

namespace ApiClient
{
    public class UsuarioClient : BaseClient<UsuarioModel>, IUsuarioClient
    {
        public UsuarioClient()
        {
        }

        public async Task<ReturnModel<PessoaModel>> Validar(UsuarioModel obj)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Usuario/Validar"));
            return await PostAsync<PessoaModel, object>(requestUrl, obj);
        }
    }
}
