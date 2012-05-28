using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGs.Cors.Internal
{
    internal class Origin : IOrigin
    {
        public List<string> Origins;
        public Origin()
        {
            Origins = new List<string>();            
        }

        public void AddOrigin(string origin)
        {
            Origins.Add(origin);
        }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\t\t<Origins>");
            foreach (var origin in Origins)
            {
                sb.AppendLine(String.Format("\t\t\t<Origin>{0}</Origin>", origin));
            }            
            return sb.AppendLine("\t\t</Origins>").ToString();           
        }
        
    }
}
