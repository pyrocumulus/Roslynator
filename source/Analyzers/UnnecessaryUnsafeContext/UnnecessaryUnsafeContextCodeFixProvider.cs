﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp.CodeFixes;

namespace Roslynator.CSharp.Analyzers.UnnecessaryUnsafeContext
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UnnecessaryUnsafeContextCodeFixProvider))]
    [Shared]
    public class UnnecessaryUnsafeContextCodeFixProvider : BaseCodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(DiagnosticIdentifiers.UnnecessaryUnsafeContext); }
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.GetSyntaxRootAsync().ConfigureAwait(false);

            if (!TryFindToken(root, context.Span.Start, out SyntaxToken token, kind: SyntaxKind.UnsafeKeyword))
                return;

            Diagnostic diagnostic = context.Diagnostics[0];

            SyntaxNode parent = token.Parent;

            if (parent is UnsafeStatementSyntax unsafeStatement)
            {
                CodeAction codeAction = CodeAction.Create(
                    "Remove unsafe context",
                    cancellationToken => UnnecessaryUnsafeContextRefactoring.RefactorAsync(context.Document, unsafeStatement, cancellationToken),
                    GetEquivalenceKey(diagnostic));

                context.RegisterCodeFix(codeAction, diagnostic);
            }
            else
            {
                ModifiersCodeFixRegistrator.RemoveModifier(context, diagnostic, parent, token);
            }
        }
    }
}
