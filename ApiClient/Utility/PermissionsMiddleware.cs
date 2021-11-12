using ApiClient.Interfaces;
using ApiClient.Utility.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiClient.Utility
{
    public class PermissionsMiddleware : IPermissionsMiddleware
    {
        private readonly IPermissaoPaginaClient _permissionService;

        public PermissionsMiddleware(IPermissaoPaginaClient permissionService)
        {
            this._permissionService = permissionService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            List<string> exceptions = new List<string>() { "/Login/Auth", "/Home/Index", "/Login/Logout"} ;
            var splittedPath = context.Request.Path.ToString().Split("/");
            var path = "/" + splittedPath[1]; 
            if(splittedPath.Length > 2)
            {
                splittedPath[2] = splittedPath[2] == "Salvar" ? "Criar" : splittedPath[2];
                path += splittedPath[2] != "Index" ? "/" + splittedPath[2] ?? "" : "";
            }

            bool isValid = (await _permissionService.ValidarPermissao(path, context.Session.GetInt32("idpermissao") ?? 2))?.status == true;
            if(exceptions.Exists(x => x == path))
            { 
                isValid = true;
            }
            if (!isValid)
            {
                var result = new ViewResult()
                {
                    ViewName = "~/Views/Shared/AcessoNegado.cshtml",
                };

                await context.WriteResultAsync(result);
            }
            else
            {
                await next(context);
            }
        }

    }
    public static class PermissionsMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermissionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermissionsMiddleware>();
        }
    }
}
