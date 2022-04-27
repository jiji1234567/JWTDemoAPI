using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtDemo.Common
{
    public class DemoAppsettings
    {
        static IConfiguration Configuration { get; set; }

        public DemoAppsettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public static string GetVal(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    string key = string.Join(":", sections);
                    return Configuration[key];
                }
            }
            catch (Exception) { }

            return string.Empty;
        } 
    }
}
