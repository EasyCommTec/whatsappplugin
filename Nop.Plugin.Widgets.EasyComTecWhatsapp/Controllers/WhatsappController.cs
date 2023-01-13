using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.EasyComTecWhatsapp.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.EasyComTecWhatsapp.Controllers
{
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WhatsappController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public WhatsappController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return Content("Access denied");

            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var settings = await _settingService.LoadSettingAsync<WhatsappSettings>(storeScope);
            var model = new ConfigurationModel
            {
                Phone = settings.Phone,
                Message = settings.Message

            };
            if (storeScope > 0)
            {
                model.Phone_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Phone, storeScope);
                model.Message_OverrideForStore = await _settingService.SettingExistsAsync(settings, x => x.Message, storeScope);
            }

            return View("~/Plugins/Widgets.EasyComTecWhatsapp/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return Content("Access denied");

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var settings = await _settingService.LoadSettingAsync<WhatsappSettings>(storeScope);


            settings.Phone = "55" + model.Phone;
            settings.Message = model.Message;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Phone, model.Phone_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(settings, x => x.Message, model.Message_OverrideForStore, storeScope, false);


            //now clear settings cache
            await _settingService.ClearCacheAsync();

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
            return await Configure();

        }

    }
}

