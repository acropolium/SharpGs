using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SharpGs.Acl;
using SharpGs.Acl.Internal;
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
            var obj = new GoogleObject(connector, bucket)
                          {
                              ETag = element.Descendants("ETag").Select(o => o.Value).FirstOrDefault(),
                              Key = element.Descendants("Key").Select(o => o.Value).FirstOrDefault(),
                              LastModified =
                                  DateTime.Parse(
                                      element.Descendants("LastModified").Select(o => o.Value).FirstOrDefault() ??
                                      DateTime.MinValue.ToString()),
                              Size =
                                  long.Parse(element.Descendants("Size").Select(o => o.Value).FirstOrDefault() ?? "0"),
                              StorageClass = element.Descendants("StorageClass").Select(o => o.Value).FirstOrDefault(),
                              Owner = Internal.Owner.FromXml(element.Descendants("Owner").FirstOrDefault(), connector)
                          };
            return obj;
        }

        public IAccessControlList Acl
        {
            get
            {
                var result = _connector.Request(RequestMethod.ACL_GET, Bucket.Name, Key);
                return new AccessControlList(result, this);
            }
        }

        public void AclSave(IAccessControlList modifiedAcl)
        {
            _connector.Request(RequestMethod.ACL_SET, Bucket.Name, Key, Encoding.UTF8.GetBytes(modifiedAcl.ToXmlString()), "application/xml");
        }
    }
}
