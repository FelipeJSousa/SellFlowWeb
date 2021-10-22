using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiClient.Interfaces;
using System;
using Models;
using System.Linq;
using SellFlowWeb.Models.ApiRequest;
using SellFlowWeb.Models.DataView;
using AutoMapper;
using System.Collections.Generic;

namespace SellFlowWeb.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly IProdutoClient _produtoClient;

        public ProdutoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _produtoClient = serviceProvider.GetService<IProdutoClient>();
        }

        public async Task<IActionResult> Index()
        {
            var _ret = await _produtoClient.GetAll();
            var _produtos = new Mapper(AutoMapperConfig.RegisterMappings()).Map<IEnumerable<ProdutoDataView>>(_ret.dados);
            return VerificarLogin(View(_produtos));
        }

        public IActionResult Criar()
        {
            @ViewBag.message = TempData["message"];
            return VerificarLogin(View());
        }

        public async Task<IActionResult> Editar(long id)
        {
            @ViewBag.message = TempData["message"];
            var _ret = await _produtoClient.Get(id);
            var _produtos = new Mapper(AutoMapperConfig.RegisterMappings()).Map<IEnumerable<ProdutoDataView>>(_ret.dados);
            return VerificarLogin(View(_produtos.FirstOrDefault()));
        }

        public async Task<IActionResult> Salvar(ProdutoDataView obj)
        {
            var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            var _produto = _mapper.Map<ProdutoApiRequest>(obj);

            ReturnModel<ProdutoModel> _ret = new();
            _ret = await _produtoClient.Save(_produto);

            if (!_ret.status)
            {
                TempData["message"] = "Não foi possível Salvar!";
                return View("Editar",obj.id);
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> ExcluirAsync(long id)
        {
            ReturnModel<ProdutoModel> _ret = await _produtoClient.Delete(id);

            if(!_ret.status)
            {
                TempData["message"] = "Não foi possível Excluir!";
                return View("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
