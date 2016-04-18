using MSP.Core.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSP.Core.Reflection
{
    public class TypeFinder: ITypeFinder
    {
        public IAssemblyFinder AssemblyFinder { get; set; }

        public TypeFinder(IAssemblyFinder AssemblyFinder)
        {
            this.AssemblyFinder = AssemblyFinder;
        }

        public List<Type> Find(Func<Type, bool> predicate)
        {
            return this.GetAllTypes().Where(predicate).ToList();
        }

        public List<Type> FindAll()
        {
            return this.GetAllTypes();
        }

        public List<Type> FindOfClassType<T>()
        {
            return Find(m => typeof(T).IsAssignableFrom(m) && !m.IsAbstract && m.IsClass);
        }

        private List<Type> GetAllTypes()
        {
            var allTypes = new List<Type>();

            foreach (var assembly in AssemblyFinder.GetAllAssemblies().Distinct())
            {
                try
                {
                    Type[] typesInThisAssembly;

                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly.IsNullOrEmpty())
                    {
                        continue;
                    }

                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                }
                catch (Exception ex)
                {
                    //
                }
            }

            return allTypes.Distinct().ToList();
        }
    }
}
