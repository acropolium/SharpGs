using System;
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

            for (var i = 0; i < 10; i++)
            {
                // Create a bucket
                client.CreateBucket("temp-bucket-" + new Random().Next());
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
                bucket.Delete();
            }

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
