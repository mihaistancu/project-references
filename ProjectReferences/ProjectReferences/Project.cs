using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ProjectReferences
{
    public class Project
    {
        public static List<string> References(string projectPath)
        {
            return XDocument.Load(projectPath)
                .Descendants().Where(e => e.Name.LocalName == "ProjectReference")
                .Select(e => FullPath(projectPath, e.Attribute("Include").Value)).ToList();
        }
        
        private static string FullPath(string parentPath, string relativePath)
        {
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(parentPath), relativePath));
        }
    }
}
