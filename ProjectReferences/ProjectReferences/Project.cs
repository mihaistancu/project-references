using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ProjectReferences
{
    public class Project
    {
        private static readonly Dictionary<string, List<string>> cache = new Dictionary<string, List<string>>();

        public static List<string> DirectReferences(string projectPath)
        {
            if (!cache.ContainsKey(projectPath))
            {
                cache.Add(projectPath, References(projectPath));
            }

            return cache[projectPath];
        }

        private static List<string> References(string projectPath)
        {
            return XDocument.Load(projectPath)
                .Descendants().Where(e => e.Name.LocalName == "ProjectReference")
                .Select(e => FullPath(projectPath, e.Attribute("Include").Value)).ToList();
        }
        
        private static string FullPath(string parentPath, string relativePath)
        {
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(parentPath), relativePath));
        }
        
        public static HashSet<string> AllReferences(string projectPath)
        {
            var hashSet = new HashSet<string>();
            var directReferences = DirectReferences(projectPath);
            hashSet.UnionWith(directReferences);

            foreach (var reference in directReferences)
            {
                var indirectReferences = AllReferences(reference);
                hashSet.UnionWith(indirectReferences);
            }
            
            return hashSet;
        }
    }
}
