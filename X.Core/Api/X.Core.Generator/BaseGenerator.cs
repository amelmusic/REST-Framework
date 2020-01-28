using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace X.Core.Generator
{
    public class BaseGenerator
    {
        public TypedConstantKind GetTypedConstantKind(string type)
        {
            Debug.Assert(type != null);
            if (type.EndsWith("?"))
            {
                return TypedConstantKind.Type;
            }
            switch (type.ToLower())
            {
                case "string":
                    return TypedConstantKind.Type;
                default:
                    return TypedConstantKind.Primitive;
            }
        }
        protected IList<ClassDeclarationSyntax> GetClassesWithSpecificAttribute(SyntaxNode document, string attributeName)
        {
            var filteredProps = document.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Where(y => y.AttributeLists.Any(z => z.Attributes.Any(u => u.Name.ToFullString() == attributeName)))
                .ToList();

            return filteredProps;
        }

        protected IList<string> GetDistinctArgumentValues(MemberDeclarationSyntax member,
            string attributeName, string argumentName)
        {

            var filteredProps = member.DescendantNodes()
                .OfType<AttributeSyntax>()
                .Where(u => u.Name.ToFullString() == attributeName)
                .SelectMany(x => x.ArgumentList.Arguments)
                .Where(x => x.NameEquals.Name.Identifier.ValueText == argumentName)
                .Select(x => x.Expression.ToFullString()).Distinct().ToList();

            return filteredProps;
        }

        protected IList<PropertyDeclarationSyntax> GetPropertiesWithSpecificAttribute(MemberDeclarationSyntax document, string attributeName)
        {
            var filteredProps = document.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(y => y.AttributeLists.Any(z => z.Attributes.Any(u => u.Name.ToFullString() == attributeName)))
                .ToList();

            return filteredProps;
        }

        protected PropertyDeclarationSyntax GetPropertiesWithSpecificName(MemberDeclarationSyntax document, string name)
        {
            var filteredProps = document
                .DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .FirstOrDefault(y => y.Identifier.ValueText == name);

            return filteredProps;
        }


        protected IList<AttributeSyntax> GetAttributes(MemberDeclarationSyntax member, string attributeName)
        {
            //var prop = member
            //    .DescendantNodes()
            //    .OfType<PropertyDeclarationSyntax>()
            //    .SelectMany(y => y.AttributeLists)
            //    .SelectMany(y => y.Attributes)
            //    .ToList();

            //var names = prop.Select(y => y.Name.ToFullString()).ToList();
            var argumentvalue = member.DescendantNodes().OfType<AttributeSyntax>()
                .Where(x => x.Name.ToFullString() == attributeName).ToList();

            return argumentvalue;
        }

        protected string GetAttributeArgumentValue(AttributeSyntax attribute, string argumentName)
        {
            var args = attribute.ArgumentList.Arguments
                .FirstOrDefault(x => x.NameEquals.Name.Identifier.ValueText == argumentName)
                ?.Expression.ToFullString()?.Trim();

            return args;
        }
    }
}
