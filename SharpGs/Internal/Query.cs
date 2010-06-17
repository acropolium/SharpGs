using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGs.RestApi;

namespace SharpGs.Internal
{
    internal class Query : IObjectQuery
    {
        private readonly IBucket _bucket;
        private readonly SharpGsClient _connector;

        public Query(IBucket bucket, SharpGsClient connector)
        {
            _bucket = bucket;
            _connector = connector;
        }

        private string _prefixPath;
        public IObjectQuery WithPrefix(string prefixPath)
        {
            _prefixPath = prefixPath;
            return this;
        }

        private int _maxKeysCount = -1;
        public IObjectQuery WithLimitCount(int maxKeysCount)
        {
            _maxKeysCount = maxKeysCount;
            return this;
        }

        private string _markerObject;
        public IObjectQuery WithMarker(string markerObject)
        {
            _markerObject = markerObject;
            return this;
        }

        private string _delimiter;
        public IObjectQuery WithDelimiter(string delimiter)
        {
            _delimiter = delimiter;
            return this;
        }

        private string Parameters
        {
            get
            {
                var sb = new StringBuilder();
                if (!String.IsNullOrEmpty(_delimiter))
                    sb.AppendParam(@"delimiter", _delimiter);
                if (_maxKeysCount > 0)
                    sb.AppendParam(@"max-keys", _maxKeysCount.ToString());
                if (!String.IsNullOrEmpty(_markerObject))
                    sb.AppendParam(@"marker", _markerObject);
                if (!String.IsNullOrEmpty(_prefixPath))
                    sb.AppendParam(@"prefix", _prefixPath);
                return sb.Length > 0 ? sb.ToString() : null;
            }
        }

        public IEnumerator<IObject> GetEnumerator()
        {
            return _connector.Request(RequestMethod.GET, _bucket.Name, parameters: Parameters)
                .Descendants(@"Contents").Select(obj => GoogleObject.FromXml(obj, _connector, _bucket))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
