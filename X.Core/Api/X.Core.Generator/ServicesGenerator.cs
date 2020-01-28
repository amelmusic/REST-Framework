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
using X.Core.Generator.Attributes;

namespace X.Core.Generator
{
    public class ServicesGenerator : BaseGenerator, ICodeGenerator
    {
        private readonly AttributeData _attributeData;
        private EntityBehaviourEnum _behaviour;
        private string _modelPath;
        private string _modelNamespace;
        private string _entityFrameworkContextName;
        private string _servicesNamespace;
        private string _interfacesNamespace;
        private string _entityFrameworkContextNamespace;

        public ServicesGenerator(AttributeData attributeData)
        {
            //Requires.NotNull(attributeData, nameof(attributeData));
            this._attributeData = attributeData;

            var mappedTo = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "ModelPath");
            string mappedToStr = mappedTo.Key == null ? null : mappedTo.Value.Value.ToString();
            _modelPath = mappedToStr;

            var modelNamespace = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "ModelNamespace");
            _modelNamespace = modelNamespace.Key == null ? "Model" : modelNamespace.Value.Value.ToString();

            var entityFrameworkContextName = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "EntityFrameworkContextName");
            _entityFrameworkContextName = entityFrameworkContextName.Key == null ? null : entityFrameworkContextName.Value.Value.ToString();

            var servicesNamespace = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "ServicesNamespace");
            _servicesNamespace = servicesNamespace.Key == null ? null : servicesNamespace.Value.Value.ToString();

            var interfacesNamespace = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "InterfacesNamespace");
            _interfacesNamespace = interfacesNamespace.Key == null ? _servicesNamespace.Replace(".Services", ".Interfaces") : interfacesNamespace.Value.Value.ToString();
            
            var entityFrameworkContextNamespace = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "EntityFrameworkContextNamespace");
            _entityFrameworkContextNamespace = entityFrameworkContextNamespace.Key == null ? _servicesNamespace : entityFrameworkContextNamespace.Value.Value.ToString();
        }

        public async Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context,
            IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            if (File.Exists(".DEBUG"))
            {
                Debugger.Launch();
            }
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

            var services = await GenerateServices(root, context);
            results = results.AddRange(services);

            var stateMachines = await GenerateStateMachines(root, context);
            results = results.AddRange(stateMachines);
            return results;
        }


        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateEFContext(SyntaxNode root,
            TransformationContext context)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            if (!string.IsNullOrWhiteSpace(_entityFrameworkContextName))
            {
                var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(_entityFrameworkContextNamespace)).NormalizeWhitespace();

                var efContext = SyntaxFactory.ClassDeclaration($"{_entityFrameworkContextName}");
                efContext = efContext.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));

                //efContext = efContext.AddBaseListTypes(
                //    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("Microsoft.EntityFrameworkCore.DbContext")));

                @namespace = @namespace.AddMembers(efContext);
                results = results.Add(@namespace);
            }

            return results;
        }
        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateServices(SyntaxNode root, TransformationContext context)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            //var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Services")).NormalizeWhitespace();
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(_servicesNamespace)).NormalizeWhitespace();
           
            //generate EF Context here

            results = results.AddRange(await GenerateEFContext(root, context));
            //service = service.AddBaseListTypes(
            //    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("X.Core.Services.Core.BaseService")));

            //results = results.Add(service);

            var classes = GetClassesWithSpecificAttribute(root, "ModelGenerator");
            //foreach class generate new service
            foreach (var classDeclarationSyntax in classes)
            {
                var service = SyntaxFactory.ClassDeclaration($"{classDeclarationSyntax.Identifier.ValueText}Service");
                service = service.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));
                var className = classDeclarationSyntax.Identifier.ValueText;

                var behaviour = GetDistinctArgumentValues(classDeclarationSyntax, "ModelGenerator", "Behaviour")?.FirstOrDefault();
                behaviour = behaviour ?? "EntityBehaviourEnum.Empty";
                if (behaviour == "EntityBehaviourEnum.Empty")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("X.Core.Services.Core.BaseService")));
                }
                else if (behaviour == "EntityBehaviourEnum.Read")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.Services.Core.BaseEFBasedReadService<{_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_entityFrameworkContextNamespace}.{_entityFrameworkContextName}, {_entityFrameworkContextNamespace}.{className}>")));
                }
                else if (behaviour == "EntityBehaviourEnum.CRUD")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.Services.Core.BaseEFBasedCRUDService<{_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_modelNamespace}.Requests.{className}InsertRequest, {_modelNamespace}.Requests.{className}UpdateRequest, {_entityFrameworkContextNamespace}.{_entityFrameworkContextName}, {_entityFrameworkContextNamespace}.{className}>")));
                }
                else if (behaviour == "EntityBehaviourEnum.CRUDAsUpsert")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.Services.Core.BaseEFBasedCRUDService<{_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_modelNamespace}.Requests.{className}UpsertRequest, {_modelNamespace}.Requests.{className}UpsertRequest, {_entityFrameworkContextNamespace}.{_entityFrameworkContextName}, {_entityFrameworkContextNamespace}.{className}>")));
                }
                else if (behaviour == "EntityBehaviourEnum.CRUDAsUpload")
                {
                    service = service.AddBaseListTypes(
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.Services.Core.BaseEFBasedCRUDService<{_modelNamespace}.{className}, {_modelNamespace}.SearchObjects.{className}SearchObject, {_modelNamespace}.SearchObjects.{className}AdditionalSearchRequestData, {_modelNamespace}.Requests.{className}InsertRequest, {_modelNamespace}.Requests.{className}UpdateRequest, {_entityFrameworkContextNamespace}.{_entityFrameworkContextName}, {_entityFrameworkContextNamespace}.{className}>")));
                }

                service = service.AddBaseListTypes(
                    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"{_interfacesNamespace}.I{className}Service")));

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

                service = GenerateFilter(service, classDeclarationSyntax, className);
                @namespace = @namespace.AddMembers(service);
            }

            @namespace = @namespace.WithUsings(
                    SyntaxFactory.SingletonList<UsingDirectiveSyntax>(
                        SyntaxFactory.UsingDirective(
                            SyntaxFactory.QualifiedName(
                                SyntaxFactory.IdentifierName("System"),
                                SyntaxFactory.IdentifierName("Linq")))))
                .NormalizeWhitespace();

            //@namespace.AddUsings("")
            results = results.Add(@namespace);
            return results;
        }

        protected ClassDeclarationSyntax GenerateFilter(ClassDeclarationSyntax root, ClassDeclarationSyntax modelClass,string className)
        {
            var str = new StringBuilder();

            //Debugger.Launch();
            var filterProps = GetPropertiesWithSpecificAttribute(modelClass, "Filter");
            //Append filter foreach
            str.AppendLine($@"protected override void AddFilterFromGeneratedCode({_modelNamespace}.SearchObjects.{className}SearchObject search, ref IQueryable<{_entityFrameworkContextNamespace}.{className}> query)
                    {{
                        base.AddFilterFromGeneratedCode(search, ref query);");
            //Filters
            foreach (var propertyDeclarationSyntax in filterProps)
            {
                //we will regenerate here
                var attrs = GetAttributes(propertyDeclarationSyntax, "Filter");

                var attrValueTmp = GetAttributeArgumentValue(attrs.First(), "Filter");
                var attrValue = attrValueTmp.Replace("\"", "").Split('|').ToList();
                attrValue = attrValue.Select(x => x.Trim()).ToList();

                var propertyType = propertyDeclarationSyntax.Type.ToFullString().Trim();
                var propertyName = propertyDeclarationSyntax.Identifier.ValueText;

                if (attrValue.Contains("FilterEnum.Equal"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName})) {{query = query.Where(x => x.{propertyName} == search.{propertyName}); }}"
                        : $"if (search.{propertyName}.HasValue) {{query = query.Where(x => x.{propertyName} == search.{propertyName}); }}");
                }
                if (attrValue.Contains("FilterEnum.NotEqual"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}NE)) {{query = query.Where(x => x.{propertyName} != search.{propertyName}NE); }}"
                        : $"if (search.{propertyName}NE.HasValue) {{query = query.Where(x => x.{propertyName} != search.{propertyName}NE); }}");
                }
                if (attrValue.Contains("FilterEnum.GreatherThan"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}GT)) {{query = query.Where(x => x.{propertyName}.StartsWith(search.{propertyName}GT)); }}"
                        : $"if (search.{propertyName}GT.HasValue) {{query = query.Where(x => x.{propertyName} > search.{propertyName}GT); }}");
                }
                if (attrValue.Contains("FilterEnum.GreatherThanOrEqual"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}GTE)) {{query = query.Where(x => x.{propertyName}.StartsWith(search.{propertyName}GTE)); }}"
                        : $"if (search.{propertyName}GTE.HasValue) {{query = query.Where(x => x.{propertyName} >= search.{propertyName}GTE); }}");
                }
                if (attrValue.Contains("FilterEnum.LowerThan"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}LT)) {{query = query.Where(x => x.{propertyName}.EndsWith(search.{propertyName}LT)); }}"
                        : $"if (search.{propertyName}LT.HasValue) {{query = query.Where(x => x.{propertyName} < search.{propertyName}LT); }}");
                }
                if (attrValue.Contains("FilterEnum.LowerThanOrEqual"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}LTE)) {{query = query.Where(x => x.{propertyName}.EndsWith(search.{propertyName}LTE)); }}"
                        : $"if (search.{propertyName}LTE.HasValue) {{query = query.Where(x => x.{propertyName} <= search.{propertyName}LTE); }}");
                }

                if (attrValue.Contains("FilterEnum.List"))
                {
                    str.AppendLine($"if (search.{propertyName}List.Count > 0) {{query = query.Where(x => search.{propertyName}List.Contains(x.{propertyName})); }}");
                }
                if (attrValue.Contains("FilterEnum.EqualOrNull"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}OrNull)) {{query = query.Where(x => x.{propertyName} == search.{propertyName} || x.{propertyName} == null); }}"
                        : $"if (search.{propertyName}OrNull.HasValue) {{query = query.Where(x => x.{propertyName} == search.{propertyName} || x.{propertyName} == null); }}");
                }

                if (attrValue.Contains("FilterEnum.NotEqualOrNull"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}NEOrNull)) {{query = query.Where(x => x.{propertyName} != search.{propertyName}NEOrNull || x.{propertyName} == null); }}"
                        : $"if (search.{propertyName}NEOrNull.HasValue) {{query = query.Where(x => x.{propertyName} != search.{propertyName}NEOrNull || x.{propertyName} == null); }}");
                }

                if (attrValue.Contains("FilterEnum.GreatherThanOrEqualOrNull"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}GTEOrNull)) {{query = query.Where(x => x.{propertyName}.StartsWith(search.{propertyName}GTEOrNull) || x.{propertyName} == null); }}"
                        : $"if (search.{propertyName}GTEOrNull.HasValue) {{query = query.Where(x => x.{propertyName} >= search.{propertyName}GTEOrNull || x.{propertyName} == null); }}");
                }

                if (attrValue.Contains("FilterEnum.LowerThanOrEqualOrNull"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}LTEOrNull)) {{query = query.Where(x => x.{propertyName}.EndsWith(search.{propertyName}LTEOrNull) || x.{propertyName} == null); }}"
                        : $"if (search.{propertyName}LTEOrNull.HasValue) {{query = query.Where(x => x.{propertyName} <= search.{propertyName}LTEOrNull || x.{propertyName} == null); }}");
                }

                if (attrValue.Contains("FilterEnum.Null"))
                {
                    str.AppendLine(
                        $"if (search.{propertyName}Null == true) {{query = query.Where(x => x.{propertyName} == null); }}");
                }

                if (attrValue.Contains("FilterEnum.GreatherThanOrNull"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}GTOrNull)) {{query = query.Where(x => x.{propertyName}.StartsWith(search.{propertyName}GTOrNull) || x.{propertyName} == null); }}"
                        : $"if (search.{propertyName}GTOrNull.HasValue) {{query = query.Where(x => x.{propertyName} > search.{propertyName}GTOrNull || x.{propertyName} == null); }}");
                }

                if (attrValue.Contains("FilterEnum.LowerThanOrNull"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName}LTOrNull)) {{query = query.Where(x => x.{propertyName}.EndsWith(search.{propertyName}LTOrNull) || x.{propertyName} == null); }}"
                        : $"if (search.{propertyName}LTOrNull.HasValue) {{query = query.Where(x => x.{propertyName} < search.{propertyName}LTOrNull || x.{propertyName} == null); }}");
                }

                if (attrValue.Contains("FilterEnum.ListOrNull"))
                {
                    str.AppendLine($"if (search.{propertyName}ListOrNull.Count > 0) {{query = query.Where(x => search.{propertyName}ListOrNull.Contains(x.{propertyName}) || x.{propertyName} == null); }}");
                }

                if (attrValue.Contains("FilterEnum.ListNotEqualOrNull"))
                {
                    str.AppendLine($"if (search.{propertyName}ListNEOrNull.Count > 0) {{query = query.Where(x => !search.{propertyName}ListNEOrNull.Contains(x.{propertyName}) || x.{propertyName} == null); }}");
                }

                if (attrValue.Contains("FilterEnum.ListNotEqual"))
                {
                    str.AppendLine($"if (search.{propertyName}ListNE.Count > 0) {{query = query.Where(x => !search.{propertyName}ListNE.Contains(x.{propertyName})); }}");
                }
            }

            var keyProp = GetPropertiesWithSpecificAttribute(modelClass, "Key").FirstOrDefault();
            if (keyProp != null)
            {
                var attrs = GetAttributes(keyProp, "Filter");
                string attrValue = null;
                if (attrs.Count > 0)
                {
                    attrValue = GetAttributeArgumentValue(attrs.First(), "Filter");
                }

                var propertyType = keyProp.Type.ToFullString();
                var propertyName = keyProp.Identifier.ValueText;

                if (attrValue == null || !attrValue.Contains("FilterEnum.List"))
                {
                    str.AppendLine($"if (search.{propertyName}List.Count > 0) {{query = query.Where(x => search.{propertyName}List.Contains(x.{propertyName})); }}");
                }

                if (attrValue == null || !attrValue.Contains("FilterEnum.Equal"))
                {
                    str.AppendLine(propertyType.Equals("string", StringComparison.OrdinalIgnoreCase)
                        ? $"if (!string.IsNullOrWhiteSpace(search.{propertyName})) {{query = query.Where(x => x.{propertyName} == search.{propertyName}); }}"
                        : $"if (search.{propertyName}.HasValue) {{query = query.Where(x => x.{propertyName} == search.{propertyName}); }}");
                }
            }
            //End
            str.AppendLine(@"}");

            SyntaxTree tree = CSharpSyntaxTree.ParseText(str.ToString());
            var newCodeRoot = (CompilationUnitSyntax)tree.GetRoot();
            root = root.AddMembers(newCodeRoot.Members.ToArray());
            return root;
        }

        /// <summary>
        /// This method generates state machine inside service and configures it.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateStateMachines(SyntaxNode root,
            TransformationContext context)

        {
            //Debugger.Launch();
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(_servicesNamespace)).NormalizeWhitespace();
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(_modelNamespace)));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(_modelNamespace + ".Requests")));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Linq")));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")));

            var classes = GetClassesWithSpecificAttribute(root, "StateMachineModelGenerator");

            foreach (var classDeclarationSyntax in classes)
            {
                var key = GetPropertiesWithSpecificAttribute(classDeclarationSyntax, "Key")?.FirstOrDefault();
                string keyType = key != null ? key.Type.ToFullString().Trim() : "int";
                if (key == null)
                {
                    throw new Exception($"Model: {classDeclarationSyntax} doesn't have property with [Key] attribute");
                }
                var path = GetDistinctArgumentValues(classDeclarationSyntax, "StateMachineModelGenerator", "StateMachineDefinitionPath")?.FirstOrDefault();
                path = path?.Trim().Trim('"');
                var propertyName = GetDistinctArgumentValues(classDeclarationSyntax, "StateMachineModelGenerator", "PropertyName")?.FirstOrDefault();
                propertyName = propertyName?.Trim().Trim('"');
                var property = GetPropertiesWithSpecificName(classDeclarationSyntax, propertyName);
                var propertyType = property.Type.ToFullString().Trim();

                StateMachineDefinition stateMachineDefinition = new StateMachineDefinition();
                //stateMachineDefinition.Parse(_modelPath + "/" + path, classDeclarationSyntax.Identifier.ValueText);
                stateMachineDefinition.Parse(path, classDeclarationSyntax.Identifier.ValueText, _modelPath);
                
                //first create partial service and instantiate SM inside it
                var service = SyntaxFactory.ClassDeclaration($"{classDeclarationSyntax.Identifier.ValueText}Service");
                service = service.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));
                
                string stateMachineDeclarationStr =
                    $"X.Core.Services.Core.StateMachine.StateMachine<{classDeclarationSyntax.Identifier.ValueText}StateMachineEnum, {classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum>";
                
                var stateMachine = SyntaxFactory.ClassDeclaration($"{classDeclarationSyntax.Identifier.ValueText}StateMachine");
                stateMachine = stateMachine.AddBaseListTypes(
                    SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(stateMachineDeclarationStr)));
                stateMachine = stateMachine.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));

                StringBuilder stateMachineBuilder = new StringBuilder();
                stateMachineBuilder.AppendLine(
                    $"public {classDeclarationSyntax.Identifier.ValueText}StateMachine _machine {{get; set;}}");
                stateMachineBuilder.AppendLine("protected bool _configured = false;");

                stateMachineBuilder.AppendLine(@"protected virtual void ConfigureStateMachine() {
                    if (_configured) {return;}");

                var initialState = stateMachineDefinition.StateDefinitions.FirstOrDefault(x => x.IsInitial);
                if (initialState == null)
                {
                    throw new Exception("Initial state must be defined. It either must be called Initial or have *");
                }
                //stateMachineBuilder.AppendLine($"_machine = new StateMachine<{classDeclarationSyntax.Identifier.ValueText}StateMachineEnum, {classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum>({classDeclarationSyntax.Identifier.ValueText}StateMachineEnum.{initialState.StateName});");

                //foreach (var stateMachineTransition in stateMachineDefinition.StateMachineTransitions.Where(x => x.InvokeCustomMethodOnTransition).Select(x => x.ActionName).Distinct())
                //{
                //    stateMachineBuilder.AppendLine(
                //        $"_{stateMachineTransition}Trigger = _machine.SetTriggerParameters<{classDeclarationSyntax.Identifier.ValueText}{stateMachineTransition}Request>({classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum.{stateMachineTransition});");
                //}
                foreach (var stateDefinition in stateMachineDefinition.StateDefinitions)
                {
                    var transitions = stateMachineDefinition.StateMachineTransitions
                        .Where(x => x.FromState == stateDefinition.StateName).ToList();

                    stateMachineBuilder.AppendLine($"_machine.Configure({classDeclarationSyntax.Identifier.ValueText}StateMachineEnum.{stateDefinition.StateName})");

                    foreach (var stateMachineTransition in transitions)
                    {
                        stateMachineBuilder.AppendLine($".Permit({classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum.{stateMachineTransition.ActionName}, {classDeclarationSyntax.Identifier.ValueText}StateMachineEnum.{stateMachineTransition.ToState})");
                    }

                    //Debugger.Launch();
                    var transitionDestinationsWithAction = stateMachineDefinition.StateMachineTransitions
                        .Where(x => x.ToState == stateDefinition.StateName && x.InvokeCustomMethodOnTransition).ToList();

                    foreach (var stateMachineTransition in transitionDestinationsWithAction)
                    {
                        stateMachineBuilder.AppendLine($".OnEntryFrom({classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum.{stateMachineTransition.ActionName},On{stateMachineTransition.ActionName}To{stateDefinition.StateName})");
                        //.OnEntryFrom(_{stateMachineTransition.ActionName}Trigger,On{stateMachineTransition.ActionName})
                        //stateMachineBuilder.AppendLine($".Permit({classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum.{stateMachineTransition.ActionName}, {classDeclarationSyntax.Identifier.ValueText}StateMachineEnum.{stateMachineTransition.ToState})");
                    }
                    stateMachineBuilder.Append(";");
                }


                stateMachineBuilder.AppendLine("OnConfigureStateMachine(); /*Partial*/ _configured = true; }  partial void OnConfigureStateMachine();");

                StringBuilder stateMachineActionsBuilder = new StringBuilder();

                var distinctTransitions = stateMachineDefinition.StateMachineTransitions.Select(x => x.ActionName).Distinct().ToList();

                foreach (var transition in distinctTransitions)
                {
                    if (transition.Equals("insert", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //insert method
                        stateMachineActionsBuilder.AppendLine(
                            $@"[X.Core.Interceptors.Transaction] public virtual async Task<{classDeclarationSyntax.Identifier.ValueText}> {transition}({classDeclarationSyntax.Identifier.ValueText}{transition}Request request)
                            {{
                                //insert
                                var entity = CreateNewInstance();
                                ConfigureStateMachine();
                                await _machine.Init({classDeclarationSyntax.Identifier.ValueText}StateMachineEnum.Initial, entity);
                                await _machine.Fire({classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum.{transition}, request);
                                entity.{propertyName} = ({propertyType})_machine.CurrentState.State;
                                if (!_machine.HandledEntityPersistence) {{
                                    Entity.Attach(entity);
                                    Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                }}
                                await SaveAsync(entity);
                                var model = Mapper.Map<{classDeclarationSyntax.Identifier.ValueText}>(entity);
                                return model;
                            }}");
                    }
                    else if (transition.Equals("delete", StringComparison.InvariantCultureIgnoreCase))
                    {
                        stateMachineActionsBuilder.AppendLine(
                            $@"[X.Core.Interceptors.Transaction] public virtual async Task<{classDeclarationSyntax.Identifier.ValueText}> {transition}({keyType} id, {classDeclarationSyntax.Identifier.ValueText}{transition}Request request)
                            {{
                                var entity = await GetByIdInternalAsync(id);
                                ConfigureStateMachine();
                                await _machine.Init(({classDeclarationSyntax.Identifier.ValueText}StateMachineEnum)entity.{propertyName}, entity);
                                await _machine.Fire({classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum.{transition}, request);
                                entity.{propertyName} = ({propertyType})_machine.CurrentState.State;
                                if (!_machine.HandledEntityPersistence) {{
                                    Entity.Attach(entity);
                                    Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                                }}
                               
                                await SaveAsync(entity, request);
                                var model = Mapper.Map<{classDeclarationSyntax.Identifier.ValueText}>(entity);
                                return model;
                            }}");
                    }
                    else
                    {
                        stateMachineActionsBuilder.AppendLine(
                            $@"[X.Core.Interceptors.Transaction] public virtual async Task<{classDeclarationSyntax.Identifier.ValueText}> {transition}({keyType} id, {classDeclarationSyntax.Identifier.ValueText}{transition}Request request)
                            {{
                                var entity = await GetByIdInternalAsync(id);
                                ConfigureStateMachine();
                                await _machine.Init(({classDeclarationSyntax.Identifier.ValueText}StateMachineEnum)entity.{propertyName}, entity);
                                await _machine.Fire({classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum.{transition}, request);
                                var machineEntity = this._machine.Entity as {_entityFrameworkContextNamespace}.{classDeclarationSyntax.Identifier.ValueText};
                                machineEntity.{propertyName} = ({propertyType})_machine.CurrentState.State;
                                
                                await SaveAsync(machineEntity, request);
                                var model = Mapper.Map<{classDeclarationSyntax.Identifier.ValueText}>(machineEntity);
                                return model;
                            }}");
                    }
                }

                stateMachineActionsBuilder.AppendLine(
                    $@"public virtual async Task<List<{classDeclarationSyntax.Identifier.ValueText}StateMachineTriggerEnum>> AllowedActionsAsync({keyType} id)
                            {{
                                ConfigureStateMachine();
                                var entity = await GetByIdInternalAsync(id);
                                var status = entity != null ? ({classDeclarationSyntax.Identifier.ValueText}StateMachineEnum)entity.{propertyName} : {classDeclarationSyntax.Identifier.ValueText}StateMachineEnum.Initial;
                                await _machine.Init(status, entity);

                                var triggers = this._machine.CurrentState.Triggers.Select(x => x.Trigger).ToList();

                                return triggers;
                            }}");

                stateMachineBuilder.Append(stateMachineActionsBuilder.ToString());
                SyntaxTree tree = CSharpSyntaxTree.ParseText(stateMachineBuilder.ToString());
                var newCodeRoot = (CompilationUnitSyntax)tree.GetRoot();
                service = service.AddMembers(newCodeRoot.Members.ToArray());
                @namespace = @namespace.AddMembers(service);
                @namespace = @namespace.AddMembers(stateMachine);
            }

            //@namespace = @namespace.WithUsings(
            //        SyntaxFactory.List<UsingDirectiveSyntax>(
            //            new UsingDirectiveSyntax[]
            //            {
            //                SyntaxFactory.UsingDirective(
            //                    SyntaxFactory.QualifiedName(
            //                        SyntaxFactory.IdentifierName("System"),
            //                        SyntaxFactory.IdentifierName("Linq")))
            //            }))
            //    .NormalizeWhitespace();


            results = results.Add(@namespace);
            return results;
        }
    }
}
