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
    public class InterfacesGenerator : BaseGenerator, ICodeGenerator
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
            if (File.Exists(".DEBUG"))
            {
                Debugger.Launch();
            }
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
            var stateMachineServices = await GenerateInterfaceMethodForStateMachine(root, context);
            results = results.AddRange(services);
            results = results.AddRange(stateMachineServices);
            return results;
        }

        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateInterfaceMethodForStateMachine(SyntaxNode root,
            TransformationContext context)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            //var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Services")).NormalizeWhitespace();
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(_interfacesNamespace))
                .NormalizeWhitespace();
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(_modelNamespace)));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(_modelNamespace + ".Requests")));

            //using System.Threading.Tasks;
            //using ApiTemplate.Model;
            //service = service.AddBaseListTypes(
            //    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("X.Core.Services.Core.BaseService")));

            //results = results.Add(service);
            //Debugger.Launch();

            var classes = GetClassesWithSpecificAttribute(root, "StateMachineModelGenerator");
            //foreach class generate new service
            foreach (var classDeclarationSyntax in classes)
            {
                var className = classDeclarationSyntax.Identifier.ValueText;
                var serviceName = $"I{classDeclarationSyntax.Identifier.ValueText}Service";
                var service = SyntaxFactory.InterfaceDeclaration(serviceName);
                var key = GetPropertiesWithSpecificAttribute(classDeclarationSyntax, "Key")?.FirstOrDefault();
                string keyType = key != null ? key.Type.ToFullString().Trim() : "int";
                if (key == null)
                {
                    throw new Exception($"Model: {classDeclarationSyntax} doesn't have property with [Key] attribute");
                }

                
                string keyName = key.Identifier.ToFullString().Trim();
                var path = GetDistinctArgumentValues(classDeclarationSyntax, "StateMachineModelGenerator", "StateMachineDefinitionPath")?.FirstOrDefault();
                path = path?.Trim().Trim('"');
                var propertyName = GetDistinctArgumentValues(classDeclarationSyntax, "StateMachineModelGenerator", "PropertyName")?.FirstOrDefault();
                propertyName = propertyName?.Trim().Trim('"');
                StateMachineDefinition stateMachineDefinition = new StateMachineDefinition();
                //stateMachineDefinition.Parse(_modelPath + "/" + path, classDeclarationSyntax.Identifier.ValueText);
                stateMachineDefinition.Parse(path, classDeclarationSyntax.Identifier.ValueText, _modelPath);

                StringBuilder stateMachineActionsBuilder = new StringBuilder();

                var distinctTransitions = stateMachineDefinition.StateMachineTransitions.Select(x => x.ActionName).Distinct().ToList();

                foreach (var transition in distinctTransitions)
                {
                    if (transition.Equals("insert", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //insert method
                        stateMachineActionsBuilder.AppendLine(
                            $@"[MethodBehaviour(Behaviour = BehaviourEnum.Insert)]Task<{classDeclarationSyntax.Identifier.ValueText}> {transition}({classDeclarationSyntax.Identifier.ValueText}{transition}Request request);");
                    }
                    else if (transition.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                    {
                        stateMachineActionsBuilder.AppendLine(
                            $@"[MethodBehaviour(Behaviour = BehaviourEnum.Delete)]Task<{classDeclarationSyntax.Identifier.ValueText}> {transition}({keyType} id, {classDeclarationSyntax.Identifier.ValueText}{transition}Request request);");
                    }
                    else
                    {
                        stateMachineActionsBuilder.AppendLine(
                            $@"[MethodBehaviour(Behaviour = BehaviourEnum.Update)]Task<{classDeclarationSyntax.Identifier.ValueText}> {transition}({keyType} id, {classDeclarationSyntax.Identifier.ValueText}{transition}Request request);");
                    }
                }

                stateMachineActionsBuilder.AppendLine(
                    $@"[MethodBehaviour(Behaviour = BehaviourEnum.GetById)]Task<List<{classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum>> AllowedActionsAsync({keyType} id);");

                SyntaxTree tree = CSharpSyntaxTree.ParseText(stateMachineActionsBuilder.ToString());
                var newCodeRoot = (CompilationUnitSyntax)tree.GetRoot();
                service = service.AddMembers(newCodeRoot.Members.ToArray());

                service = service.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));

                @namespace = @namespace.AddMembers(service);
            }

            results = results.Add(@namespace);
            return results;
        }

        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateInterfaces(SyntaxNode root, TransformationContext context)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            //var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Services")).NormalizeWhitespace();
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(_interfacesNamespace)).NormalizeWhitespace();

            var classes = GetClassesWithSpecificAttribute(root, "ModelGenerator");
            //foreach class generate new service
            foreach (var classDeclarationSyntax in classes)
            {
                var key = GetPropertiesWithSpecificAttribute(classDeclarationSyntax, "Key")?.FirstOrDefault();
                string keyType = key != null ? key.Type.ToFullString().Trim() : "int";
                if (key == null)
                {
                    throw new Exception($"Model: {classDeclarationSyntax} doesn't have property with [Key] attribute");
                }

                string keyName = key.Identifier.ToFullString().Trim();

                var className = classDeclarationSyntax.Identifier.ValueText;
                var serviceName = $"I{classDeclarationSyntax.Identifier.ValueText}Service";
                var service = SyntaxFactory.InterfaceDeclaration(serviceName);
                service = service.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));

                var behaviour = GetDistinctArgumentValues(classDeclarationSyntax, "ModelGenerator", "Behaviour")?.FirstOrDefault();
                var resourceName = GetDistinctArgumentValues(classDeclarationSyntax, "ModelGenerator", "ResourceName")?.FirstOrDefault();
                var internalFlag = GetDistinctArgumentValues(classDeclarationSyntax, "ModelGenerator", "Internal")?.FirstOrDefault();
                
                var internalFlagArg = SyntaxFactory.AttributeArgument(
                                           SyntaxFactory.LiteralExpression(
                                               SyntaxKind.FalseLiteralExpression))
                                       .WithNameEquals(
                                           SyntaxFactory.NameEquals(
                                               SyntaxFactory.IdentifierName("Internal")));

               
                if (internalFlag != null && internalFlag.Equals("true", StringComparison.OrdinalIgnoreCase)){
                    internalFlagArg = SyntaxFactory.AttributeArgument(
                                           SyntaxFactory.LiteralExpression(
                                               SyntaxKind.TrueLiteralExpression))
                                       .WithNameEquals(
                                           SyntaxFactory.NameEquals(
                                               SyntaxFactory.IdentifierName("Internal")));
                }



               

                if (resourceName == null)
                {
                    resourceName = className;
                }

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
                else if (behaviour == "EntityBehaviourEnum.CRUDAsUpload")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.Interface.ICRUDService<{_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_modelNamespace}.Requests.{className}InsertRequest, {_modelNamespace}.Requests.{className}UpdateRequest>")));
                }


                service = service.WithAttributeLists(
                    SyntaxFactory.SingletonList<AttributeListSyntax>(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                                SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("Service"))
                                    .WithArgumentList(
                                        SyntaxFactory.AttributeArgumentList(
                                            SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(
                                                new SyntaxNodeOrToken[]
                                                {
                                                    SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.ParseExpression(behaviour))
                                                        .WithNameEquals(
                                                            SyntaxFactory.NameEquals(
                                                                SyntaxFactory.IdentifierName("Behaviour"))),
                                                    SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                    SyntaxFactory.AttributeArgument(
                                                            SyntaxFactory.LiteralExpression(
                                                                SyntaxKind.StringLiteralExpression,
                                                                SyntaxFactory.Literal(resourceName)))
                                                        .WithNameEquals(
                                                            SyntaxFactory.NameEquals(
                                                                SyntaxFactory.IdentifierName("ResourceName"))),
                                                    SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                    internalFlagArg
                                                })))))));

                service = service.AddAttributeLists(SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                        SyntaxFactory.Attribute(
                                SyntaxFactory.IdentifierName("Metadata"))
                            .WithArgumentList(
                                SyntaxFactory.AttributeArgumentList(
                                    SyntaxFactory.SeparatedList<AttributeArgumentSyntax>(
                                        new SyntaxNodeOrToken[]
                                        {
                                            SyntaxFactory.AttributeArgument(
                                                    SyntaxFactory.LiteralExpression(
                                                        SyntaxKind.StringLiteralExpression,
                                                        SyntaxFactory.Literal(keyName)))
                                                .WithNameEquals(
                                                    SyntaxFactory.NameEquals(
                                                        SyntaxFactory.IdentifierName("Key"))),
                                            SyntaxFactory.Token(SyntaxKind.CommaToken),
                                            SyntaxFactory.AttributeArgument(
                                                    SyntaxFactory.LiteralExpression(
                                                        SyntaxKind.StringLiteralExpression,
                                                        SyntaxFactory.Literal(keyType)))
                                                .WithNameEquals(
                                                    SyntaxFactory.NameEquals(
                                                        SyntaxFactory
                                                            .IdentifierName("KeyType")))
                                        }))))));

                @namespace = @namespace.AddMembers(service);
            }

            results = results.Add(@namespace);
            return results;
        }
    }
}
