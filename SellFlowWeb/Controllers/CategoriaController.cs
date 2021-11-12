using Microsoft.Extensions.DependencyInjection;
using ApiClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using SellFlowWeb.Models.DataView;
using System.Collections.Generic;
using System.Linq;
using SellFlowWeb.Models.ApiRequest;
using Models;

namespace SellFlowWeb.Controllers
{
    public class CategoriaController : BaseController
    {

        private readonly ICategoriaClient _categoriaClient;

        public CategoriaController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _categoriaClient = serviceProvider.GetService<ICategoriaClient>();
        }

        public async Task<IActionResult> Index()
        {
            var redirect = SessionExists();
            if (redirect is null)
            {
                var _ret = await _categoriaClient.GetAll();
                var _categoria = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<CategoriaDataView>>(_ret.dados);
                return SessionExists(View(_categoria));
            }
            return redirect;
        }

        public IActionResult Criar()
        {
            @ViewBag.message = TempData["message"];
            return SessionExists(View());
        }

        public async Task<IActionResult> Editar(long id)
        {
            @ViewBag.message = TempData["message"];
            var ret = await _categoriaClient.Get(id);
            var _categoria = new Mapper(AutoMapperConfig.RegisterMappings()).Map<IEnumerable<CategoriaDataView>>(ret.dados);
            return SessionExists(View(_categoria.FirstOrDefault()));
        }

        public async Task<IActionResult> Salvar(CategoriaDataView obj)
        {
            var redirect = SessionExists();
            if (redirect is null)
            {
                var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                var _produto = _mapper.Map<CategoriaApiRequest>(obj);
            
                ReturnModel<CategoriaModel> _ret = new();
                _ret = await _categoriaClient.Save(_produto);

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
            var redirect = SessionExists();
            if (redirect is null)
            {
                ReturnModel<CategoriaModel> _ret = await _categoriaClient.Delete(id);

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
