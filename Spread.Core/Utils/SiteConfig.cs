using System.Configuration;

namespace Spread.Core.Utils
{
    public class SiteConfig
    {
        /// <summary>
        /// 静态资源站点
        /// </summary>
        public static string StaticDomian
        {
            get
            {
                return ConfigurationManager.AppSettings["domain-static"] ?? string.Empty;
            }
        }
    }
}
