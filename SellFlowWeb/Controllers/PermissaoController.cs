using ApiClient.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Models;
using SellFlowWeb.Models.ApiRequest;
using SellFlowWeb.Models.DataView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellFlowWeb.Controllers
{
    public class PermissaoController : BaseController
    {

        private readonly IPermissaoClient _permissaoClient;
        private readonly IPaginaClient _paginaClient;

        public PermissaoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _permissaoClient = serviceProvider.GetService<IPermissaoClient>();
            _paginaClient = serviceProvider.GetService<IPaginaClient>();
        }

        public async Task<IActionResult> Index()
        {
            var returnModel = await _permissaoClient.GetAll();
            var permissao = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<PermissaoDataView>>(returnModel?.dados);
            return View(permissao);
        }

        public async Task<IActionResult> Criar()
        {
            @ViewBag.paginas = (await _paginaClient.GetAll())?.dados;
            @ViewBag.message = TempData["message"];
            return VerificarLogin(View());
        }

        public async Task<IActionResult> Editar(long id)
        {
            @ViewBag.paginas = (await _paginaClient.GetAll())?.dados;
            @ViewBag.message = TempData["message"];
            var ret = await _permissaoClient.Get(id);
            var _permissao = new Mapper(AutoMapperConfig.RegisterMappings()).Map<IEnumerable<PermissaoDataView>>(ret.dados);
            return VerificarLogin(View(_permissao.FirstOrDefault()));
        }

        public async Task<IActionResult> Salvar(PermissaoDataView obj)
        {
            var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            var _produto = _mapper.Map<PermissaoApiRequest>(obj);

            ReturnModel<PermissaoModel> _ret = new();
            _ret = await _permissaoClient.Save(_produto);

            if (!_ret.status)
            {
                TempData["message"] = "Não foi possível Salvar!";
                if (obj.id > 0)
                {
                    return View("Editar", new { id = obj.id });
                }
                else
                {
                    return View("Criar");
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ExcluirAsync(long id)
        {
            ReturnModel<PermissaoModel> _ret = await _permissaoClient.Delete(id);

            if (!_ret.status)
            {
                TempData["message"] = "Não foi possível Excluir!";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
