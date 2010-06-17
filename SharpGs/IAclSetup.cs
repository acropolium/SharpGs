using SharpGs.Acl;

namespace SharpGs
{
    public interface IAclSetup
    {
        IAccessControlList Acl { get; }
        void AclSave(IAccessControlList modifiedAcl);
    }
}
