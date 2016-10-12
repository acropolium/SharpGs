using System;
using System.IO;
using SharpGs.Cors;

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
        /// Put new object to the bucket
        /// </summary>
        /// <param name="key">object path/key</param>
        /// <param name="stream">stream data</param>
        /// <param name="contentType">object content type</param>
        /// <param name="closeStream">flag to close stream after operation</param>
        void AddObject(string key, Stream stream, string contentType, bool closeStream = false);

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

        /// <summary>
        /// Retrieve Cross origin resource sharing object for the bucket
        /// </summary>
        ICors Cors { get; }

        /// <summary>
        /// Save a new resource sharing object for the bucket
        /// </summary>
        /// <param name="cors"></param>
        void CorsSave(ICors cors);

    }
}
