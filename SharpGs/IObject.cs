using System;

namespace SharpGs
{
    public interface IObject
    {
        IBucket Bucket { get; }
        IOwner Owner { get; }
        string Key { get; }
        DateTime LastModified { get; }
        string ETag { get; }
        string StorageClass { get; }
        long Size { get; }

        IObjectData Get();
        void Delete();
    }
}
