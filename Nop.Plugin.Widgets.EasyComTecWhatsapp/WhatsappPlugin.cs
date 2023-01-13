using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.EasyComTecWhatsapp
{
    public class WhatsappPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;
        private readonly IWebHelper _webHelper;

        public WhatsappPlugin(IPermissionService permissionService, ILocalizationService localizationService, INotificationService notificationService, ISettingService settingService, IStoreContext storeContext, IStoreService storeService, IWorkContext workContext, IWebHelper webHelper)
        {
            _permissionService = permissionService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _settingService = settingService;
            _storeContext = storeContext;
            _storeService = storeService;
            _workContext = workContext;
            _webHelper = webHelper;
        }

        public bool HideInWidgetList => false;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WhatsappViewComponent";
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.HomepageBottom, "body_start_html_tag_after" });
        }
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/Whatsapp/Configure";
        }

        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.Whatsapp.Phone"] = "Telefone",
                ["Plugins.Widgets.Whatsapp.Text"] = "Mensagem",
                ["Plugins.Widgets.Whatsapp.Text.HelpPhone"] = "Digite o numero de telefone com DDD que será utilizado para receber e enviar mensagens via whatsapp.",
                ["Plugins.Widgets.Whatsapp.Text.HelpMessage"] = "Digite a primeira mensagem que será enviado pelo whastaspp.",

            });
            await base.InstallAsync();
        }
        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<WhatsappSettings>();
            //locales
            await _localizationService.DeleteLocaleResourcesAsync("Plugin.Widgets.EasyComTecWhatsapp");

            await base.UninstallAsync();
        }
    }
}
