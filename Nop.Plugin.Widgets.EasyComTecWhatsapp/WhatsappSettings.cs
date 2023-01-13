using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.EasyComTecWhatsapp
{
    public class WhatsappSettings : ISettings
    {
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
