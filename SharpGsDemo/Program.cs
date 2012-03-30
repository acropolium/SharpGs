using System;
using System.IO;
using System.Text;
using SharpGs;

namespace SharpGsDemo
{
    class Program
    {
        // Google keys pair - key & secret
        private const string AuthKey = @"put yours here";
        private const string AuthSecret = @"put yours here";

        static void Main()
        {
            var client = GoogleStorageFactory.Create(AuthKey, AuthSecret);

            //--- Proxy usage (implemented by community user Fabrizio)
            //IWebProxy proxy = HttpWebRequest.DefaultWebProxy;
            //proxy.Credentials = CredentialCache.DefaultCredentials;
            //client.WebProxy = proxy;

            for (var i = 0; i < 2; i++)
            {
                // Create a bucket
                var name = "temp-bucket-" + new Random().Next();
                client.CreateBucket(name);
            }

            // Fetching all buckets of user
            foreach (var bucket in client.Buckets)
            {
                Console.WriteLine("{0} - {1}", bucket.Name, bucket.CreationDate);

                // Simple buffer content
                bucket.AddObject("someobj/on" + new Random().Next(), Encoding.UTF8.GetBytes("Simple text"), "text/plain");
                // Streamed content (stream will be closed at the end)
                bucket.AddObject("someobj/stream-on" + new Random().Next(), GetStreamedString("Stream me!!!"), "text/plain", true);

                foreach (var o in bucket.Objects)
                {
                    Console.WriteLine("    {0} - {1}", o.Key, o.Size);
                    if (o.Key.StartsWith("someobj/stream-on"))
                    {
                        // Get streamed content
                        using (var ms = new MemoryStream())
                        {
                            o.Retrieve(ms);
                            ms.Seek(0, SeekOrigin.Begin);
                            using (var reader = new StreamReader(ms))
                            {
                                Console.WriteLine("      {0}", reader.ReadToEnd());
                            }
                        }
                    }
                    else
                    {
                        // Get simple content
                        Console.WriteLine("      {0}", Encoding.UTF8.GetString(o.Retrieve().Content));
                    }
                    o.Delete();
                }
                
                // Delete bucket
                if (bucket.Name.StartsWith("temp-bucket-"))
                    bucket.Delete();
            }

            Console.WriteLine("Finished");
            Console.ReadKey();
        }

        static Stream GetStreamedString(string data)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(data));
        }
    }
}
