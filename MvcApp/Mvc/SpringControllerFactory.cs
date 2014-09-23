using Common.Logging;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;

namespace MvcApp.Mvc
{
    public class SpringControllerFactory : DefaultControllerFactory
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(SpringControllerFactory));
        private static readonly IApplicationContext SpringContext = ContextRegistry.GetContext();

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType != null)
                {
                    var objectsOfType = SpringContext.GetObjectsOfType(controllerType);
                    if (objectsOfType.Count > 0)
                    {
                        return (IController)objectsOfType.Cast<DictionaryEntry>().First<DictionaryEntry>().Value;
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Log.Error("SpringControllerFactory: " + ex.Message);
            }

            return null;
        }        
    }
}