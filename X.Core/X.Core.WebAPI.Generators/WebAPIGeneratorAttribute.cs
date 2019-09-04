using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CodeGeneration.Roslyn;
using X.Core.Attributes;

namespace X.Core.WebAPI.Generators
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    [CodeGenerationAttribute(typeof(WebAPIGeneratorGenerator))]
    [Conditional("CodeGeneration")]
    public class WebAPIGeneratorAttribute : EntityAttribute
    {
        /// <summary>
        /// Looks up in specific location for location project. For eg: "../Model" means PWD and reroute to model
        /// </summary>
        public string InterfacesPath { get; set; }

        public string InterfacesNamespace { get; set; }

        public string WebAPINamespace { get; set; }

        public string ModelNamespace { get; set; }
    }
}
