using System.Xml.Linq;

namespace SharpGs.Exceptions
{
    public sealed class BucketAlreadyExists : BaseGoogleException
    {
        public BucketAlreadyExists(XElement content)
            : base(content) { }
    }
}
