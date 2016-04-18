using MSP.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spread.Core.Entities.Wx
{
    public class WeChat: EntityBase
    {
        [DisplayName("名称")]
        public virtual string Name { get; set; }
        [DisplayName("AppId")]
        public virtual string AppId { get; set; }
        [DisplayName("AppSecret")]
        public virtual string AppSecret { get; set; }

        [DisplayName("Url")]
        public virtual string Url { get; set; }

        [DisplayName("Token")]
        public virtual string Token { get; set; }
        [DisplayName("EncodingAESkey")]
        public virtual string EncodingAESkey { get; set; }
    }
}
