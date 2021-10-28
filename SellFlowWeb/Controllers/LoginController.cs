using System;
using System.Threading.Tasks;
using ApiClient.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace SellFlowWeb.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUsuarioClient _usuarioClient;
        public LoginController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _usuarioClient = serviceProvider.GetService<IUsuarioClient>();
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Auth(UsuarioModel obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.email) && !string.IsNullOrWhiteSpace(obj.senha))
            {
                var _obj = new Mapper(AutoMapperConfig.RegisterMappings()).Map<UsuarioModel>(obj);
                var _retUsuario = await _usuarioClient.Validar(_obj);
                if (_retUsuario != null && _retUsuario.status)
                {
                    HttpContext.Session.SetInt32("idusuario", ((int)_retUsuario.dados.usuarioObj.id));
                    HttpContext.Session.SetString("email", _retUsuario.dados.usuarioObj.email);
                    HttpContext.Session.SetString("nome", _retUsuario.dados.nome);
                    HttpContext.Session.SetString("sobrenome", _retUsuario.dados.sobrenome);
                    HttpContext.Session.SetString("cpf", _retUsuario.dados.cpf);
                    return RedirectToAction("Index", "Home");
                } 
            }
            TempData["message"] = "Não foi possível realizar o login.";
            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

    }
}
