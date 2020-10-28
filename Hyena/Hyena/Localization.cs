//
// Localization.cs
//
// Author:
//   Matthew Jakeman <mjak923@aucklanduni.ac.nz>
//
// Copyright (C) 2020 Matthew Jakeman
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;

using NGettext;

namespace Hyena
{
    // Provides a static shim around NGettext
    // for building with .NET Core
    // Functionally equivalent to Mono.Unix.Catalog
    // We should transition to using NGettext directly
    public static class Catalog
    {
        private static NGettext.Catalog catalog = new NGettext.Catalog();

        public static string GetPluralString(string text, string pluralText, long n)
            => catalog.GetPluralString(text, pluralText, n);

        public static string GetPluralString(string text, string pluralText, long n, params object[] args)
            => catalog.GetPluralString(text, pluralText, n, args);

        public static string GetString(string text)
			=> catalog.GetString(text);
		
    }
}