using System;
using System.Collections.Generic;
using Adamant.Tools.Compiler.Bootstrap.Framework;
using JetBrains.Annotations;

namespace Adamant.Tools.Compiler.Bootstrap.Names
{
    public class SimpleName : Name, IEquatable<SimpleName>
    {
        [NotNull] public override SimpleName UnqualifiedName => this;
        [NotNull] public readonly string Text;
        [NotNull] public readonly bool IsSpecial;

        public SimpleName([NotNull] string text)
            : this(text, false)
        {
        }

        public SimpleName([NotNull] string text, bool isSpecial)
        {
            Requires.NotNull(nameof(text), text);
            Text = text;
            IsSpecial = isSpecial;
        }

        public override IEnumerable<SimpleName> Segments => this.Yield();

        public override bool HasQualifier([NotNull] Name name)
        {
            Requires.NotNull(nameof(name), name);
            // A simple name doesn't have a qualifier
            return false;
        }

        public override string ToString()
        {
            // TODO deal with IsSpecial and Special chars like space
            return Text;
        }

        #region Equals
        public override bool Equals(object obj)
        {
            return Equals(obj as SimpleName);
        }

        public bool Equals(SimpleName other)
        {
            return other != null &&
                   Text == other.Text &&
                   IsSpecial == other.IsSpecial;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Text, IsSpecial);
        }

        public static bool operator ==(SimpleName name1, SimpleName name2)
        {
            return EqualityComparer<SimpleName>.Default.Equals(name1, name2);
        }

        public static bool operator !=(SimpleName name1, SimpleName name2)
        {
            return !(name1 == name2);
        }
        #endregion
    }
}
