using System;
using System.Collections.Generic;
using System.Text;
using X.Core.Generator.Attributes;

[assembly: ServicesGenerator(ServiceType = ServiceTypeEnum.EntityFramework, ModelPath = "../PermissionModule.Model"
    , EntityFrameworkContextName = "PermissionModuleContext"
    , EntityFrameworkContextNamespace = "PermissionModule.Services.Database"
    , ModelNamespace = "PermissionModule.Model"
    , ServicesNamespace = "PermissionModule.Services")]
namespace PermissionModule.Services
{
    public class XCoreHelloServicesRunner
    {
    }
}
