using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Framework
{
    public static class DictionaryExtensions
    {
        [Obsolete("Use ToFixedDictionary() instead")]
        [NotNull]
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

        [NotNull]
        public static FixedDictionary<TKey, TValue> ToFixedDictionary<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> dictionary)
        {
            return new FixedDictionary<TKey, TValue>(dictionary);
        }
    }
}
