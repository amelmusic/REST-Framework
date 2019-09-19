using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;

[assembly: WebAPIGenerator(InterfacesPath = "../ApiTemplate.Interfaces"
    , InterfacesNamespace = "ApiTemplate.Interfaces"
    , ModelNamespace = "ApiTemplate.Model"
    , WebAPINamespace = "ApiTemplate.API")]
namespace ApiTemplate.API
{
    public class XCoreHelloAPIRunner
    {
    }
}
