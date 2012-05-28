using SharpGs.RestApi;

namespace SharpGs.Cors
{
    /// <summary>
    /// Setup Cross Origin 
    /// </summary>
    public interface ICors
    {

        /// <summary>
        /// Adds an origin to the origin collection. 
        /// 
        /// An Origin permitted for cross origin resource sharing with this Google Cloud Storage bucket. 
        /// For example, http://origin1.example.com. You can use wildcards ("*"). However, if the host part of the Origin begins with a *, 
        /// then any origin that ends with the same suffix will be considered a match. If you supply a value that consists of only
        /// the wildcard (<Origin>*</Origin>), this gives access to ALL origins.
        /// </summary>
        /// <param name="origin">The origin to add</param>
        void AddOrigin(string origin);

        /// <summary>
        /// Adds a request method to the collection of methods supported in this configuration. Valid values are GET, HEAD, PUT, POST, and DELETE.        
        /// 
        /// </summary>
        /// <param name="method"></param>
        void AddMethod(string method);

        /// <summary>
        ///  Adds a response header/s that the user agent is permitted to share across origins.
        /// </summary>
        void AddHeader(string responseHeader);

        /// <summary>
        /// This value is used to respond to preflight requests, indicating the number of seconds that the client (browser) is allowed to make 
        /// requests before the client must repeat the preflight request. (Indicates cache expiry time.) Preflight requests are required if 
        /// the request method contains non-simple headers or if the request method is not POST, GET, or HEAD. The value is returned in the 
        /// Access-Control-Max-Age header in responses to preflight requests.
        /// </summary>
        int MaxAge { get; set; }


        string ToXmlString();
    }
}
