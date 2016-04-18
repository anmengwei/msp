using Castle.Windsor;
using System;
using System.Reflection;

namespace MSP.Core.Dependency
{
    public interface IIocManager: IDisposable
    {
        IWindsorContainer IocContainer { get; set; }

        #region Registrar
        void RegisterAssemblyByConvention(Assembly assembly);
        void RegisterAssemblyByBaseOn(Assembly assembly, Type BaseOn, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        void RegisterUsingFactoryMethod<T>(Func<T> func, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) 
            where T : class;
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        bool IsRegistered(Type type);
        bool IsRegistered<TType>();
        #endregion

        #region Resolver
        T Resolve<T>();
        T Resolve<T>(Type type);
        object Resolve(Type type);
        void Release(object obj);
        #endregion
    }
}
