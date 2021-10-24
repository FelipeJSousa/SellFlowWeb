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

        [Route("{controller}/{usuario}")]
        public async Task<IActionResult> Index(int usuario)
        {
            var _ret = await _produtoClient.GetAll(usuario);
            var _produtos = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<ProdutoDataView>>(_ret.dados);
            return VerificarLogin(View(_produtos));
        }

        public IActionResult Criar()
        {
            @ViewBag.message = TempData["message"];
            return VerificarLogin(View());
        }

        [Route("Editar/{usuario}/{id}")]
        public async Task<IActionResult> Editar(long usuario, long id)
        {
            var _ret = await _produtoClient.Get(id);
            @ViewBag.message = TempData["message"];
            var ret = await _produtoClient.Get(id, usuario);
            var _produtos = new Mapper(AutoMapperConfig.RegisterMappings()).Map<IEnumerable<ProdutoDataView>>(_ret.dados);
            return VerificarLogin(View(_produtos.FirstOrDefault()));
        }

        public async Task<IActionResult> Salvar(ProdutoApiRequest obj)
        {
            var redirectHome = VerificarLogin();
            if (redirectHome is not null)
            {
                return redirectHome;
            }

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
            var redirectHome = VerificarLogin();
            if (redirectHome is not null)
            {
                return redirectHome;
            }

            ReturnModel<ProdutoModel> _ret = await _produtoClient.Delete(id);

            if(!_ret.status)
            {
                TempData["message"] = "Não foi possível Excluir!";
                return View("Index");
            }

            return View("Index");
        }

    }
}
