using System;
using System.Collections.Generic;
using System.Text;
using X.Core.Generator.Attributes;

[assembly: ServicesGenerator(ServiceType = ServiceTypeEnum.EntityFramework, ModelPath = "../ApiTemplate.Model"
    , EntityFrameworkContextName = "ApiTemplate"
    , EntityFrameworkContextNamespace = "ApiTemplate.Services.Database"
    , ModelNamespace = "ApiTemplate.Model"
    , ServicesNamespace = "ApiTemplate.Services")]
namespace ApiTemplate.Services
{
    public class XCoreHelloServicesRunner
    {
    }
}
