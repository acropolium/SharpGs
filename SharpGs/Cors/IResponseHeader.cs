namespace SharpGs.Cors
{
    public interface IResponseHeader
    {
        void AddResponseHeader(string responseHeader);
        string ToXmlString();
    }
}
