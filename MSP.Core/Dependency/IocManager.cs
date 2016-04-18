using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Reflection;

namespace MSP.Core.Dependency
{
    public class IocManager:IIocManager
    {
        public readonly static IocManager Instance = new IocManager();
        public IWindsorContainer IocContainer { get; set; }

        private IocManager()
        {
            IocContainer = new WindsorContainer();
        }
        public void Dispose()
        {
            IocContainer.Dispose();
        }
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            //Transient
            IocContainer.Register(
                Classes.FromAssembly(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );

            //Singleton
            IocContainer.Register(
                Classes.FromAssembly(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ISingletonDependency>()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton()
                );

            //Windsor Interceptors
            IocContainer.Register(
                Classes.FromAssembly(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<IInterceptor>()
                    .WithService.Self()
                    .LifestyleTransient()
                );
        }

        public void RegisterAssemblyByBaseOn(Assembly assembly, Type BaseOn, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Singleton:
                    IocContainer.Register(Classes.FromAssembly(assembly).BasedOn(BaseOn).LifestyleSingleton());
                    break;
                case DependencyLifeStyle.Transient:
                    IocContainer.Register(Classes.FromAssembly(assembly).BasedOn(BaseOn).LifestyleTransient());
                    break;
                default:
                    IocContainer.Register(Classes.FromAssembly(assembly).BasedOn(BaseOn));
                    break;
            }
            
        }
        public void RegisterUsingFactoryMethod<T>(Func<T> func, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(typeof(T)).UsingFactoryMethod(func), lifeStyle));
        }
        public void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<T>(),lifeStyle));
        }
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
    }
}
