using System;
using System.Collections.Generic;

namespace MSP.Core.Reflection
{
    public interface ITypeFinder
    {
        List<Type> FindOfClassType<T>();

        List<Type> Find(Func<Type, bool> predicate);

        List<Type> FindAll();
    }
}
