using System.Xml.Linq;

namespace SharpGs.Exceptions
{
    public sealed class SignatureDoesNotMatch : BaseGoogleException
    {
        public SignatureDoesNotMatch(XElement content)
            : base(content) { }
    }
}