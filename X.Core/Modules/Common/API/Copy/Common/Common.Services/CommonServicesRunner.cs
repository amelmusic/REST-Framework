using System;
using System.Collections.Generic;
using System.Text;
using X.Core.Generator.Attributes;

[assembly: ServicesGenerator(ServiceType = ServiceTypeEnum.EntityFramework, ModelPath = "../Common.Model"
    , EntityFrameworkContextName = "CommonContext"
    , EntityFrameworkContextNamespace = "Common.Services.Database"
    , ModelNamespace = "Common.Model"
    , ServicesNamespace = "Common.Services")]
namespace Common.Services
{
    public class CommonServicesRunner
    {
    }
}
