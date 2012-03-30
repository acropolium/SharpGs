using System.IO;

namespace SharpGs
{
    /// <summary>
    /// Google Storage Object information
    /// </summary>
    public interface IObject : IAclSetup, IObjectHead
    {
        /// <summary>
        /// Owner of the object
        /// </summary>
        IOwner Owner { get; }

        /// <summary>
        /// Class of storage in GS
        /// </summary>
        string StorageClass { get; }

        /// <summary>
        /// Request object form GS with body
        /// </summary>
        /// <returns></returns>
        IObjectContent Retrieve();

        /// <summary>
        /// Request object form GS into stream (memory, file, etc)
        /// </summary>
        /// <returns></returns>
        IObjectContent Retrieve(Stream targetStream);

        /// <summary>
        /// Delete current object from GS
        /// </summary>
        void Delete();
    }
}
