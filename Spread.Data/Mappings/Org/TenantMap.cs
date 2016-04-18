using MSP.NHibernate.EntityMapping;
using Spread.Core.Entities.Org;

namespace Spread.Data.Mappings.Org
{
    public class TenantMap : EnityMap<Tenant>
    {
        public TenantMap()
            : base("Tenant")
        {
            Map(m => m.TenantName).Length(150);
        }
    }
}
