using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CodeGeneration.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using X.Core.Generator.Attributes;

namespace X.Core.Generator
{
    public class StateMachineModelGenerator : ICodeGenerator
    {
        private readonly AttributeData _attributeData;
        private string _model;
        private string _propertyName;

        //Model="Account", PropertyName="StatusId", PropertyType="int"
        public StateMachineModelGenerator(AttributeData attributeData)
        {
            //Requires.NotNull(attributeData, nameof(attributeData));
            this._attributeData = attributeData;

            //var mappedTo = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "MapTo");
            //string mappedToStr = mappedTo.Key == null ? null : mappedTo.Value.Value.ToString();
        }

        public async Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();

            var model = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "StateMachineDefinitionPath");
            _model = model.Value.Value.ToString();

            var property = _attributeData.NamedArguments.FirstOrDefault(x => x.Key == "PropertyName");
            _propertyName = property.Value.Value.ToString();

            results = results.AddRange(await GenerateTriggersAndStates(context));

            return results;
        }

        protected virtual async Task<SyntaxList<MemberDeclarationSyntax>> GenerateTriggersAndStates(TransformationContext context)
        {
            //Debugger.Launch();
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            var applyToClass = (ClassDeclarationSyntax)context.ProcessingNode;

            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("Requests")).NormalizeWhitespace();
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")));
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("X.Core.Model")));

            StateMachineDefinition stateMachine = new StateMachineDefinition();
            stateMachine.Parse(_model, applyToClass.Identifier.ValueText, null);

            var stateEnum = SyntaxFactory.EnumDeclaration($"{applyToClass.Identifier.ValueText}StateMachineEnum")
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword)));

            int stateId = 1;
            foreach (var state in stateMachine.StateDefinitions)
            {

                stateEnum = stateEnum.AddMembers(SyntaxFactory.EnumMemberDeclaration(
                        SyntaxFactory.Identifier(state.StateName))
                    .WithEqualsValue(
                        SyntaxFactory.EqualsValueClause(
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.NumericLiteralExpression,
                                SyntaxFactory.Literal(stateId)))));

                stateId++;
            }

            results = results.Add(stateEnum);

            var triggerEnum = SyntaxFactory.EnumDeclaration($"{applyToClass.Identifier.ValueText}StateMachineTriggerEnum")
                .WithModifiers(
                    SyntaxFactory.TokenList(
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword)));

            int triggerId = 0;
            foreach (var trigger in stateMachine.TriggerDefinitions.Select(x=>x.TriggerName).Distinct())
            {
                triggerEnum = triggerEnum.AddMembers(SyntaxFactory.EnumMemberDeclaration(
                        SyntaxFactory.Identifier(trigger))
                    .WithEqualsValue(
                        SyntaxFactory.EqualsValueClause(
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.NumericLiteralExpression,
                                SyntaxFactory.Literal(triggerId++)))));

                //foreach trigger create request
                var classDeclaration = SyntaxFactory.ClassDeclaration($"{applyToClass.Identifier.ValueText}{trigger}Request");
                classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.PartialKeyword));

                @namespace = @namespace.AddMembers(classDeclaration);
            }
            results = results.Add(@namespace);

            results = results.Add(triggerEnum);


            return results;
        }
    }
}


