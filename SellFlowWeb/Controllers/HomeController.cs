using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellFlowWeb.Models;
using System.Diagnostics;
using System;
using ApiClient.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using SellFlowWeb.Models.DataView;
using System.Collections.Generic;

namespace SellFlowWeb.Controllers
{
    public class HomeController : BaseController
    {
        private IAnuncioClient _anuncioClient;
        public HomeController(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
            _anuncioClient = serviceProvider.GetService<IAnuncioClient>();
        }

        public async Task<IActionResult> Index()
        {
            var redirect = VerificarLogin();
            if (redirect is null)
            {
                var obj = await _anuncioClient.GetPublicados();
                if (obj.status)
                {
                    var anuncioList = new Mapper(AutoMapperConfig.RegisterMappings()).Map<List<AnuncioDataView>>(obj.dados);
                    return View(anuncioList);
                }
                return View(new List<AnuncioDataView>());
            }

            return redirect;

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
