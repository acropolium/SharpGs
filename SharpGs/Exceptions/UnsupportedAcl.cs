using System.Xml.Linq;

namespace SharpGs.Exceptions
{
    public sealed class UnsupportedAcl : BaseGoogleException
    {
        public UnsupportedAcl(XElement content)
            : base(content) { }
    }
}
