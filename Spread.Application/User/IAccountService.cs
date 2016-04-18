using Spread.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spread.Application.User
{
    public interface IAccountService
    {
        bool Create(Account account);

    }
}
