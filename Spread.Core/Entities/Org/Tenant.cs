using MSP.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spread.Core.Entities.Org
{
    public class Tenant: EntityBase
    {
        public virtual string TenantName { get; set; }
    }
}
