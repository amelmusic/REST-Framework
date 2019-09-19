//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using CodeGeneration.Roslyn;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Validation;
//using X.Core.Attributes;

//namespace X.Core.Model.Generators
//{
//    /// <summary>
//    /// Serves as entry point for generating requests, search objects etc
//    /// </summary>
//    public class ModelGenerator : ICodeGenerator
//    {
//        private readonly AttributeData _attributeData;
//        private EntityBehaviourEnum _behaviour;

//        public ModelGenerator(AttributeData attributeData)
//        {
//            Requires.NotNull(attributeData, nameof(attributeData));
//            this._attributeData = attributeData;

//            //var mappedTo = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "MapTo");
//            //string mappedToStr = mappedTo.Key == null ? null : mappedTo.Value.Value.ToString();
//        }

//        public async Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context,
//            IProgress<Diagnostic> progress, CancellationToken cancellationToken)
//        {
//            var results = SyntaxFactory.List<MemberDeclarationSyntax>();

//            var behaviour = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "Behaviour");
//            _behaviour = (EntityBehaviourEnum)behaviour.Value.Value;
//            //Here we will generate requests and search objects


//            //// Our generator is applied to any class that our attribute is applied to.
//            //var applyToClass = (ClassDeclarationSyntax)context.ProcessingNode;

//            //// Apply a suffix to the name of a copy of the class.
//            //var copy = applyToClass
//            //    .WithIdentifier(SyntaxFactory.Identifier(applyToClass.Identifier.ValueText + this.suffix));

//            //// Return our modified copy. It will be added to the user's project for compilation.
//            //results = results.Add(copy);
//            results = results.AddRange(await GenerateRequests(context));
//            return results;
//        }

//        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateRequests(TransformationContext context)
//        {
//            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
//            var applyToClass = (ClassDeclarationSyntax) context.ProcessingNode;

//            StringBuilder requestBuilder = new StringBuilder();
//            Dictionary<string, List<string>> requests = new Dictionary<string, List<string>>();
//            var root = await applyToClass.SyntaxTree.GetRootAsync();
//            var nmspc = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
            
//            //INamedTypeSymbol simpleClassToAnalyze =
//            //    context.Compilation.GetTypeByMetadataName("X.Core.Model." + applyToClass.Identifier.ValueText);
//            //var name = simpleClassToAnalyze.ContainingNamespace.Name;
//            //requestBuilder.AppendLine($"namespace {nmspc.Name.ToString()} {{ \n");
//            requestBuilder.AppendLine($"namespace Requests {{ \n");

            
//            //if (_behaviour == EntityBehaviourEnum.CRUD)
//            //{
//            //    //we are sure that we need at least Insert / Update
//            //    requests.Add("");
//            //}
//            //applyToClass
//            var distinctArgValues = GetDistinctArgumentValues(applyToClass, "RequestField", "RequestName");
//            if (_behaviour == EntityBehaviourEnum.CRUD)
//            {
//                if (!distinctArgValues.Contains("Insert"))
//                {
//                    distinctArgValues.Add("Insert");
//                }

//                if (!distinctArgValues.Contains("Update"))
//                {
//                    distinctArgValues.Add("Update");
//                }
//            }

//            //Debugger.Launch();
//            foreach (var distinctArgValue in distinctArgValues)
//            {
//                requestBuilder.AppendLine($"\t public partial class {applyToClass.Identifier.ValueText}{distinctArgValue.Trim('"')}Request {{ \n");
//                var requestProps = GetPropertiesWithAttributeAndArgumentValue(applyToClass, "RequestField", "RequestName",
//                    distinctArgValue);
//                foreach (var propertyDeclarationSyntax in requestProps)
//                {
//                    var propType = propertyDeclarationSyntax.Type.ToFullString();
//                    requestBuilder.AppendLine($"\t\tpublic {propType} {propertyDeclarationSyntax.Identifier.ValueText} {{ get; set; }}");
//                }
//                requestBuilder.AppendLine("\t}");
//            }

//            //var props = GetPropertiesWithSpecificAttribute(applyToClass, "RequestField");
//            //foreach (var propertyDeclarationSyntax in props)
//            //{
//            //    var attr = GetAttributes(propertyDeclarationSyntax, "RequestField");
//            //    foreach (var attributeSyntax in attr)
//            //    {
//            //        var val = GetAttributeArgumentValue(attributeSyntax, "RequestName");
//            //    }
                
//            //}
//            requestBuilder.AppendLine("} //END NAMESPACE");

//            SyntaxTree tree = CSharpSyntaxTree.ParseText(requestBuilder.ToString());
//            var newCodeRoot = (CompilationUnitSyntax)tree.GetRoot();
//            results = results.AddRange(newCodeRoot.Members);
//            return results;
//        }

//        protected IList<PropertyDeclarationSyntax> GetPropertiesWithSpecificAttribute(MemberDeclarationSyntax document, string attributeName)
//        {
//            var filteredProps = document.DescendantNodes()
//                .OfType<PropertyDeclarationSyntax>()
//                .Where(y => y.AttributeLists.Any(z => z.Attributes.Any(u => u.Name.ToFullString() == attributeName)))
//                .ToList();

//            return filteredProps;
//        }

//        protected IList<AttributeSyntax> GetAttributes(MemberDeclarationSyntax member, string attributeName)
//        {
//            //var prop = member
//            //    .DescendantNodes()
//            //    .OfType<PropertyDeclarationSyntax>()
//            //    .SelectMany(y => y.AttributeLists)
//            //    .SelectMany(y => y.Attributes)
//            //    .ToList();

//            //var names = prop.Select(y => y.Name.ToFullString()).ToList();
//            var argumentvalue = member.DescendantNodes().OfType<AttributeSyntax>()
//                .Where(x=>x.Name.ToFullString() == attributeName).ToList();

//            return argumentvalue;
//        }

//        protected string GetAttributeArgumentValue(AttributeSyntax attribute, string argumentName)
//        {
//            var args = attribute.ArgumentList.Arguments
//                .FirstOrDefault(x => x.NameEquals.Name.Identifier.ValueText == argumentName)
//                ?.Expression.ToFullString()?.Trim();

//            return args;
//        }

//        protected IList<string> GetDistinctArgumentValues(MemberDeclarationSyntax member,
//            string attributeName, string argumentName)
//        {

//            var filteredProps = member.DescendantNodes()
//                .OfType<AttributeSyntax>()
//                .Where(u => u.Name.ToFullString() == attributeName)
//                .SelectMany(x => x.ArgumentList.Arguments)
//                .Where(x => x.NameEquals.Name.Identifier.ValueText == argumentName)
//                .Select(x => x.Expression.ToFullString()).Distinct().ToList();

//            return filteredProps;
//        }

//        protected IList<PropertyDeclarationSyntax> GetPropertiesWithAttributeAndArgumentValue(
//            MemberDeclarationSyntax member,
//            string attributeName, string argumentName, string argumentValue)
//        {
//            var filteredProps = member.DescendantNodes()
//                .OfType<PropertyDeclarationSyntax>()
//                .Where(y => y.AttributeLists.Any(z => z.Attributes.Any(u => u.Name.ToFullString() == attributeName 
//                                                                            && u.ArgumentList.Arguments.Any(i => i.NameEquals.Name.Identifier.ValueText == argumentName && i.Expression.ToFullString() == argumentValue)
//                                                                            )))

//                .ToList();

//            return filteredProps;
//        }
//    }
//}
