using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSP.Core.Reflection
{
    public class CurrentAssemblyFinder : IAssemblyFinder
    {
        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}
