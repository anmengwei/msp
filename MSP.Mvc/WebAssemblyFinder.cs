using MSP.Core.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;

namespace MSP.Mvc
{
    public class WebAssemblyFinder : IAssemblyFinder
    {
        public static SearchOption FindAssembliesSearchOption = SearchOption.TopDirectoryOnly;
        public List<Assembly> GetAllAssemblies()
        {
            var assembliesInBinFolder = new List<Assembly>();

            var allReferencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            var dllFiles = Directory.GetFiles(HttpRuntime.AppDomainAppPath + "bin\\", "*.dll", FindAssembliesSearchOption).ToList();

            foreach (string dllFile in dllFiles)
            {
                var locatedAssembly = allReferencedAssemblies.FirstOrDefault(asm => AssemblyName.ReferenceMatchesDefinition(asm.GetName(), AssemblyName.GetAssemblyName(dllFile)));
                if (locatedAssembly != null)
                {
                    assembliesInBinFolder.Add(locatedAssembly);
                }
            }
            return assembliesInBinFolder;
        }
    }
}
