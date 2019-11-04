using System;
using System.Collections.Generic;
using System.Text;
using X.Core.Generator.Attributes;

[assembly: InterfacesGenerator(ModelPath = "../PermissionModule.Model"
    , InterfacesNamespace = "PermissionModule.Interfaces"
    , ModelNamespace = "PermissionModule.Model")]
namespace PermissionModule.Interfaces
{
    class XCoreHelloInterfaceRunner
    {
    }
}
