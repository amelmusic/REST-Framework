using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CodeGeneration.Roslyn;

namespace X.Core.Generator.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    [CodeGenerationAttribute(typeof(DuplicateWithSuffixGenerator))]
    [Conditional("CodeGeneration")]
    public class DuplicateWithSuffixAttribute : Attribute
    {
        public DuplicateWithSuffixAttribute(string suffix)
        {
            //Requires.NotNullOrEmpty(suffix, nameof(suffix));

            this.Suffix = suffix;
        }

        public string Suffix { get; }
    }
}
