using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeGeneration.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace X.Core.Generator
{
    public class InterfacesGenerator : ICodeGenerator
    {
        private readonly AttributeData _attributeData;
        private string _modelPath;
        private string _modelNamespace;
        private string _interfacesNamespace;

        public InterfacesGenerator(AttributeData attributeData)
        {
            //Requires.NotNull(attributeData, nameof(attributeData));
            this._attributeData = attributeData;

            var mappedTo = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "ModelPath");
            string mappedToStr = mappedTo.Key == null ? null : mappedTo.Value.Value.ToString();
            _modelPath = mappedToStr;

            var modelNamespace = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "ModelNamespace");
            _modelNamespace = modelNamespace.Key == null ? "Model" : modelNamespace.Value.Value.ToString();

            var interfacesNamespace = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "InterfacesNamespace");
            _interfacesNamespace = interfacesNamespace.Key == null ? null : interfacesNamespace.Value.Value.ToString();
        }

        public async Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context,
            IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            //results.add
            //Debugger.Launch();


            var dir = Directory.GetCurrentDirectory();
            var modelDir = dir + "/" + _modelPath;
            var files = Directory.GetFiles(modelDir, "*.cs", SearchOption.AllDirectories);
            StringBuilder code = new StringBuilder();
            foreach (var file in files)
            {
                var text = File.ReadAllText(file);
                code.AppendLine(text);
            }

            var tree = SyntaxFactory.ParseSyntaxTree(code.ToString());
            var root = tree.GetRoot();

            var services = await GenerateInterfaces(root, context);
            results = results.AddRange(services);
            return results;
        }


        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateInterfaces(SyntaxNode root, TransformationContext context)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            //var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Services")).NormalizeWhitespace();
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(_interfacesNamespace)).NormalizeWhitespace();

            //service = service.AddBaseListTypes(
            //    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("X.Core.Services.Core.BaseService")));

            //results = results.Add(service);
            //Debugger.Launch();
            var classes = GetClassesWithSpecificAttribute(root, "ModelGenerator");
            //foreach class generate new service
            foreach (var classDeclarationSyntax in classes)
            {
                var className = classDeclarationSyntax.Identifier.ValueText;
                var serviceName = $"I{classDeclarationSyntax.Identifier.ValueText}Service";
                var service = SyntaxFactory.InterfaceDeclaration(serviceName);
                service = service.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));

                var behaviour = GetDistinctArgumentValues(classDeclarationSyntax, "ModelGenerator", "Behaviour")?.FirstOrDefault();

                if (behaviour == "EntityBehaviourEnum.Empty")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("X.Core.Interface.IService")));
                }
                else if (behaviour == "EntityBehaviourEnum.Read")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.Interface.IReadService<{_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData>")));
                }
                else if (behaviour == "EntityBehaviourEnum.CRUD")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.Interface.ICRUDService<{_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_modelNamespace}.Requests.{className}InsertRequest, {_modelNamespace}.Requests.{className}UpdateRequest>")));
                }
                else if (behaviour == "EntityBehaviourEnum.CRUDAsUpsert")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.Interface.ICRUDService<{_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_modelNamespace}.Requests.{className}UpsertRequest, {_modelNamespace}.Requests.{className}UpsertRequest>")));
                }

                service = service.WithAttributeLists(
                    SyntaxFactory.SingletonList<AttributeListSyntax>(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                                SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("Service"))
                                    .WithArgumentList(
                                        SyntaxFactory.AttributeArgumentList(
                                            SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>(
                                                SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.ParseExpression(behaviour))
                                                    .WithNameEquals(
                                                        SyntaxFactory.NameEquals(
                                                            SyntaxFactory.IdentifierName("Behaviour"))))))))));

                @namespace = @namespace.AddMembers(service);
            }

            results = results.Add(@namespace);
            return results;
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

    }
}
