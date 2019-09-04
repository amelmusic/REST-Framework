﻿using System;
using System.Diagnostics;
using CodeGeneration.Roslyn;

namespace X.Core.Generator.Attributes
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
