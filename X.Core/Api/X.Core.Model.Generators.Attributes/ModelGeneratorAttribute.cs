using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CodeGeneration.Roslyn;

using X.Core.Attributes;

namespace X.Core.Model.Generators.Attributes
{
    /// <summary>
    /// Serves as entry point for generating requests, search objects etc
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    //[CodeGenerationAttribute(typeof(ModelGenerator))]
    [CodeGenerationAttribute("X.Core.Model.Generators.ModelGenerator, X.Core.Model.Generators")]
    [Conditional("CodeGeneration")]
    public class ModelGeneratorAttribute : EntityAttribute
    {

    }
}
