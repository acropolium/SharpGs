namespace SharpGs.Acl
{
    /// <summary>
    /// One access control list entry
    /// </summary>
    public interface IAclEntry
    {
        /// <summary>
        /// Permission of the entry
        /// </summary>
        AclPermission Permission { get; }
        /// <summary>
        /// Scope of influence
        /// </summary>
        IScope Scope { get; }
        string ToXmlString();
    }
}
