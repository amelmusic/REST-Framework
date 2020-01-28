using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ApiTemplate.API
{
    public static class MvcOptionsExtensions
    {
        public static void UseGeneralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute, Assembly controllerAssembly)
        {
            opts.Conventions.Add(new RoutePrefixConvention(routeAttribute, controllerAssembly));
        }

        public static void UseGeneralRoutePrefix(this MvcOptions opts, string prefix, Assembly controllerAssembly)
        {
            opts.UseGeneralRoutePrefix(new RouteAttribute(prefix), controllerAssembly);
        }
    }

    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _routePrefix;
        private readonly Assembly _controllerAssembly;

        public RoutePrefixConvention(IRouteTemplateProvider route, Assembly controllerAssembly)
        {
            _routePrefix = new AttributeRouteModel(route);
            _controllerAssembly = controllerAssembly;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var selector in application.Controllers
                .Where(x => x.ControllerType.Assembly.FullName == _controllerAssembly.FullName)
                .SelectMany(c => c.Selectors))
            {
                if (selector.AttributeRouteModel != null)
                {
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                }
                else
                {
                    selector.AttributeRouteModel = _routePrefix;
                }
            }
        }
    }
}
