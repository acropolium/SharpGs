using System;
using System.Collections.Generic;

namespace SharpGs
{
    /// <summary>
    /// Google Storage service connector
    /// </summary>
    public interface ISharpGs : IDisposable
    {
        /// <summary>
        /// Get the AuthKey of GoogleStorage account
        /// </summary>
        string AuthKey { get; }

        /// <summary>
        /// Get the AuthSecret of GoogleStorage account
        /// </summary>
        string AuthSecret { get; }

        /// <summary>
        /// If true, uses https secured protocol. True by default
        /// </summary>
        bool SecuredConnection { get; set; }

        /// <summary>
        /// Request server for list of buckets
        /// https://code.google.com/apis/storage/docs/reference-methods.html#getservice
        /// </summary>
        IEnumerable<IBucket> Buckets { get; }

        /// <summary>
        /// Create new bucket. If bucket with same name already exists in GS, exception will be thrown.
        /// https://code.google.com/apis/storage/docs/reference-methods.html#putbucket
        /// </summary>
        /// <param name="name">unique name of the bucket</param>
        void CreateBucket(string name);

        /// <summary>
        /// Get the bucket info
        /// </summary>
        /// <param name="name">name of the bucket</param>
        /// <returns>bucket information</returns>
        IBucket GetBucket(string name);
    }
}
