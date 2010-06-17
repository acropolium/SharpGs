using System.Text;

namespace SharpGs.Internal
{
    internal static class StringBuilderHelper
    {
        internal static StringBuilder AppendParam(this StringBuilder builder, string key, string value)
        {
            if (builder.Length > 0)
                builder.Append('&');
            builder.Append(key);
            builder.Append('=');
            builder.Append(value);
            return builder;
        }
    }
}
