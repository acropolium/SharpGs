using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using SharpGs.Internal;
using SharpGs.RestApi;

namespace SharpGs
{
    public class SharpGsClient
    {
        private const string DefaultGoogleHost = @"commondatastorage.googleapis.com";

        protected string GoogleStorageHost
        {
            get; set;
        }

        protected string AuthKey
        {
            get; private set;
        }

        protected string AuthSecret
        {
            get; private set;
        }

        protected bool SecuredConnection
        {
            get; set;
        }

        private Uri ConnectionUrl(RequestMethod requestMethod, string bucketName = null, string path = null, string parameters = null)
        {
            var bucket = bucketName == null ? String.Empty : bucketName + '.';
            return
                new Uri(String.Format("{0}://{1}{2}/{3}{4}", SecuredConnection ? Uri.UriSchemeHttps : Uri.UriSchemeHttp,
                                      bucket, GoogleStorageHost, path, GetAdditionalParameters(requestMethod, parameters)));
        }

        private static string GetAdditionalParameters(RequestMethod requestMethod, string parameters)
        {
            if (requestMethod == RequestMethod.ACL_GET || requestMethod == RequestMethod.ACL_SET)
                return "?acl";
            if (parameters != null)
                return "?" + parameters;
            return String.Empty;
        }

        public SharpGsClient(string key, string secret)
        {
            AuthKey = key;
            AuthSecret = secret;
            SecuredConnection = true;
            GoogleStorageHost = DefaultGoogleHost;
        }

        #region Request Generation

        private static string SyndicateCanonicalHeaders(RequestMethod requestMethod, string contentMd5, string contentType, string date)
        {
            return String.Format("{0}\n{1}\n{2}\n{3}\n", RestApiClient.PureRequestMethod(requestMethod), contentMd5, contentType, date);
        }

        private static string SyndicateCanonicalResource(RequestMethod requestMethod, string bucket, string path)
        {
            var sb = new StringBuilder("/");
            if (bucket != null)
            {
                sb.Append(bucket);
                sb.Append("/");
                if (path != null)
                    sb.Append(path);
                if (requestMethod == RequestMethod.ACL_GET || requestMethod == RequestMethod.ACL_SET)
                    sb.Append("?acl");
            }
            return sb.ToString();
        }

        private static string SyndicateAuthValue(string key, string signature)
        {
            return String.Format(@"GOOG1 {0}:{1}", key, signature);
        }

        internal XDocument Request(RequestMethod requestMethod = RequestMethod.GET, string bucket = null, string path = null, byte[] content = null, string contentType = null, Bucket.ObjectHead objectHead = null, bool withData = false, string parameters = null)
        {
            var contentTypeFixed = contentType ?? @"application/xml";
            var dateO = DateTime.UtcNow;
            var date = dateO.ToString(@"ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.GetCultureInfo("EN-US"));

            var canonicalHeaders = SyndicateCanonicalHeaders(requestMethod,
                                                             content == null
                                                                 ? null
                                                                 : Convert.ToBase64String(
                                                                     MD5.Create().ComputeHash(content)),
                                                             contentTypeFixed,
                                                             date);
            var canonicalResource = SyndicateCanonicalResource(requestMethod, bucket, path);

            var signatureOrigin = String.Format("{0}{1}", canonicalHeaders, canonicalResource);
            var signature = Convert.ToBase64String(new HMACSHA1(Encoding.UTF8.GetBytes(AuthSecret)).ComputeHash(Encoding.UTF8.GetBytes(signatureOrigin)));

            using (var api = new RestApiClient(ConnectionUrl(requestMethod, bucket, path, parameters), requestMethod))
            {
                var result = api.Request(SyndicateAuthValue(AuthKey, signature), dateO, content, contentTypeFixed, objectHead, withData);
                if (String.IsNullOrEmpty(result))
                    return null;
                var responce = XDocument.Parse(FilterResponse(result));
                var error = responce.Descendants(@"Error").FirstOrDefault();
                if (error == null)
                    return responce;
                throw error.FindException();
            }
        }

        private static string FilterResponse(string response)
        {
            var p = response;
            while (true)
            {
                var ptr = p.IndexOf(" xmlns=");
                if (ptr < 0)
                    break;
                var ptrE = p.IndexOf(p[ptr + 7], ptr + 8);
                if (ptrE <= ptr)
                    break;
                p = p.Substring(0, ptr) + p.Substring(ptrE + 1);
            }
            return p;
        }

        #endregion Request Generation

        public IEnumerable<IBucket> Buckets
        {
            get
            {
                return Request().Descendants(@"Bucket").Select(bucket => Bucket.FromXml(bucket, this));
            }
        }

        public IBucket GetBucket(string name)
        {
            return Buckets.Where(b => b.Name.Equals(name)).FirstOrDefault();
        }

        public void AddBucket(string name)
        {
            Request(RequestMethod.PUT, name);
        }
    }
}
