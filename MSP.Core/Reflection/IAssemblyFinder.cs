using System.Collections.Generic;
using System.Reflection;

namespace MSP.Core.Reflection
{
    public interface IAssemblyFinder
    {
        List<Assembly> GetAllAssemblies();
    }
}
