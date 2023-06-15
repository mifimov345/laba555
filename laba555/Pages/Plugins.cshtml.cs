using laba555.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace laba555.Pages
{
    public class PluginsModel : PageModel
    {
        private readonly PluginsConfiguration _pluginsConfiguration;

        public List<Plugin> Plugins { get; set; }

        public PluginsModel(PluginsConfiguration pluginsConfiguration)
        {
            _pluginsConfiguration = pluginsConfiguration;
        }

        public void OnGet()
        {
            Plugins = _pluginsConfiguration.GetPlugins();
        }
    }
}
