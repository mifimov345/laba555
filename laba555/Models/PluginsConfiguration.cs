using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace laba555.Models
{
    public class PluginsConfiguration
    {
        private readonly IConfiguration _configuration;


        public PluginsConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Plugin> GetPlugins()
        {
            return _configuration.GetSection("Plugins").Get<List<Plugin>>();
        }

        public List<GrayPlugin> GetGrayPlugins()
        {
            return GetPlugins().OfType<GrayPlugin>().ToList();
        }

        public List<ContrastPlugin> GetContrastPlugins()
        {
            return GetPlugins().OfType<ContrastPlugin>().ToList();
        }
    }
}
