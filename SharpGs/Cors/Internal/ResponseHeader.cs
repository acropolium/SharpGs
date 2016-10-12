using System.Collections.Generic;
using System.Text;

namespace SharpGs.Cors.Internal
{
    internal class ResponseHeader: IResponseHeader
    {
        public List<string> ResponseHeaders = new List<string>(); 
        public void AddResponseHeader(string responseHeader)
        {
            ResponseHeaders.Add(responseHeader);
        }

        public string ToXmlString()
        {
            if (ResponseHeaders.Count == 0)
                return null;
            var sb = new StringBuilder();
            sb.AppendLine("\t\t<ResponseHeaders>");
            foreach (var responseHeader in ResponseHeaders)
            {
                sb.AppendLine(string.Format("\t\t\t<ResponseHeader>{0}<ResponseHeader>", responseHeader));
            }
            return sb.AppendLine("\t\t</ResponseHeaders>").ToString();
        }
    }
}
