using System;
using System.Collections.Generic;

namespace SharpGs
{
    public interface IBucket
    {
        string Name { get; }
        DateTime CreationDate { get; }
        IEnumerable<IObject> Objects { get; }
        void AddObject(string key, byte[] content, string contentType);
        IObjectData GetObjectHead(string key);
        void Delete();
    }
}
