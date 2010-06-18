using System;

namespace SharpGs
{
    /// <summary>
    /// Google Storage Bucket information
    /// </summary>
    public interface IBucket : IAclSetup
    {
        /// <summary>
        /// Name of the bucket
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Creation date of the bucket
        /// </summary>
        DateTime CreationDate { get; }

        /// <summary>
        /// Query bucket for objects
        /// </summary>
        IObjectQuery Objects { get; }

        /// <summary>
        /// Put new object to the bucket
        /// </summary>
        /// <param name="key">object path/key</param>
        /// <param name="content">object body</param>
        /// <param name="contentType">object content type</param>
        void AddObject(string key, byte[] content, string contentType);

        /// <summary>
        /// Retrieve only header of the object
        /// </summary>
        /// <param name="key">object path/key</param>
        /// <returns>header information</returns>
        IObjectContent GetObjectHead(string key);

        /// <summary>
        /// Delete current bucket
        /// </summary>
        void Delete();
    }
}
