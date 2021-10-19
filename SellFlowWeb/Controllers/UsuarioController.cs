using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiClient.Interfaces;
using System.Linq;
using Models;
using System;

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
        
        public async Task<IActionResult> Salvar(PessoaModel obj)
        {
            if(obj.usuarioObj.permissao is null)
            {
                obj.usuarioObj.permissao = 1; //ADM
            }

            var retusu = await _usuarioClient.Save(obj.usuarioObj);
            if (retusu.status)
            {
                obj.usuario = retusu.dados.id;
                obj.usuarioObj = null;
                var retpes = await _pessoaClient.Save(obj);

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
