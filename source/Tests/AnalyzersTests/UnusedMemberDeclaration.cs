﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

#pragma warning disable RCS1016, RCS1081, RCS1163, RCS1164, RCS1175, RCS1176

namespace Roslynator.CSharp.Analyzers.Tests
{
    internal static class UnusedMemberDeclaration
    {
        private static class Foo
        {
            private static readonly string _f;
            private static readonly string _f2, _f3;

            private static void FooMethod()
            {
                string s = _f2;

                EventHandler eh = FooEvent3;

                FooMethod<string>();

                s.FooExtensionMethod<string>();
            }

            private static void FooMethod<T>()
            {
            }

            private static string FooProperty { get; } = _f3;

            private static event EventHandler FooEvent;
            private static event EventHandler FooEvent2, FooEvent3;

            private delegate void FooDelegate();
        }

        private static void FooExtensionMethod<T>(this T value)
        {
        }

        // n

        private partial class FooPartial
        {
            private void FooMethod()
            {
            }
        }

        private partial class FooPartial
        {
        }

        private class Base
        {
            public Base(string value)
            {
            }
        }

        private class Derived : Base
        {
            public Derived(string value)
                : base(Bar())
            {
            }

            private static string Bar()
            {
                return null;
            }
        }
    }
}
