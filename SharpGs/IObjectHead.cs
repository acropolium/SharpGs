using System;

namespace SharpGs
{
    /// <summary>
    /// Common headers of GS object info
    /// </summary>
    public interface IObjectHead
    {
        /// <summary>
        /// Link to owning bucket
        /// </summary>
        IBucket Bucket { get; }

        /// <summary>
        /// Object key inside the bucket
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Object last modified date
        /// </summary>
        DateTime LastModified { get; }

        /// <summary>
        /// Object's ETag unique number
        /// </summary>
        string ETag { get; }

        /// <summary>
        /// Size of the object content
        /// </summary>
        long Size { get; }
    }
}
