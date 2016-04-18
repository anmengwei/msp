using FluentNHibernate.Mapping;
using MSP.Core.Domain.Entities;

namespace MSP.NHibernate.EntityMapping
{
    public abstract class EntityMap<TEntity, TPrimaryKey> : ClassMap<TEntity> where TEntity : IEntity<TPrimaryKey>
    {
        protected EntityMap(string tableName)
        {
            Table(tableName);
            Id(m => m.Id);
        }
    }

    public abstract class EnityMap<Tentity> : EntityMap<Tentity, int> where Tentity : IEntity<int>
    {
        protected EnityMap(string tableName)
            :base(tableName)
        {

        }
    }
 }
