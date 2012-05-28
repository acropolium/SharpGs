using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SharpGs.RestApi;

namespace SharpGs.Cors.Internal
{
    public class Cors : ICors
    {
        private IOrigin Origins = new Origin();
        private IHttpMethods Methods = new HttpMethods();
        private IResponseHeader ResponseHeaders = new ResponseHeader();
        public int MaxAge { get; set; }
        public Cors()
        {            
            MaxAge = 1800;
        }
 
        public Cors(XDocument document)
        {
            foreach (var origin in document.Descendants("Origin"))
                Origins.AddOrigin(origin.Value);
            foreach (var method in document.Descendants("Method"))
                Methods.AddMethod(method.Value.ToUpper());
            foreach (var response in document.Descendants("ResponseHeader"))
                ResponseHeaders.AddResponseHeader(response.Value);
            var firstOrDefault = document.Descendants("MaxAgeSec").FirstOrDefault();
            if (firstOrDefault != null)
                MaxAge = Convert.ToInt16(firstOrDefault.Value);
        }
        public void AddOrigin(string origin)
        {
            Origins.AddOrigin(origin);
        }

        public void AddMethod(string method)
        {
            Methods.AddMethod(method);
        }

        public void AddHeader(string responseHeader)
        {
            ResponseHeaders.AddResponseHeader(responseHeader);
        }

        public string ToXmlString()
        {
            var sb = new StringBuilder("<?xml version='1.0' encoding='utf-8'?>").AppendLine();
            sb.AppendLine("<CorsConfig>");
            sb.AppendLine("\t<Cors>");
            sb.Append(Origins.ToXmlString());
            sb.Append(Methods.ToXmlString());
            sb.Append(ResponseHeaders.ToXmlString());
            sb.AppendLine("\t\t<MaxAgeSec>" + MaxAge + "</MaxAgeSec>");
            sb.AppendLine("\t</Cors>");
            sb.AppendLine("</CorsConfig>");
            return sb.ToString();
        }
    }
}
