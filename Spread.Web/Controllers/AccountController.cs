using Spread.Application.User;
using Spread.Core.Entities;
using Spread.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spread.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AccountViewModule AccountModule)
        {
            accountService.Create(
                new Account()
                {
                    Email = AccountModule.Email,
                    Mobile = AccountModule.Mobile,
                    NickName = AccountModule.NickName,
                    HashPassWord = AccountModule.PassWord
                }
            );
            return null;
        }
    }
}