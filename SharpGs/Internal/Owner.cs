using System.Linq;
using System.Xml.Linq;

namespace SharpGs.Internal
{
    internal class Owner : IOwner
    {
        private readonly SharpGsClient _connector;

        public Owner(SharpGsClient connector)
        {
            _connector = connector;
        }

        public string ID
        {
            get; internal set;
        }

        public string DisplayName
        {
            get; internal set;
        }

        public static IOwner FromXml(XElement element, SharpGsClient connector)
        {
            if (element == null)
                return null;
            var owner = new Owner(connector);
            owner.ID = element.Descendants("ID").Select(o => o.Value).FirstOrDefault();
            owner.DisplayName = element.Descendants("DisplayName").Select(o => o.Value).FirstOrDefault();
            return owner;
        }
    }
}
