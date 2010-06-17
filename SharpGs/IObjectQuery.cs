using System.Collections.Generic;

namespace SharpGs
{
    public interface IObjectQuery : IEnumerable<IObject>
    {
        /// <summary>
        /// used with list object requests to limit the number of objects that are
        /// returned in the list to only those with the given prefix
        /// </summary>
        /// <param name="prefixPath"></param>
        /// <returns></returns>
        IObjectQuery WithPrefix(string prefixPath);

        /// <summary>
        /// used with list object requests to specify the maximum number of objects
        /// you want in the returned list
        /// </summary>
        /// <param name="maxKeysCount"></param>
        /// <returns></returns>
        IObjectQuery WithLimitCount(int maxKeysCount);

        /// <summary>
        /// used with list object requests to specify which object you want the
        /// returned list to begin with
        /// </summary>
        /// <param name="markerObject"></param>
        /// <returns></returns>
        IObjectQuery WithMarker(string markerObject);

        /// <summary>
        /// used with list object requests to limit the scope of the returned list
        /// </summary>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        IObjectQuery WithDelimiter(string delimiter);
    }
}
