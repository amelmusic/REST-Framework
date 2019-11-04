using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;

[assembly: WebAPIGenerator(InterfacesPath = "../PermissionModule.Interfaces"
    , InterfacesNamespace = "PermissionModule.Interfaces"
    , ModelNamespace = "PermissionModule.Model"
    , WebAPINamespace = "PermissionModule.API")]
namespace PermissionModule.API
{
    public class XCoreHelloAPIRunner
    {
    }
}
