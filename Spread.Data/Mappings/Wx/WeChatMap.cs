using MSP.NHibernate.EntityMapping;
using Spread.Core.Entities.Wx;
namespace Spread.Data.Mappings.Wx
{
    public class WeChatMap: EnityMap<WeChat>
    {
        public WeChatMap()
            :base("WeChat")
        {
            Map(x => x.Name).Length(20);
            Map(x => x.AppId).Length(50);
            Map(x => x.AppSecret).Length(50);
            Map(x => x.EncodingAESkey).Length(50);
            Map(x => x.Token).Length(50);
            Map(x => x.Url).Length(200);
        }
    }
}
