using System.Xml.Linq;

namespace SharpGs.Exceptions
{
    public sealed class InvalidBucketName : BaseGoogleException
    {
        public InvalidBucketName(XElement content)
            : base(content) { }
    }
}
