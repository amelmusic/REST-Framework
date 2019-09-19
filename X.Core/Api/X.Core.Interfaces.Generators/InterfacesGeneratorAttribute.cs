using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CodeGeneration.Roslyn;
using X.Core.Attributes;

namespace X.Core.Interfaces.Generators
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
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
