using System;
using System.Linq;
using System.Xml.Linq;
using SharpGs.RestApi;

namespace SharpGs.Internal
{
    internal class GoogleObject : IObject
    {
        private readonly SharpGsClient _connector;

        public GoogleObject(SharpGsClient connector, IBucket bucket)
        {
            _connector = connector;
            Bucket = bucket;
        }

        public IBucket Bucket
        {
            get; internal set;
        }

        public IOwner Owner
        {
            get; internal set;
        }

        public string Key
        {
            get; internal set;
        }

        public DateTime LastModified
        {
            get;
            internal set;
        }

        public string ETag
        {
            get;
            internal set;
        }

        public string StorageClass
        {
            get;
            internal set;
        }

        public long Size
        {
            get;
            internal set;
        }

        public IObjectData Get()
        {
            var oh = new Bucket.ObjectHead { Bucket = Bucket };
            _connector.Request(RequestMethod.GET, Bucket.Name, Key, null, null, oh, true);
            return oh.ETag == null ? null : oh;
        }

        public void Delete()
        {
            _connector.Request(RequestMethod.DELETE, Bucket.Name, Key);
        }

        public static IObject FromXml(XElement element, SharpGsClient connector, IBucket bucket)
        {
            var obj = new GoogleObject(connector, bucket);
            obj.ETag = element.Descendants("ETag").Select(o => o.Value).FirstOrDefault();
            obj.Key = element.Descendants("Key").Select(o => o.Value).FirstOrDefault();
            obj.LastModified = DateTime.Parse(element.Descendants("LastModified").Select(o => o.Value).FirstOrDefault() ?? DateTime.MinValue.ToString());
            obj.Size = long.Parse(element.Descendants("Size").Select(o => o.Value).FirstOrDefault() ?? "0");
            obj.StorageClass = element.Descendants("StorageClass").Select(o => o.Value).FirstOrDefault();
            obj.Owner = Internal.Owner.FromXml(element.Descendants("Owner").FirstOrDefault(), connector);
            return obj;
        }
    }
}
