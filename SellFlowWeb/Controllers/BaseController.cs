﻿using Microsoft.AspNetCore.Http;
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

        public IActionResult VerificarLogin()
        {
            if (!HttpContext.Session.GetInt32("idusuario").HasValue)
            {
                return RedirectToAction("Index", "Login");
            }
            return null;
        }

    }
}
