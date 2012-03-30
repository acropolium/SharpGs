using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SharpGs.Acl;
using SharpGs.Acl.Internal;
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

        public IObjectQuery Objects
        {
            get { return new Query(this, _connector); }
        }

        public void AddObject(string key, byte[] content, string contentType)
        {
            AddObject(key, new MemoryStream(content), contentType, true);
        }

        public void AddObject(string key, Stream stream, string contentType, bool closeStream = false)
        {
            try
            {
                _connector.RequestStream(RequestMethod.PUT, Name, key, stream, contentType);
            }
            finally
            {
                if (stream != null && closeStream)
                    stream.Close();
            }
        }

        internal class ObjectHead : IObjectContent
        {
            public IBucket Bucket { get; set; }
            public string ContentType { get; set; }
            public string Key { get; set; }
            public DateTime LastModified { get; set; }
            public long Size { get; set; }
            public string ETag { get; set; }
            public byte[] Content { get; set; }

            internal Stream TargetStream { get; set; }

            public ObjectHead(IBucket ownerBucket, Stream targetStream = null)
            {
                Bucket = ownerBucket;
                TargetStream = targetStream;
            }
        }

        public IObjectContent GetObjectHead(string key)
        {
            var oh = new ObjectHead(this);
            _connector.Request(RequestMethod.HEAD, Name, key, null, null, oh);
            return oh.ETag == null ? null : oh;
        }

        public void Delete()
        {
            _connector.Request(RequestMethod.DELETE, Name);
        }

        public IAccessControlList Acl
        {
            get
            {
                var result = _connector.Request(RequestMethod.ACL_GET, Name);
                return new AccessControlList(result, this);
            }
        }

        public void AclSave(IAccessControlList modifiedAcl)
        {
            _connector.Request(RequestMethod.ACL_SET, Name, null, Encoding.UTF8.GetBytes(modifiedAcl.ToXmlString()), "application/xml");
        }

        public static IBucket FromXml(XElement element, SharpGsClient connector)
        {
            var bucket = new Bucket(connector)
                             {
                                 Name = element.Descendants("Name").First().Value,
                                 CreationDate = DateTime.Parse(element.Descendants("CreationDate").First().Value)
                             };
            return bucket;
        }
    }
}
