using MvcApp.Business.Interfaces.Service;
using MvcApp.Controllers.Aspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    [ManageSessionController]
    public class HomeController : Controller
    {
        public IUsuarioService UsuarioService { get; set; }

        public ActionResult Index()
        {
            var email = "";
            var clave = "";

            var usuario = UsuarioService.Login(email, clave);

            return View();
        }

       
    }
}
