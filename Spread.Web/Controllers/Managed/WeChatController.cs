using Newtonsoft.Json;
using Spread.Application;
using Spread.Core.Entities.Wx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spread.Web.Controllers.Managed
{
    [RoutePrefix("managed/wechat")]
    public class WeChatController : ManagedBaseController
    {
        private readonly IWeChatService _WeChatService;

        public WeChatController(IWeChatService weChatService)
        {
            this._WeChatService = weChatService;
        }
        /// <summary>
        /// 微信账号列表页面
        /// </summary>
        /// <returns></returns>
        [Route]
        public ActionResult Index()
        {
            return View("~/Views/Managed/WeChat/Index.cshtml");
        }

        /// <summary>
        /// 微信账号列表数据
        /// </summary>
        /// <returns></returns>
        [Route("Search")]
        public ActionResult Search(int draw, int PageIndex,int PageSize)
        {
            var res = this._WeChatService.PageList(null, PageIndex, PageSize);
            var returnData = new
            {
                draw = draw,
                iTotalRecords = res.TotalCount,
                recordsFiltered = res.TotalCount,
                data = res
            };
            return Content(JsonConvert.SerializeObject(returnData));
        }

        /// <summary>
        /// 绑定微信账号
        /// </summary>
        /// <returns></returns>
        [Route("bind")]
        [HttpGet]
        public ActionResult BindAccount()
        {
            return View("~/Views/Managed/WeChat/Index.cshtml");
        }

        /// <summary>
        /// 绑定微信账号
        /// </summary>
        /// <returns></returns>
        [Route("bind")]
        [HttpPost]
        public ActionResult BindAccount(WeChat WeChat)
        {
            return View("~/Views/Managed/WeChat/Index.cshtml");
        }
    }
}