namespace SharpGs.Cors
{
    public interface IOrigin
    {
        void AddOrigin(string origin);
        string ToXmlString();
    }
}
