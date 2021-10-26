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

namespace SellFlowWeb.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioClient _usuarioClient;
        private readonly IPessoaClient _pessoaClient;

        public UsuarioController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _usuarioClient = serviceProvider.GetService<IUsuarioClient>();
            _pessoaClient = serviceProvider.GetService<IPessoaClient>();
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
            var retusu = await _usuarioClient.Save(_objusu);
            if (retusu.status)
            {
                var _objpes = _mapper.Map<PessoaApiRequest>(obj);
                _objpes.usuario = retusu.dados.id.Value;
                var retpes = await _pessoaClient.Save(_objpes);

                if (!retpes.status) 
                {
                    TempData["message"] = "Não foi possível realizar o cadastro.";
                    return View("Cadastrar");
                }
                else
                {
                    TempData["message"] = "Login criado com sucesso!";
                    return RedirectToAction("Index","Login");
                }
            }

            TempData["message"] = retusu.erro ?? retusu.mensagem;
            TempData["message"] += "\nNão foi possível realizar o cadastro.";
            
            return View("Cadastrar");
        }

        public async Task<IActionResult> Perfil()
        {
            var redirect = VerificarLogin();
            if(redirect is not null)
            {
                return redirect;
            }
            var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            var _ret = await _pessoaClient.GetPessoaByUsuario(HttpContext.Session.GetInt32("idusuario").Value);
            return View(_mapper.Map<PessoaDataView>(_ret.dados.FirstOrDefault() ?? default));
        }
    }
}
