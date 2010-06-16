using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SharpGs.RestApi;

namespace SharpGs.Internal
{
    internal class Bucket : IBucket
    {
        private readonly SharpGsClient _connector;
        
        public Bucket(SharpGsClient connector)
        {
            _connector = connector;
        }

        public string Name
        {
            get; internal set;
        }

        public DateTime CreationDate
        {
            get; internal set;
        }

        public IEnumerable<IObject> Objects
        {
            get
            {
                return
                    _connector.Request(RequestMethod.GET, Name).Descendants(@"Contents").Select(
                        obj => GoogleObject.FromXml(obj, _connector, this));
            }
        }

        public void AddObject(string key, byte[] content, string contentType)
        {
            _connector.Request(RequestMethod.PUT, Name, key, content, contentType);
        }

        internal class ObjectHead : IObjectData
        {
            public IBucket Bucket { get; set; }
            public string ContentType { get; set; }
            public DateTime LastModified { get; set; }
            public string ETag { get; set; }
            public long ContentLength { get; set; }
            public byte[] Content { get; set; }
        }

        public IObjectData GetObjectHead(string key)
        {
            var oh = new ObjectHead {Bucket = this};
            _connector.Request(RequestMethod.HEAD, Name, key, null, null, oh);
            return oh.ETag == null ? null : oh;
        }

        public void Delete()
        {
            _connector.Request(RequestMethod.DELETE, Name);
        }

        public static IBucket FromXml(XElement element, SharpGsClient connector)
        {
            var bucket = new Bucket(connector);
            bucket.Name = element.Descendants("Name").First().Value;
            bucket.CreationDate = DateTime.Parse(element.Descendants("CreationDate").First().Value);
            return bucket;
        }
    }
}
