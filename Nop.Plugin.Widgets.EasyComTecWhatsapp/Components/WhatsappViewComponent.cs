using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.EasyComTecWhatsapp.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.EasyComTecWhatsapp.Components
{
    [ViewComponent(Name = "WhatsappViewComponent")]
    public class WhatsappViewComponent : NopViewComponent
    {

        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public WhatsappViewComponent(ISettingService settingService, IStoreContext storeContext)
        {
            _settingService = settingService;
            _storeContext = storeContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var settings = await _settingService.LoadSettingAsync<WhatsappSettings>(store.Id);

            var model = new PublicInfoModel
            {
                Phone = settings.Phone,
                Message = settings.Message
            };

            return View("~/Plugins/Widgets.EasyComTecWhatsapp/Views/PublicInfo.cshtml", model);
  

        }
    }
}
