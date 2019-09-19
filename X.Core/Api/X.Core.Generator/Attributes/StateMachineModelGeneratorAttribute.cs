using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CodeGeneration.Roslyn;

namespace X.Core.Generator.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    [CodeGenerationAttribute(typeof(StateMachineModelGenerator))]
    //[CodeGenerationAttribute("X.Core.Model.Generators.ModelGenerator, X.Core.Model.Generators")]
    [Conditional("CodeGeneration")]
    public class StateMachineModelGeneratorAttribute : Attribute
    {
        public string StateMachineDefinitionPath { get; set; }
        public string PropertyName { get; set; }
        //public string PropertyType { get; set; }
    }
}
