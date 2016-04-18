using System;
using System.Reflection;

namespace MSP.Core.Domain.Uow
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute: Attribute
    {
        public bool IsDisabled { get; set; }

        internal static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(UnitOfWorkAttribute), false);
            if (attrs.Length > 0)
            {
                return (UnitOfWorkAttribute)attrs[0];
            }

            if (UnitOfWorkHelper.IsConventionalUowClass(methodInfo.DeclaringType))
            {
                return new UnitOfWorkAttribute();
            }

            return null;
        }
    }
}
