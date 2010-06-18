using SharpGs.Acl;

namespace SharpGs
{
    /// <summary>
    /// Interface says that GS item supports permissions
    /// </summary>
    public interface IAclSetup
    {
        IAccessControlList Acl { get; }
        void AclSave(IAccessControlList modifiedAcl);
    }
}
