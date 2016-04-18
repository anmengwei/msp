using MSP.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spread.Core.Entities
{
    public class Account: EntityBase
    {
        public virtual string Email { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string HashPassWord { get; set; }
        public virtual string Salt { get; set; }
        public virtual string NickName { get; set; }
    }
}
