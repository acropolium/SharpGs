using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using SharpGs.Internal;

namespace SharpGs.RestApi
{
    internal class RestApiClient
    {
        private readonly Uri _uri;
        private readonly RequestMethod _method;
        private readonly IWebProxy _webProxy;

        public RestApiClient(Uri uri, RequestMethod method, IWebProxy proxy = null)
        {
            _uri = uri;
            _method = method;
            _webProxy = proxy;
        }

        internal static RequestMethod PureRequestMethod(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.ACL_GET:
                    return RequestMethod.GET;
                case RequestMethod.ACL_SET:
                    return RequestMethod.PUT;
                default:
                    return method;
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            var buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        private HttpWebRequest CreateRequest(string authValue, DateTime date, Stream content, string contentType)
        {
            var request = (HttpWebRequest)WebRequest.Create(_uri);
            request.Method = PureRequestMethod(_method).ToString();

            if (_webProxy != null)
                request.Proxy = _webProxy;

            request.Headers.Add(@"Authorization", authValue);
            if (content != null)
            {
                content.Seek(0, SeekOrigin.Begin);
                request.Headers.Add(@"Content-MD5", Convert.ToBase64String(MD5.Create().ComputeHash(content)));
            }
            request.Date = date;
            request.ContentLength = content == null ? 0 : content.Length;
            request.ContentType = contentType;
            request.KeepAlive = false;
            if (content != null)
            {
                var resultedStream = request.GetRequestStream();
                content.Seek(0, SeekOrigin.Begin);
                CopyStream(content, resultedStream);
            }
            return request;
        }

        private static bool FetchDataFromResponse(HttpWebResponse response, Bucket.ObjectHead objectHead, bool withData)
        {
            if (objectHead == null)
                return false;
            objectHead.Size = response.ContentLength;
            objectHead.ContentType = response.ContentType;
            objectHead.ETag = response.Headers["ETag"];
            objectHead.LastModified = response.LastModified;
            if (withData)
            {
                var stream = response.GetResponseStream();
                if (objectHead.TargetStream == null)
                {
                    objectHead.Content = new byte[objectHead.Size];
                    var read = (long) 0;
                    if (stream != null)
                    {
                        while (read < objectHead.Size)
                        {
                            var toread = (int) ((objectHead.Size - read)%4048);
                            read += stream.Read(objectHead.Content, 0, toread);
                        }
                        return true;
                    }
                }
                else
                {
                    if (stream != null)
                    {
                        CopyStream(stream, objectHead.TargetStream);
                        return true;
                    }
                }
            }
            return false;
        }

        private static string StreamToString(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public string Request(string authValue, DateTime date, Stream content, string contentType, Bucket.ObjectHead objectHead, bool withData)
        {
            try
            {
                var request = CreateRequest(authValue, date, content, contentType);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK && FetchDataFromResponse(response, objectHead, withData))
                        return String.Empty;
                    return StreamToString(response.GetResponseStream());
                }
            }
            catch (WebException exception)
            {
                return StreamToString(exception.Response.GetResponseStream());
            }
        }
    }
}
