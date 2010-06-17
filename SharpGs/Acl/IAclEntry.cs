namespace SharpGs.Acl
{
    public interface IAclEntry
    {
        AclPermission Permission { get; }
        IScope Scope { get; }
        string ToXmlString();
    }
}
