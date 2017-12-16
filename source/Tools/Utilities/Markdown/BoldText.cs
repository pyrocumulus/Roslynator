﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Utilities.Markdown
{
    public struct BoldText : IMarkdown
    {
        internal BoldText(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public void WriteTo(MarkdownWriter mw)
        {
            mw.WriteBoldDelimiter();
            mw.WriteMarkdown(Text);
            mw.WriteBoldDelimiter();
        }
    }
}