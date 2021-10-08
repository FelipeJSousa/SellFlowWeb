using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SellFlowWeb.Controllers
{
    public class BaseController : Controller
    {
        protected BaseController(IServiceProvider serviceProvider)
        {
        }

    }
}
