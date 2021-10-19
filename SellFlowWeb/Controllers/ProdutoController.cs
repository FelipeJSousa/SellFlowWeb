using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiClient.Interfaces;
using System;
using Models;

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
            return View(_ret.dados);
        }

        public async Task<ActionResult> Salvar(ProdutoModel obj)
        {

            ReturnModel<ProdutoModel> _ret = new();

            _ret = await _produtoClient.Save(obj);

            if (!_ret.status)
            {
                TempData["message"] = "Não foi possível Salvar!";
                return View("Editar",obj.id);
            }

            return View("Index");

        }

        public async Task<IActionResult> ExcluirAsync(long id)
        {
            ReturnModel<ProdutoModel> _ret = new();

            _ret = await _produtoClient.Delete(id);

            if (!_ret.status)
            {
                TempData["message"] = "Não foi possível Salvar!";
                BadRequest();
            }

            return Ok();
        }

    }
}
