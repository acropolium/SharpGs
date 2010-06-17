using System;

namespace SharpGs
{
    public interface IObject : IAclSetup
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
