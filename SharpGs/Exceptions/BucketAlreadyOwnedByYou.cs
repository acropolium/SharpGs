using System.Xml.Linq;

namespace SharpGs.Exceptions
{
    public sealed class BucketAlreadyOwnedByYou : BaseGoogleException
    {
        public BucketAlreadyOwnedByYou(XElement content)
            : base(content) { }
    }
}
