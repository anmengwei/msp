using MSP.Core.Domain.Repositories;
using Spread.Core.Entities.Wx;

namespace Spread.Application
{
    public class WeChatService: ServiceBase<WeChat>, IWeChatService
    {
        private readonly IRepository<WeChat> _repWeChat;
        public WeChatService(IRepository<WeChat> repWeChat)
            :base(repWeChat)
        {
            this._repWeChat = repWeChat;
        }


        //[UnitOfWork(IsDisabled = true)]
        //public void Add(WeChat weChat)
        //{
        //    _repWeChat.Insert(weChat);
        //}

        //public void BatAdd(List<WeChat> weChats)
        //{
        //    foreach (var item in weChats)
        //    {
        //        _repWeChat.Insert(item);
        //    }
        //}

        //public string Say()
        //{
        //    return "hello autofac";
        //}
    }
}
