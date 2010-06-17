using System;
using System.Linq;
using System.Xml.Linq;

namespace SharpGs.Acl
{
    internal class ScopeBuilder
    {
        private static Type FindScopeType(string name)
        {
            var type = typeof (ScopeBuilder).Assembly.GetType(typeof (ScopeBuilder).Namespace + ".Scopes." + name);
            if (type == null)
                throw new Exception("Scope " + name + " was not found");
            return type;
        }

        public static IScope CreateScope(ScopeType scopeType, params string[] scopeParameters)
        {
            var type = FindScopeType(scopeType.ToString());
            return (IScope) Activator.CreateInstance(type, scopeParameters);
        }

        public static IScope CreateScope(XElement scopeXml)
        {
            var type = FindScopeType(scopeXml.Attributes().Single().Value);
            return (IScope)Activator.CreateInstance(type, scopeXml);
        }
    }
}
