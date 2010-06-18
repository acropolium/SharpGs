namespace SharpGs
{
    /// <summary>
    /// Google Storage Object information
    /// </summary>
    public interface IObject : IAclSetup, IObjectHead
    {
        IOwner Owner { get; }
        string StorageClass { get; }
        IObjectContent Retrieve();
        void Delete();
    }
}
