using MSP.NHibernate.EntityMapping;
using Spread.Core.Entities;

namespace Spread.Data.Mappings.User
{
    public class AccountMap: EnityMap<Account>
    {
        public AccountMap() : base("Account")
        {
            Map(m => m.Email).Length(50);
            Map(m => m.Mobile).Length(20);
            Map(m => m.NickName).Length(20);
            Map(m => m.HashPassWord).Length(50);
            Map(m => m.Salt).Length(50);
        }
    }
}
