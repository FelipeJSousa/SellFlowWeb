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
using Microsoft.AspNetCore.Http;

namespace SellFlowWeb.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly IProdutoClient _produtoClient;

        public ProdutoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _produtoClient = serviceProvider.GetService<IProdutoClient>();
        }

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

        [Route("{controller}/Editar/{usuario}/{id}")]
        public async Task<IActionResult> Editar(long usuario, long id)
        {
            @ViewBag.message = TempData["message"];
            var ret = await _produtoClient.Get(id, usuario);
            var _produtos = new Mapper(AutoMapperConfig.RegisterMappings()).Map<IEnumerable<ProdutoDataView>>(ret.dados);
            return VerificarLogin(View(_produtos.FirstOrDefault()));
        }

        public async Task<IActionResult> Salvar(ProdutoDataView obj)
        {
            var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            var _produto = _mapper.Map<ProdutoApiRequest>(obj);
            var idusuario = HttpContext.Session.GetInt32("idusuario").Value;
            ReturnModel<ProdutoModel> _ret = new();
            _ret = await _produtoClient.Save(_produto);

            if (!_ret.status)
            {
                TempData["message"] = "Não foi possível Salvar!";
                if(obj.id > 0)
                {
                    return View("Editar", new { id = obj.id, usuario = idusuario});
                }
                else
                {
                    return View("Criar");
                }

            }

            return RedirectToAction("Index", new { usuario = idusuario });

        }

        public async Task<IActionResult> ExcluirAsync(long id)
        {
            ReturnModel<ProdutoModel> _ret = await _produtoClient.Delete(id);

            if(!_ret.status)
            {
                TempData["message"] = "Não foi possível Excluir!";
                return RedirectToAction("Index", new { usuario = HttpContext.Session.GetInt32("idusuario").Value });
            }

            return RedirectToAction("Index", new { usuario = HttpContext.Session.GetInt32("idusuario").Value });
        }

    }
}
