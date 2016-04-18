using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MSP.Core;
using MSP.Core.Dependency;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;

namespace Spread.Data
{
    public class SpreadDataModule: MSPModule
    {
        public override void Initialize()
        {
            IocManager.Instance.RegisterUsingFactoryMethod<ISessionFactory>(() =>
            {
                return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey("Default")).AdoNetBatchSize(1000).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, false))
                .BuildSessionFactory();
            });
            IocManager.Instance.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

    }
}
