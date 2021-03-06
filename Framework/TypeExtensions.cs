using System;
using System.Linq;

namespace Adamant.Tools.Compiler.Bootstrap.Framework
{
    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            if (type.IsGenericParameter || !type.IsGenericType)
                return type.Name;

            var name = type.Name;
            var index = name.IndexOf("`", StringComparison.Ordinal);
            name = name.Substring(0, index);
            var genericArguments = string.Join(',', type.GetGenericArguments().Select(GetFriendlyName));
            return $"{name}<{genericArguments}>";
        }
    }
}
