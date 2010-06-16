using System.Xml.Linq;

namespace SharpGs.Exceptions
{
    public sealed class BucketNameUnavailable : BaseGoogleException
    {
        public BucketNameUnavailable(XElement content)
            : base(content) { }
    }
}
