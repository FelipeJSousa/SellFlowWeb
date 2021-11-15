using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiClient.Interfaces;
using Models;
using System;
using AutoMapper;
using SellFlowWeb.Models.DataView;
using SellFlowWeb.Models.ApiRequest;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

namespace SellFlowWeb.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioClient _usuarioClient;
        private readonly IPessoaClient _pessoaClient;
        private readonly IPermissaoClient _permissaoClient;

        public UsuarioController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _usuarioClient = serviceProvider.GetService<IUsuarioClient>();
            _pessoaClient = serviceProvider.GetService<IPessoaClient>();
            _permissaoClient = serviceProvider.GetService<IPermissaoClient>();
        }

        public async Task<IActionResult> Index(int usuario)
        {
            var redirect = SessionExists();
            if (redirect is null)
            {
                var _ret = await _pessoaClient.GetAll();
                var _produtos = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<PessoaDataView>>(_ret.dados);
                return View(_produtos);
            }
            return redirect;
        }

        public async Task<IActionResult> Criar()
        {
            var redirect = SessionExists();
            if (redirect is null)
            {
                var _ret = await _permissaoClient.GetAll();
                ViewBag.permissoes = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<PermissaoDataView>>(_ret.dados);
                return View();
            }
            return redirect;
        }

        [Route("{controller}/Editar/{id}")]
        public async Task<IActionResult> Editar(long id)
        {
            var redirect = SessionExists();
            if (redirect is null)
            {
                @ViewBag.message = TempData["message"];
                var ret = await _pessoaClient.GetPessoaByUsuario(id);
                var _ret = await _permissaoClient.GetAll();
                ViewBag.permissoes = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<PermissaoDataView>>(_ret.dados);
                var _pessoa = new Mapper(AutoMapperConfig.RegisterMappings()).Map<PessoaDataView>(ret.dados.FirstOrDefault());
                return View(_pessoa);
            }
            return redirect;
        }


        public IActionResult Cadastrar()
        {
            return View();
        }
        
        public async Task<IActionResult> Salvar(PessoaDataView obj)
        {
            var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            var _obj = _mapper.Map<PessoaModel>(obj);
            
            var _objusu = _mapper.Map<UsuarioApiRequest>(_obj.usuarioObj);
            _objusu.senha = _objusu.senha == "*****" ? null : _objusu.senha;
            var retusu = await _usuarioClient.Save(_objusu);
            if (retusu?.status == true)
            {
                var _objpes = _mapper.Map<PessoaApiRequest>(obj);
                _objpes.usuario = retusu.dados.id.Value;
                var retpes = await _pessoaClient.Save(_objpes);

                if(HttpContext.Session.GetInt32("idpermissao").HasValue && HttpContext.Session.GetInt32("idpermissao").Value != 2)
                {
                    if (!retpes.status)
                    {
                        TempData["message"] = "Não foi possível realizar o cadastro.";
                        return View("Criar");
                    }
                    else
                    {
                        TempData["message"] = "Login criado com sucesso!";
                        return RedirectToAction("Index", "Usuario");
                    }
                }
                else
                {
                    if (!retpes.status)
                    {
                        TempData["message"] = "Não foi possível realizar o cadastro.";
                        return View("Cadastrar");
                    }
                    else
                    {
                        TempData["message"] = "Login criado com sucesso!";
                        return RedirectToAction("Index", "Login");
                    }
                }
            }
            if (HttpContext.Session.GetInt32("permissao").HasValue && HttpContext.Session.GetInt32("permissao").Value != 2)
            {
                TempData["message"] = "Não foi possível realizar o cadastro.";
                return View("Criar");
            }

            TempData["message"] = retusu.erro ?? retusu.mensagem;
            TempData["message"] += "\nNão foi possível realizar o cadastro.";
            
            return View("Cadastrar");
        }

        [HttpGet("Perfil")]
        public async Task<IActionResult> Perfil()
        {
            var redirect = SessionExists();
            if(redirect is null)
            {
                var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
                var _ret = await _pessoaClient.GetPessoaByUsuario(HttpContext.Session.GetInt32("idusuario").Value);
                return View(_mapper.Map<PessoaDataView>(_ret.dados.FirstOrDefault() ?? default));
            }
            return redirect;
        }

        public async Task<IActionResult> ExcluirAsync(long id)
        {
            var redirect = SessionExists();
            if (redirect is null)
            {
                ReturnModel<PessoaModel> _ret = await _pessoaClient.Delete(id);

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
