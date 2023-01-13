using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.EasyComTecWhatsapp.Models
{
    public record PublicInfoModel: BaseNopModel
    {
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
