using SharpGs.Internal;

namespace SharpGs
{
    public sealed class GoogleStorageFactory
    {
        /// <summary>
        /// Create new connector to the Google Storage
        /// </summary>
        /// <param name="authKey">authentication key provided by google</param>
        /// <param name="authSecret">authentication secret provided by google</param>
        /// <returns>connector to the service</returns>
        public static ISharpGs Create(string authKey, string authSecret)
        {
            return new SharpGsClient(authKey, authSecret);
        }
    }
}
