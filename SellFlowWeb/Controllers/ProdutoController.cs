using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiClient.Interfaces;
using System;

namespace SellFlowWeb.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly IProdutoClient _produtoClient;

        public ProdutoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _produtoClient = serviceProvider.GetService<IProdutoClient>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Criar()
        {
            @ViewBag.message = TempData["message"];
            return View();
        }

        public async Task<IActionResult> Editar(long id)
        {
            var _ret = await _produtoClient.Get(id);
            @ViewBag.message = TempData["message"];
            return View(_ret);
        }

        public IActionResult Excluir(long id)
        {
            return View();
        }

    }
}
