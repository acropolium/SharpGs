namespace SharpGs
{
    /// <summary>
    /// Google Storage Object head information
    /// </summary>
    public interface IObjectContent : IObjectHead
    {
        /// <summary>
        /// Content type of the object
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Request object content
        /// </summary>
        byte[] Content { get; }
    }
}
