using ApiClient.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace SellFlowWeb.Controllers
{
    public class BaseController : Controller
    {
        private readonly IPermissaoPaginaClient _permissaoPaginaClient;

        protected BaseController(IServiceProvider serviceProvider)
        {
            _permissaoPaginaClient = serviceProvider.GetService<IPermissaoPaginaClient>();
        }

        public IActionResult SessionExists()
        {
            if (!HttpContext.Session.GetInt32("idusuario").HasValue)
            {
                return RedirectToAction("Index", "Login");
            }
            return null;
        }

        public IActionResult SessionExists(IActionResult view)
        {
            if (!HttpContext.Session.GetInt32("idusuario").HasValue)
            {
                return RedirectToAction("Index", "Login");
            }
            return view;
        }

        public bool ValidarPermissoes(string caminho, int idPermissao) => (_permissaoPaginaClient.ValidarPermissao(caminho, idPermissao)).GetAwaiter().GetResult()?.status ?? false;

    }
}
