using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiClient.Utility.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task WriteResultAsync(this HttpContext context, ViewResult result)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var viewDataDictionary = new ViewDataDictionary(
                                            new EmptyModelMetadataProvider(),
                                            new ModelStateDictionary());
            viewDataDictionary.Model = new AcessoNegadoViewModel();


            var executor = context.RequestServices.GetRequiredService<IActionResultExecutor<ViewResult>>();
            var routeData = context.GetRouteData() ?? new RouteData();
            var actionContext = new ActionContext(context, routeData,
            new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

            await executor.ExecuteAsync(actionContext, result);
        }
    }
}