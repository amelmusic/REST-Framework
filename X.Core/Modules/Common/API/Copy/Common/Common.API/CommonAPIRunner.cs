using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;

[assembly: WebAPIGenerator(InterfacesPath = "../Common.Interfaces"
    , InterfacesNamespace = "Common.Interfaces"
    , ModelNamespace = "Common.Model"
    , WebAPINamespace = "Common.API")]
namespace Common.API
{
    public class CommonAPIRunner
    {
    }
}
