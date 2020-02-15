using System;
using System.Collections.Generic;
using System.Text;
using X.Core.Generator.Attributes;

[assembly: InterfacesGenerator(ModelPath = "../Common.Model"
    , InterfacesNamespace = "Common.Interfaces"
    , ModelNamespace = "Common.Model")]
namespace Common.Interfaces
{
    class CommonInterfaceRunner
    {
    }
}
