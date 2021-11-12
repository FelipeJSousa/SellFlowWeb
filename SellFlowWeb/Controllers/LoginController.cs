using System;
using System.Linq;
using System.Threading.Tasks;
using ApiClient.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SellFlowWeb.Models.ApiRequest;

namespace SellFlowWeb.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IPessoaClient _pessoaClient;
        public LoginController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _pessoaClient = serviceProvider.GetService<IPessoaClient>();
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost("[controller]/Auth")]
        public async Task<IActionResult> Auth([FromBody]ApiRequestAuth obj)
        {
            if (obj.id > 0 && !string.IsNullOrWhiteSpace(obj.token))
            {
                var _ret = await _pessoaClient.GetPessoaByUsuario(obj.id);
                if (_ret != null && _ret.status && _ret.dados.Count > 0)
                {
                    var _pessoa = _ret.dados.FirstOrDefault();
                    HttpContext.Session.SetInt32("idusuario", (int)_pessoa.usuarioObj.id);
                    HttpContext.Session.SetInt32("idpermissao", (int)(_pessoa.usuarioObj.permissao.HasValue ? _pessoa.usuarioObj.permissao : 2));
                    HttpContext.Session.SetString("email", _pessoa.usuarioObj.email);
                    HttpContext.Session.SetString("nome", _pessoa.nome);
                    HttpContext.Session.SetString("sobrenome", _pessoa.sobrenome);
                    HttpContext.Session.SetString("cpf", _pessoa.cpf);
                    HttpContext.Session.SetString("token", obj.token);
                    return Ok(true);
                } 
            }
            TempData["message"] = "Não foi possível realizar o login.";
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

    }
}
