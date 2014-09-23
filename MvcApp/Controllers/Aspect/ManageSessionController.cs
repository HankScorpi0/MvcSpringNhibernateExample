using IrisLib.Architecture.Interface.Service;
using MvcApp.Business.Data.Conf;
using System.Linq;
using System.Web.Mvc;

namespace MvcApp.Controllers.Aspect
{
    /// <summary>
    /// Clase que actua como un aspecto cuando un controlador lanza una acción.
    /// Tiene como finalidad asignarle la sesión actual a los servicios que pueda tener el controlador.
    /// </summary>
    public class ManageSessionController : ActionFilterAttribute
    {
        /// <summary>
        /// Método que se lanza antes que se ejecute la acción del controlador.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var properties = filterContext.Controller.GetType().GetProperties();
            foreach (var item in properties)
            {
                //Si implementa la interfaz IService le asignamos la sesión.
                var isService = item.GetValue(filterContext.Controller, null).GetType().GetInterfaces().Any(x => x.Name.Equals(typeof(IService).Name));
                if (!isService) continue;

                var service = (IService)item.GetValue(filterContext.Controller, null);
                service.SessionFactory = NHibernateSessionPerRequest.GetSessionFactory();
            }
        }
    }
}