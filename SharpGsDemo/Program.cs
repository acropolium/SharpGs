using System;
using System.Collections.Generic;
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

            var bucketNames = new HashSet<string>();

            for (var i = 0; i < 10; i++)
            {
                // Create a bucket
                var name = "temp-bucket-" + new Random().Next();
                bucketNames.Add(name);
                client.CreateBucket(name);
            }

            // Fetching all buckets of user
            foreach (var bucket in client.Buckets)
            {
                Console.WriteLine("{0} - {1}", bucket.Name, bucket.CreationDate);

                for (var i = 0; i < 5; i++)
                {
                    bucket.AddObject("someobj/on" + new Random().Next(), new byte[] { 33, 77, 123, 34 }, "application/exe");
                }

                foreach (var o in bucket.Objects)
                {
                    Console.WriteLine("    {0} - {1}", o.Key, o.Size);
                    o.Delete();
                }
                
                // Delete bucket
                if (bucketNames.Contains(bucket.Name))
                    bucket.Delete();
            }

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
