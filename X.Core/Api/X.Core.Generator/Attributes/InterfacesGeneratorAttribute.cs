using System;
using System.Diagnostics;
using CodeGeneration.Roslyn;

namespace X.Core.Generator.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    [CodeGenerationAttribute(typeof(InterfacesGenerator))]
    [Conditional("CodeGeneration")]
    public class InterfacesGeneratorAttribute : Attribute
    {
        /// <summary>
        /// Looks up in specific location for location project. For eg: "../Model" means PWD and reroute to model
        /// </summary>
        public string ModelPath { get; set; }

        public string ModelNamespace { get; set; }

        public string InterfacesNamespace { get; set; }
    }
}
