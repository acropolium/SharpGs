using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using SharpGs.Internal;

namespace SharpGs.RestApi
{
    internal class RestApiClient : IDisposable
    {
        private readonly Uri _uri;
        private readonly string _method;

        public RestApiClient(Uri uri, string method)
        {
            _uri = uri;
            _method = method;
        }

        public string Request(string authValue, DateTime date, byte[] content, string contentType, Bucket.ObjectHead objectHead, bool withData)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(_uri);
                request.Method = _method;

                request.Headers.Add(@"Authorization", authValue);
                if (content != null)
                    request.Headers.Add(@"Content-MD5", Convert.ToBase64String(MD5.Create().ComputeHash(content)));
                request.Date = date;
                request.ContentLength = content == null ? 0 : content.Length;
                request.ContentType = contentType;
                request.KeepAlive = false;
                if (content != null)
                    request.GetRequestStream().Write(content, 0, content.Length);

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var isData = false;
                    if (response.StatusCode == HttpStatusCode.OK && objectHead != null)
                    {
                        objectHead.ContentLength = response.ContentLength;
                        objectHead.ContentType = response.ContentType;
                        objectHead.ETag = response.Headers["ETag"];
                        objectHead.LastModified = response.LastModified;
                        if (withData)
                        {
                            isData = true;
                            objectHead.Content = new byte[objectHead.ContentLength];
                            var read = (long)0;
                            var stream = response.GetResponseStream();
                            while (read < objectHead.ContentLength)
                            {
                                var toread = (int)((objectHead.ContentLength - read)%4048);
                                read += stream.Read(objectHead.Content, 0, toread);
                            }
                        }
                    }
                    if (!isData)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                    return String.Empty;
                }
            }
            catch (WebException exception)
            {
                using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
