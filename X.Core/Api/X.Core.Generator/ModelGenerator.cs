using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeGeneration.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using X.Core.Generator.Attributes;

namespace X.Core.Generator
{
    /// <summary>
    /// Serves as entry point for generating requests, search objects etc
    /// </summary>
    public class ModelGenerator : BaseGenerator, ICodeGenerator
    {
        private readonly AttributeData _attributeData;
        private EntityBehaviourEnum _behaviour;

        public ModelGenerator(AttributeData attributeData)
        {
            //Requires.NotNull(attributeData, nameof(attributeData));
            this._attributeData = attributeData;

            //var mappedTo = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "MapTo");
            //string mappedToStr = mappedTo.Key == null ? null : mappedTo.Value.Value.ToString();
        }

        public async Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context,
            IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            if (File.Exists(".DEBUG"))
            {
                Debugger.Launch();
            }

            var results = SyntaxFactory.List<MemberDeclarationSyntax>();

            var behaviour = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "Behaviour");
            _behaviour = (EntityBehaviourEnum)behaviour.Value.Value;
            //Here we will generate requests and search objects


            //// Our generator is applied to any class that our attribute is applied to.
            //var applyToClass = (ClassDeclarationSyntax)context.ProcessingNode;

            //// Apply a suffix to the name of a copy of the class.
            //var copy = applyToClass
            //    .WithIdentifier(SyntaxFactory.Identifier(applyToClass.Identifier.ValueText + this.suffix));

            //// Return our modified copy. It will be added to the user's project for compilation.
            //results = results.Add(copy);
            results = results.AddRange(await GenerateRequests(context));

            results = results.AddRange(await GenerateSearchObjects(context));
            return results;
        }

        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateRequests(TransformationContext context)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            var applyToClass = (ClassDeclarationSyntax)context.ProcessingNode;

            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Requests")).NormalizeWhitespace();
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("X.Core.Model")));
            //Debugger.Launch();
            var distinctArgValues = GetDistinctArgumentValues(applyToClass, "RequestField", "RequestName");
            if (_behaviour == EntityBehaviourEnum.CRUD
                || _behaviour == EntityBehaviourEnum.CRUDAsUpload)
            {
                if (!distinctArgValues.Contains("\"Insert\""))
                {
                    distinctArgValues.Add("\"Insert\"");
                }

                if (!distinctArgValues.Contains("\"Update\""))
                {
                    distinctArgValues.Add("\"Update\"");
                }
            }
            
            //Debugger.Launch();
            foreach (var distinctArgValue in distinctArgValues)
            {
                var classDeclaration = SyntaxFactory.ClassDeclaration($"{applyToClass.Identifier.ValueText}{distinctArgValue.Trim('"')}Request");
                classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));


                var requestProps = GetPropertiesWithAttributeAndArgumentValue(applyToClass, "RequestField", "RequestName",
                    distinctArgValue);

                foreach (var propertyDeclarationSyntax in requestProps)
                {
                    var prop = propertyDeclarationSyntax;

                    //TODO: Add additional parameters
                    //foreach (var attributeListSyntax in propertyDeclarationSyntax.AttributeLists)
                    //{

                    //    prop = propertyDeclarationSyntax.RemoveNode(attributeListSyntax.Attributes.FirstOrDefault(),
                    //        SyntaxRemoveOptions.KeepNoTrivia);
                    //    prop.AttributeLists.RemoveAt(0);
                    //}
                    //var newAttributes = new SyntaxList<AttributeListSyntax>();
                    //var list = prop.AttributeLists.SelectMany(x => x.Attributes)
                    //    .Where(x => x.Name.ToFullString() != "RequestField"
                    //                && x.Name.ToFullString() != "Filter").ToList();
                    //Debugger.Launch();
                    //prop = prop.WithAttributeLists(newAttributes);
                    var existingType = prop.Type.ToString();
                    var type = GetTypedConstantKind(existingType);
                    var newType = type == TypedConstantKind.Type ? existingType : existingType + "?";
                    var newProp = prop.WithType(SyntaxFactory.ParseTypeName(newType));
                    //prop.AttributeLists.First().Attributes.AddRange(list.ToArray());
                    classDeclaration = classDeclaration.AddMembers(newProp);
                    // Create a Property: (public int Quantity { get; set; })
                    //var propertyDeclaration = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName("int"), "Quantity")
                    //    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    //    .AddAccessorListAccessors(
                    //        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                    //        SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
                }

                @namespace = @namespace.AddMembers(classDeclaration);
            }

            results = results.Add(@namespace);
            return results;
        }

        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateSearchObjects(TransformationContext context)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            var applyToClass = (ClassDeclarationSyntax)context.ProcessingNode;

            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("SearchObjects")).NormalizeWhitespace();
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("X.Core.Model")));
            var classDeclarationAdditionalData = SyntaxFactory.ClassDeclaration($"{applyToClass.Identifier.ValueText}AdditionalSearchRequestData");
            classDeclarationAdditionalData = classDeclarationAdditionalData.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));

            classDeclarationAdditionalData = classDeclarationAdditionalData.AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("X.Core.Model.BaseAdditionalSearchRequestData")));

            @namespace = @namespace.AddMembers(classDeclarationAdditionalData);

            var classDeclaration = SyntaxFactory.ClassDeclaration($"{applyToClass.Identifier.ValueText}SearchObject");
            classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));
            classDeclaration = classDeclaration.AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName($"X.Core.Model.BaseSearchObject<{applyToClass.Identifier.ValueText}AdditionalSearchRequestData>")));
            //Debugger.Launch();
            var properties = GetPropertiesWithSpecificAttribute(applyToClass, "Filter");
            
            //Debugger.Launch();
            foreach (var propertyDeclarationSyntax in properties)
            {
                //we will regenerate here
                var attrs = GetAttributes(propertyDeclarationSyntax, "Filter");

                var attrValueTmp = GetAttributeArgumentValue(attrs.First(), "Filter");
                var attrValue = attrValueTmp.Replace("\"", "").Split('|').ToList();
                attrValue = attrValue.Select(x => x.Trim()).ToList();
                var propertyType = propertyDeclarationSyntax.Type.ToFullString();
                var propertyName = propertyDeclarationSyntax.Identifier.ValueText;
                bool filterAdded = false;
                if (attrValue.Contains("FilterEnum.Equal"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName, "Equal");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.List"))
                {
                    var propertyList = CreatePropertyForSearchObject(propertyType, propertyName + "List", "List");
                    classDeclaration = classDeclaration.AddMembers(propertyList);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.NotEqual"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "NE", "NotEqual");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.GreatherThan"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "GT", "GreatherThan");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.GreatherThanOrEqual"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "GTE", "GreatherThanOrEqual");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.LowerThan"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "LT", "LowerThan");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.LowerThanOrEqual"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "LTE", "LowerThanOrEqual");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.EqualOrNull"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "OrNull", "EqualOrNull");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.NotEqualOrNull"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "NEOrNull", "NotEqualOrNull");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.GreatherThanOrEqualOrNull"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "GTEOrNull", "GreatherThanOrEqualOrNull");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.LowerThanOrEqualOrNull"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "LTEOrNull", "LowerThanOrEqualOrNull");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.Null"))
                {
                    var property = CreatePropertyForSearchObject("bool", propertyName + "Null", "Null");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.GreatherThanOrNull"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "GTOrNull", "GreatherThanOrNull");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.LowerThanOrNull"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "LTOrNull", "LowerThanOrNull");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.ListOrNull"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "ListOrNull", "ListOrNull");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.ListNotEqualOrNull"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "ListNEOrNull", "ListNotEqualOrNull");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
                if (attrValue.Contains("FilterEnum.ListNotEqual"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName + "ListNE", "ListNotEqual");
                    classDeclaration = classDeclaration.AddMembers(property);
                    filterAdded = true;
                }
            }
           
            var keyProp = GetPropertiesWithSpecificAttribute(applyToClass, "Key").FirstOrDefault();
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
                    var propertyList = CreatePropertyForSearchObject(propertyType, propertyName + "List", "List");
                    classDeclaration = classDeclaration.AddMembers(propertyList);
                }

                if (attrValue == null || !attrValue.Contains("FilterEnum.Equal"))
                {
                    var property = CreatePropertyForSearchObject(propertyType, propertyName, "Equal");
                    classDeclaration = classDeclaration.AddMembers(property);
                }
            }
            else
            {
                throw new ApplicationException($"Model: {applyToClass.Identifier.ValueText} doesn't have Key attribute");
            }

            @namespace = @namespace.AddMembers(classDeclaration);

            results = results.Add(@namespace);
            return results;
        }

        protected virtual PropertyDeclarationSyntax CreatePropertyForSearchObject(string propertyType, string propertyName, string argumentValue)
        {
            //Debugger.Launch();
            TypeSyntax type = null;
            var isList = (propertyName.EndsWith("List")
                          || propertyName.EndsWith("ListOrNull")
                          || propertyName.EndsWith("ListNotEqualOrNull")
                          || propertyName.EndsWith("ListNotEqual"));
            if (isList)
            {
                type = SyntaxFactory.GenericName(
                    SyntaxFactory.Identifier("List")).WithTypeArgumentList(
                    SyntaxFactory.TypeArgumentList(
                        SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                            SyntaxFactory.ParseTypeName(propertyType))));
            }            
            else if (propertyType.Contains("string") || propertyType.Contains("?"))
            {
                type = SyntaxFactory.ParseTypeName(propertyType);
            }
            else
            {
                type = SyntaxFactory.NullableType(
                    SyntaxFactory.ParseTypeName(propertyType));
            }

            var propertyDeclaration = SyntaxFactory.PropertyDeclaration(
                    type, propertyName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .WithAccessorList(
                        SyntaxFactory.AccessorList(
                            SyntaxFactory.List<AccessorDeclarationSyntax>(
                                new AccessorDeclarationSyntax[]{
                                    SyntaxFactory.AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                    SyntaxFactory.AccessorDeclaration(
                                            SyntaxKind.SetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            SyntaxFactory.Token(SyntaxKind.SemicolonToken))})))
                .WithAttributeLists(
                    SyntaxFactory.SingletonList<AttributeListSyntax>(
                        SyntaxFactory.AttributeList(
                            SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(
                                SyntaxFactory.Attribute(
                                        SyntaxFactory.IdentifierName("Filter"))
                                    .WithArgumentList(
                                        SyntaxFactory.AttributeArgumentList(
                                            SyntaxFactory.SingletonSeparatedList<AttributeArgumentSyntax>(
                                                SyntaxFactory.AttributeArgument(
                                                        SyntaxFactory.MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            SyntaxFactory.IdentifierName("FilterEnum"),
                                                            SyntaxFactory.IdentifierName(argumentValue)))
                                                    .WithNameEquals(
                                                        SyntaxFactory.NameEquals(
                                                            SyntaxFactory.IdentifierName("Filter"))))))))));

            if (isList)
            {
                propertyDeclaration = propertyDeclaration.WithInitializer(
                    SyntaxFactory.EqualsValueClause(
                        SyntaxFactory.ObjectCreationExpression(
                                SyntaxFactory.GenericName(
                                        SyntaxFactory.Identifier("List"))
                                    .WithTypeArgumentList(
                                        SyntaxFactory.TypeArgumentList(
                                            SyntaxFactory.SingletonSeparatedList<TypeSyntax>(
                                                SyntaxFactory.ParseTypeName(propertyType)))))
                            .WithArgumentList(
                                SyntaxFactory.ArgumentList())))
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            }
            return propertyDeclaration;
        }


        #region HELPER METHODS
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

        protected IList<PropertyDeclarationSyntax> GetPropertiesWithAttributeAndArgumentValue(
            MemberDeclarationSyntax member,
            string attributeName, string argumentName, string argumentValue)
        {
            var filteredProps = member.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(y => y.AttributeLists.Any(z => z.Attributes.Any(u => u.Name.ToFullString() == attributeName
                                                                            && u.ArgumentList.Arguments.Any(i => i.NameEquals.Name.Identifier.ValueText == argumentName && i.Expression.ToFullString() == argumentValue)
                                                                            )))

                .ToList();

            return filteredProps;
        }
        #endregion
    }
}
