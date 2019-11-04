using System;
using System.Collections.Generic;
using System.Text;
using X.Core.Generator.Attributes;

[assembly: ServicesGenerator(ServiceType = ServiceTypeEnum.EntityFramework, ModelPath = "../X.Core.PermissionModule.Model"
    , EntityFrameworkContextName = "X.Core.PermissionModuleContext"
    , EntityFrameworkContextNamespace = "X.Core.PermissionModule.Services.Database"
    , ModelNamespace = "X.Core.PermissionModule.Model"
    , ServicesNamespace = "X.Core.PermissionModule.Services")]
namespace X.Core.PermissionModule.Services
{
    public class XCoreHelloServicesRunner
    {
    }
}
