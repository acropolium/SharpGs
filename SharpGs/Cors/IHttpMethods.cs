namespace SharpGs.Cors
{
    public interface IHttpMethods
    {
        void AddMethod(string method);
        string ToXmlString();
    }
}
