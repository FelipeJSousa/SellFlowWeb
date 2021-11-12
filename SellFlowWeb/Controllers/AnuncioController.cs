using ApiClient.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class AnuncioController : BaseController
    {
        private readonly IAnuncioClient _anuncioClient;
        private readonly IProdutoClient _produtoClient;

        public AnuncioController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _anuncioClient = serviceProvider.GetService<IAnuncioClient>();
            _produtoClient = serviceProvider.GetService<IProdutoClient>();
        }

        public async Task<IActionResult> Index(int usuario)
        {
            var _ret = await _anuncioClient.GetAll(usuario);
            var _anuncios = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<AnuncioDataView>>(_ret.dados);
            return SessionExists(View(_anuncios));
        }

        public IActionResult Criar()
        {
            if(!ValidarPermissoes("/Anuncio/Criar" , HttpContext.Session.GetInt32("idpermissao") ?? 2))
            {
                if (!string.IsNullOrWhiteSpace(Request.Headers["Referer"].ToString()))
                { 
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                return Redirect(Request.Host.ToString());
            }
            @ViewBag.message = TempData["message"];
            @ViewBag.produtos = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<ProdutoDisplayDataView>>(
                                            _produtoClient.GetAll(HttpContext.Session.GetInt32("idusuario").Value).GetAwaiter().GetResult().dados);
            return SessionExists(View());
        }

        [Route("{controller}/Editar/{usuario}/{id}")]
        public async Task<IActionResult> Editar(long usuario, long id)
        {
            @ViewBag.message = TempData["message"];
            @ViewBag.produtos = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<ProdutoDisplayDataView>>(
                                            _produtoClient.GetAll(HttpContext.Session.GetInt32("idusuario").Value).GetAwaiter().GetResult().dados);
            var ret = await _anuncioClient.Get(id, usuario);
            var _anuncios = new Mapper(AutoMapperConfig.RegisterMappings()).Map<IEnumerable<AnuncioDataView>>(ret.dados);
            return SessionExists(View(_anuncios.FirstOrDefault()));
        }

        public async Task<IActionResult> Salvar(AnuncioDataView obj)
        {
            var redirect = SessionExists();
            if (redirect is null)
            {
                var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                var _produto = _mapper.Map<AnuncioApiRequest>(obj);
                var idusuario = HttpContext.Session.GetInt32("idusuario").Value;
                ReturnModel<AnuncioModel> _ret = new();
                _ret = await _anuncioClient.Save(_produto);

                if (!_ret.status)
                {
                    TempData["message"] = "Não foi possível Salvar!";
                    if (obj.id.HasValue && obj.id > 0)
                    {
                        return View("Editar", new { id = obj.id, usuario = idusuario });
                    }
                    else
                    {
                        return View("Criar");
                    }
                }
                return RedirectToAction("Index", new { usuario = idusuario });
            }
            return redirect;
        }

        public async Task<IActionResult> ExcluirAsync(long id)
        {
            var redirect = SessionExists();
            if (redirect is null)
            {
                ReturnModel<AnuncioModel> _ret = await _anuncioClient.Delete(id);

                if (!_ret.status)
                {
                    TempData["message"] = "Não foi possível Excluir!";
                    return RedirectToAction("Index", new { usuario = HttpContext.Session.GetInt32("idusuario").Value });
                }

                return RedirectToAction("Index", new { usuario = HttpContext.Session.GetInt32("idusuario").Value });
            }
            return redirect;
        }
    }
}
