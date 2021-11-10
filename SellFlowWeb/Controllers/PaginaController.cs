using Models;
using System;
using AutoMapper;
using System.Linq;
using ApiClient.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SellFlowWeb.Models.DataView;
using SellFlowWeb.Models.ApiRequest;
using Microsoft.Extensions.DependencyInjection;

namespace SellFlowWeb.Controllers
{
    public class PaginaController : BaseController
    {
        private readonly IPaginaClient _paginaClient;
        private readonly IPermissaoClient _permissaoClient;

        public PaginaController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _paginaClient = serviceProvider.GetService<IPaginaClient>();
            _permissaoClient = serviceProvider.GetService<IPermissaoClient>();
        }

        public async Task<IActionResult> Index()
        {
            var redirect = VerificarLogin();
            if (redirect is null)
            {
                var returnModel = await _paginaClient.GetAll();
                var _pagina = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<PaginaDataView>>(returnModel?.dados);
                return View(_pagina);
            }
            return redirect;
        }

        public async Task<IActionResult> Criar()
        {
            var redirect = VerificarLogin();
            if (redirect is null)
            {
                @ViewBag.permissoes = (await _permissaoClient.GetAll())?.dados;
                @ViewBag.message = TempData["message"];
                return VerificarLogin(View());
            }
            return redirect;
        }

        public async Task<IActionResult> Editar(long id)
        {
            var redirect = VerificarLogin();
            if (redirect is null)
            {
                @ViewBag.permissoes = (await _permissaoClient.GetAll())?.dados;
                @ViewBag.message = TempData["message"];
                var ret = await _paginaClient.Get(id);
                var _pagina = new Mapper(AutoMapperConfig.RegisterMappings()).Map<IEnumerable<PaginaDataView>>(ret.dados);
                return VerificarLogin(View(_pagina.FirstOrDefault()));
            }
            return redirect;
        }

        public async Task<IActionResult> Salvar(PaginaDataView obj)
        {
            var redirect = VerificarLogin();
            if (redirect is null)
            {
                var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                var _produto = _mapper.Map<PaginaApiRequest>(obj);

                ReturnModel<PaginaModel> _ret = new();
                _ret = await _paginaClient.Save(_produto);

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
            return redirect;
        }

        public async Task<IActionResult> ExcluirAsync(long id)
        {
            var redirect = VerificarLogin();
            if (redirect is null)
            {
                ReturnModel<PaginaModel> _ret = await _paginaClient.Delete(id);

                if (!_ret.status)
                {
                    TempData["message"] = "Não foi possível Excluir!";
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            return redirect;
        }
    }
}
