using MSP.Core.Application;
using MSP.Core.Domain.Repositories;
using MSP.Core.Utils;
using Spread.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spread.Application.User
{
    public class AccountService : ApplicationService, IAccountService
    {
        private readonly IRepository<Account> _repAccount;

        public AccountService(IRepository<Account> repAccount)
        {
            _repAccount = repAccount;
        }

        public bool Create(Account account)
        {
            account.Salt = Guid.NewGuid().ToString("N");
            account.HashPassWord = SecurityUtil.MD5(account.HashPassWord + account.Salt);
            return _repAccount.InsertAndGetId(account) > 0;
        }
    }
}
