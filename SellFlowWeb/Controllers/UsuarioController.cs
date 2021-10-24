using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiClient.Interfaces;
using Models;
using System;
using AutoMapper;
using SellFlowWeb.Models.DataView;
using SellFlowWeb.Models.ApiRequest;

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
            return VerificarLogin(View());
        }
        
        public async Task<IActionResult> Salvar(PessoaDataView obj)
        {
            var redirectHome = VerificarLogin();
            if (redirectHome is not null)
            {
                return redirectHome;
            }

            var _mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            var _obj = _mapper.Map<PessoaModel>(obj);

            if (_obj.usuarioObj.permissao is null)
            {
                _obj.usuarioObj.permissao = 1;
            }

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
                    return View("Cadastro");
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
    }
}
