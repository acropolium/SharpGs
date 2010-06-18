namespace SharpGs
{
    /// <summary>
    /// GS item owner information
    /// </summary>
    public interface IOwner
    {
        /// <summary>
        /// Unique ID of the user in google system
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Display name for google user
        /// </summary>
        string DisplayName { get; }
    }
}
