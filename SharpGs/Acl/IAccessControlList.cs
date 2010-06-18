using System.Collections.Generic;
using System.Xml.Linq;

namespace SharpGs.Acl
{
    /// <summary>
    /// Access control list of the GS item
    /// </summary>
    public interface IAccessControlList
    {
        /// <summary>
        /// Owner of the item
        /// </summary>
        IOwner Owner { get; }

        /// <summary>
        /// Entries of permissions
        /// </summary>
        IEnumerable<IAclEntry> Entries { get; }

        /// <summary>
        /// Create new permission entry inside Acl
        /// </summary>
        /// <param name="permission">type of granted permission</param>
        /// <param name="scopeType">scope of permission influence</param>
        /// <param name="scopeParameters">parameters for the scope</param>
        /// <returns></returns>
        IAclEntry AddEntry(AclPermission permission, ScopeType scopeType, params string[] scopeParameters);

        /// <summary>
        /// Remove specified entry from Acl
        /// </summary>
        /// <param name="entry">entry to remove</param>
        void RemoveEntry(IAclEntry entry);

        /// <summary>
        /// Remove all entries of the Acl
        /// </summary>
        void CleanEntries();

        string ToXmlString();
        XDocument ToXml();

        /// <summary>
        /// Store current state of Acl to GS server
        /// </summary>
        void Save();
    }
}
