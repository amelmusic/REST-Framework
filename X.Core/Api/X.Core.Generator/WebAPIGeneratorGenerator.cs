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
    public class WebAPIGeneratorGenerator : ICodeGenerator
    {
        private readonly AttributeData _attributeData;
        private string _interfacesPath;
        private string _interfacesNamespace;
        private string _webAPINamespace;
        private string _modelNamespace;

        public WebAPIGeneratorGenerator(AttributeData attributeData)
        {
            //Requires.NotNull(attributeData, nameof(attributeData));
            this._attributeData = attributeData;

            var mappedTo = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "InterfacesPath");
            string mappedToStr = mappedTo.Key == null ? null : mappedTo.Value.Value.ToString();
            _interfacesPath = mappedToStr;

            var interfacesNamespace = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "InterfacesNamespace");
            _interfacesNamespace = interfacesNamespace.Key == null ? "Interfaces" : interfacesNamespace.Value.Value.ToString();

            var webAPINamespace = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "WebAPINamespace");
            _webAPINamespace = interfacesNamespace.Key == null ? null : webAPINamespace.Value.Value.ToString();

            var modelNamespace = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "ModelNamespace");
            _modelNamespace = modelNamespace.Key == null ? "Model" : modelNamespace.Value.Value.ToString();
        }

        public async Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            if (File.Exists(".DEBUG"))
            {
                Debugger.Launch();
            }
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            //results.adds
            //Debugger.Launch();

            var dir = Directory.GetCurrentDirectory();
            var modelDir = dir + "/" + _interfacesPath;
            var files = Directory.GetFiles(modelDir, "*.cs", SearchOption.AllDirectories);
            StringBuilder code = new StringBuilder();
            foreach (var file in files)
            {
                var text = File.ReadAllText(file);
                code.AppendLine(text);
            }

            var tree = SyntaxFactory.ParseSyntaxTree(code.ToString());
            var root = tree.GetRoot();
           
            var services = await GenerateControllers(root, context, cancellationToken);
            results = results.AddRange(services);
            return results;
        }

        private async Task<IEnumerable<MemberDeclarationSyntax>> GenerateControllers(SyntaxNode root, TransformationContext context, CancellationToken cancellationToken)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            //var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Services")).NormalizeWhitespace();
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName($"{_webAPINamespace}.Controllers")).NormalizeWhitespace();
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Microsoft.AspNetCore.Mvc")));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(_modelNamespace)));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(_modelNamespace + ".Requests")));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")));


            var interfaces = GetTypesWithSpecificAttribute<InterfaceDeclarationSyntax>(root, "Service");

            foreach (var classDeclarationSyntax in interfaces)
            {
                var internalFlag = GetDistinctArgumentValues(classDeclarationSyntax, "Service", "Internal")?.FirstOrDefault();
                if (internalFlag != null && internalFlag.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                var allWithSameName = root.DescendantNodes().OfType<InterfaceDeclarationSyntax>().Where(x =>
                    x.Identifier.ValueText == classDeclarationSyntax.Identifier.ValueText).ToList();


                cancellationToken.ThrowIfCancellationRequested();
                var className = classDeclarationSyntax.Identifier.ValueText.Substring(1)
                    .Substring(0, classDeclarationSyntax.Identifier.ValueText.Length - 8);
                    //.Replace("Service", "");

                var controllerName = className + "Controller";

                var controller = SyntaxFactory.ClassDeclaration(controllerName);

                controller = GenerateConstructorMethods(controllerName, classDeclarationSyntax, controller);

                controller = controller.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));
                
                //string keyType = key != null ? key.Type.ToFullString().Trim() : "int";
                //if (key == null)
                //{
                //    throw new Exception($"Model: {className} doesn't have property with [Key] attribute");
                //}
                
                string keyType = GetDistinctArgumentValues(classDeclarationSyntax, "Metadata", "KeyType")
                    ?.FirstOrDefault()?.Trim()?.Trim('"');

                var behaviour = GetDistinctArgumentValues(classDeclarationSyntax, "Service", "Behaviour")?.FirstOrDefault();
                behaviour = behaviour ?? "EntityBehaviourEnum.Empty";

                //NOTE: FOR NOW WE WONT CHANGE CONTROLLER NAME!!!!
                var resourceName = GetDistinctArgumentValues(classDeclarationSyntax, "Service", "ResourceName")?.FirstOrDefault();
                if (resourceName == null)
                {
                    resourceName = className;
                }

                if (behaviour == "EntityBehaviourEnum.Empty")
                {
                    controller = controller.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("X.Core.WebAPI.Core.BaseController")));
                }
                else if (behaviour == "EntityBehaviourEnum.Read")
                {
                    controller = controller.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.WebAPI.Core.BaseReadController<{keyType}, {_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData>")));
                }
                else if (behaviour == "EntityBehaviourEnum.CRUD")
                {
                    controller = controller.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.WebAPI.Core.BaseCRUDController<{keyType}, {_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_modelNamespace}.Requests.{className}InsertRequest, {_modelNamespace}.Requests.{className}UpdateRequest>")));
                }
                else if (behaviour == "EntityBehaviourEnum.CRUDAsUpsert")
                {
                    controller = controller.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.WebAPI.Core.BaseCRUDController<{keyType}, {_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_modelNamespace}.Requests.{className}UpsertRequest, {_modelNamespace}.Requests.{className}UpsertRequest>")));
                }
                else if (behaviour == "EntityBehaviourEnum.CRUDAsUpload")
                {
                    controller = controller.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.WebAPI.Core.BaseCRUDUploadController<{keyType}, {_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_modelNamespace}.Requests.{className}InsertRequest, {_modelNamespace}.Requests.{className}UpdateRequest>")));
                }

                //X.Core.WebAPI.Core.BaseCRUDController<int, Model.User, Model.SearchObjects.UserSearchObject, Model.SearchObjects.UserAdditionalSearchRequestData, UserUpsertRequest, UserUpsertRequest>
                //generate additional methods from interface here
                foreach (var interfaceDeclarationSyntax in allWithSameName)
                {
                    controller = GenerateAdditionalMethods(controllerName, interfaceDeclarationSyntax, controller);
                }

                @namespace = @namespace.AddMembers(controller);
            }

            results = results.Add(@namespace);
            return results;
        }

        protected virtual ClassDeclarationSyntax GenerateAdditionalMethods(string controllerName,
            InterfaceDeclarationSyntax classDeclarationSyntax, ClassDeclarationSyntax controller)

        {
            StringBuilder builder = new StringBuilder();
            //get all methods annotated by getbyid
            var methods = GetMethodsWithSpecificAttribute<MethodDeclarationSyntax>(classDeclarationSyntax, "MethodBehaviour");

            foreach (var method in methods)
            {

                var behaviour = GetDistinctArgumentValues(method, "MethodBehaviour", "Behaviour")?.FirstOrDefault();
                if (behaviour != null)
                {
                    if (behaviour == "BehaviourEnum.GetById")
                    {
                        // generate method getbyid
                        BuildMethod(builder, method, "[HttpGet]", "[FromRoute]", "[FromQuery]", behaviour);
                    }
                    else if (behaviour == "BehaviourEnum.Get")
                    {
                        BuildMethod(builder, method, "[HttpGet]", "[FromQuery]", "[FromQuery]", behaviour);
                    }
                    else if (behaviour == "BehaviourEnum.Insert")
                    {
                        BuildMethod(builder, method, "[HttpPost]", "[FromBody]","", behaviour);
                    }
                    else if (behaviour == "BehaviourEnum.Update")
                    {
                        BuildMethod(builder, method, "[HttpPut]", "[FromRoute]", "[FromBody]", behaviour);
                    }
                    else if (behaviour == "BehaviourEnum.Delete")
                    {
                        BuildMethod(builder, method, "[HttpDelete]", "[FromRoute]", "[FromBody]", behaviour);
                    }
                    else if (behaviour == "BehaviourEnum.Download")
                    {
                        BuildMethod(builder, method, "[HttpGet]", "[FromRoute]", "[FromQuery]", behaviour);
                    }
                    else if (behaviour == "BehaviourEnum.DownloadAsPost")
                    {
                        BuildMethod(builder, method, "[HttpPost]", "[FromBody]", "[FromBody]", behaviour);
                    }
                }
            }

            if (builder.Length > 0)
            {
                SyntaxTree constructor = CSharpSyntaxTree.ParseText(
                    builder.ToString());
                var constructorRoot = (CompilationUnitSyntax)constructor.GetRoot();
                controller = controller.AddMembers(constructorRoot.Members.ToArray());
            }
            

            return controller;
        }

        protected virtual void BuildMethod(StringBuilder builder, MethodDeclarationSyntax method, string httpRequestType, string firstParamSource, string additionalParamsSource, string behaviour)
        {
            string friendlyMethodName = method.Identifier.ValueText;
            if (friendlyMethodName.EndsWith("Async"))
            {
                friendlyMethodName = friendlyMethodName.Substring(0, friendlyMethodName.IndexOf("Async", StringComparison.Ordinal));
            }
            String route = "";
            String firstParameterIdentifier = "";
            if (firstParamSource == "[FromRoute]")
            {
                firstParameterIdentifier = method.ParameterList.Parameters.FirstOrDefault()?.Identifier.ValueText ?? firstParameterIdentifier;
                if (behaviour == "BehaviourEnum.Delete")
                {
                    route = $"[Route(\"{{{firstParameterIdentifier}}}\")]";
                }
                else if (behaviour == "BehaviourEnum.GetById" && friendlyMethodName == "Get")
                {
                    route = $"[Route(\"{{{firstParameterIdentifier}}}\")]";
                }
                else if (!friendlyMethodName.Equals("Update", StringComparison.InvariantCultureIgnoreCase))
                {
                    route = $"[Route(\"{{{firstParameterIdentifier}}}/{friendlyMethodName}\")]";
                }
                else
                {
                    if (method.ParameterList.Parameters.Count > 1)
                    {
                        route = $"[Route(\"{{{firstParameterIdentifier}}}\")]";
                    }
                    else
                    {
                        route = $"[Route(\"{{{firstParameterIdentifier}}}/{friendlyMethodName}\")]";
                    }
                }
            }
            else if (firstParamSource == "[FromQuery]")
            {
                route = $"[Route(\"{friendlyMethodName}\")]";
            }

            if (behaviour == "BehaviourEnum.DownloadAsPost")
            {
                route = $"[Route(\"{friendlyMethodName}\")]";
            }

            if (behaviour == "BehaviourEnum.Insert" && !friendlyMethodName.Equals("Insert", StringComparison.InvariantCultureIgnoreCase))
            {
                route = $"[Route(\"{friendlyMethodName}\")]";
            }


            if (behaviour == "BehaviourEnum.Download" || behaviour == "BehaviourEnum.DownloadAsPost")
            {
                builder.AppendLine(
                    $@"{route}{httpRequestType} public async System.Threading.Tasks.Task<IActionResult> {method.Identifier.ValueText}(");
            }
            else
            {
                builder.AppendLine(
                    $@"{route}{httpRequestType} public async {method.ReturnType.ToString()} {method.Identifier.ValueText}(");


            }

            foreach (var parameter in method.ParameterList.Parameters)
            {
                builder.Append($"{firstParamSource}{parameter.Type.ToString()} {parameter.Identifier.ValueText},");
                firstParamSource = additionalParamsSource;
            }

            builder.Remove(builder.Length - 1, 1); //remove trailing comma
            if(behaviour == "BehaviourEnum.Download" || behaviour == "BehaviourEnum.DownloadAsPost")
            {
                builder.AppendLine($@") {{
                           var additionalDownloadData = await _mainService.{method.Identifier.ValueText}(");
            } 
            else
            {
                builder.AppendLine($@") {{
                           return await _mainService.{method.Identifier.ValueText}(");

            }
            foreach (var parameter in method.ParameterList.Parameters)
            {
                builder.Append($"{parameter.Identifier.ValueText},");
            }
            
            builder.Remove(builder.Length - 1, 1); //remove trailing comma

            builder.AppendLine($@");");
            if (behaviour == "BehaviourEnum.Download" || behaviour == "BehaviourEnum.DownloadAsPost")
            {
                builder.AppendLine($@"return File(additionalDownloadData.Stream, additionalDownloadData.ContentType, additionalDownloadData.FileName, true);");
            }
            builder.AppendLine($@"}}");
        }

        protected virtual ClassDeclarationSyntax GenerateConstructorMethods(string controllerName,
            InterfaceDeclarationSyntax classDeclarationSyntax, ClassDeclarationSyntax controller)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(
                $"protected readonly {_interfacesNamespace}.{classDeclarationSyntax.Identifier.ValueText} _mainService;");

            builder.AppendLine(
                $"public {controllerName} ({_interfacesNamespace}.{classDeclarationSyntax.Identifier.ValueText} service) : base(service)" +
                $"{{ _mainService = service; }}");


            SyntaxTree constructor = CSharpSyntaxTree.ParseText(
                builder.ToString());
            var constructorRoot = (CompilationUnitSyntax)constructor.GetRoot();
            controller = controller.AddMembers(constructorRoot.Members.ToArray());

            return controller;
        }

        protected IList<T> GetMethodsWithSpecificAttribute<T>(SyntaxNode document, string attributeName) where T : BaseMethodDeclarationSyntax
        {
            var filteredProps = document.DescendantNodes()
                .OfType<T>()
                .Where(y => y.AttributeLists.Any(z => z.Attributes.Any(u => u.Name.ToFullString() == attributeName)))
                .ToList();

            return filteredProps;
        }

        protected IList<T> GetTypesWithSpecificAttribute<T>(SyntaxNode document, string attributeName) where T: TypeDeclarationSyntax
        {
            var filteredProps = document.DescendantNodes()
                .OfType<T>()
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
