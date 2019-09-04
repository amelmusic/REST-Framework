using System;
using System.Diagnostics;
using CodeGeneration.Roslyn;


namespace X.Core.Generator.Attributes
{
    /// <summary>
    /// Serves as entry point for generating requests, search objects etc
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    [CodeGenerationAttribute(typeof(ModelGenerator))]
    //[CodeGenerationAttribute("X.Core.Model.Generators.ModelGenerator, X.Core.Model.Generators")]
    [Conditional("CodeGeneration")]
    public class ModelGeneratorAttribute : EntityAttribute
    {

    }
}
