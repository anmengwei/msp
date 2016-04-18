using MSP.Core.Application;
using MSP.Core.Domain.Repositories;
using System;
using System.Reflection;

namespace MSP.Core.Domain.Uow
{
    internal static class UnitOfWorkHelper
    { 
        public static bool IsConventionalUowClass(Type type)
        {
            return typeof(IRepository).IsAssignableFrom(type) || typeof(IApplicationService).IsAssignableFrom(type);
        }

        public static bool HasUnitOfWorkAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(UnitOfWorkAttribute), false);
            if (attrs.Length <= 0)
            {
                return null;
            }

            return (UnitOfWorkAttribute)attrs[0];
        }
    }
}
