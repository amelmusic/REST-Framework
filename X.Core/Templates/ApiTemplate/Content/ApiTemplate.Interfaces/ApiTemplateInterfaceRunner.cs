using System;
using System.Collections.Generic;
using System.Text;
using X.Core.Generator.Attributes;

[assembly: InterfacesGenerator(ModelPath = "../ApiTemplate.Model"
    , InterfacesNamespace = "ApiTemplate.Interfaces"
    , ModelNamespace = "ApiTemplate.Model")]
namespace ApiTemplate.Interfaces
{
    class ApiTemplateInterfaceRunner
    {
    }
}
