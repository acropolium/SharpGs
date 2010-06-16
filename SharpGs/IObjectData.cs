using System;

namespace SharpGs
{
    public interface IObjectData
    {
        IBucket Bucket { get; }
        string ContentType { get; }
        DateTime LastModified { get; }
        string ETag { get; }
        long ContentLength { get; }
        byte[] Content { get; }
    }
}
