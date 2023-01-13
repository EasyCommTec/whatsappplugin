using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.EasyComTecWhatsapp.Models
{
    /// <summary>
    /// Represents plugin configuration model
    /// </summary>
    public record ConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("Plugins.Widgets.Whatsapp.Phone")]
        public string Phone { get; set; }
        public bool Phone_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Whatsapp.Text")]
        public string Message { get; set; }
        public bool Message_OverrideForStore { get; set; }
        

    }
}