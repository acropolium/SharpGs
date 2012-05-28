using System.Collections.Generic;
using System.Text;

namespace SharpGs.Cors.Internal
{
    internal class HttpMethods : IHttpMethods
    {
        public List<string> RequestMethods;

        public HttpMethods()
        {
            RequestMethods = new List<string>();    
        }

        public void AddMethod(string method)
        {
            RequestMethods.Add(method);
        }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\t\t<Methods>");
            foreach (var requestMethod in RequestMethods)
            {                
                sb.AppendLine(string.Format("\t\t\t<Method>{0}</Method>", requestMethod));
            }
            return sb.AppendLine("\t\t</Methods>").ToString();
        }
    }
}
