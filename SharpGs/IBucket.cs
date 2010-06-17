using System;

namespace SharpGs
{
    public interface IBucket : IAclSetup
    {
        string Name { get; }
        DateTime CreationDate { get; }
        //IEnumerable<IObject> Objects { get; }
        IObjectQuery Objects { get; }
        void AddObject(string key, byte[] content, string contentType);
        IObjectData GetObjectHead(string key);
        void Delete();
    }
}
