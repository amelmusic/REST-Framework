using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;

[assembly: WebAPIGenerator(InterfacesPath = "../X.Core.PermissionModule.Interfaces"
    , InterfacesNamespace = "X.Core.PermissionModule.Interfaces"
    , ModelNamespace = "X.Core.PermissionModule.Model"
    , WebAPINamespace = "X.Core.PermissionModule.API")]
namespace X.Core.PermissionModule.API
{
    public class XCoreHelloAPIRunner
    {
    }
}
