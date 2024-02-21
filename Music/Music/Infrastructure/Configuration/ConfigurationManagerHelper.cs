using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Music.Infrastructure.Configuration
{
    public static class ConfigurationManagerHelper
    {
        public static readonly object ClockObject = new object();

        public static string Read(string key)
        {
            string value = string.Empty;
            try
            {
                string exeFullName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                var path = Path.GetDirectoryName(exeFullName);
                string jsonStr = System.IO.File.ReadAllText(path + "/SystemConfig.json", Encoding.UTF8);
                JObject jobj = JsonConvert.DeserializeObject<JObject>(jsonStr);
                value = jobj.SelectToken(key)?.ToString();
            }
            catch (Exception ex)
            {
                return value;
            }
            return value;
        }

        public static bool Write(string key, string value)
        {
            try
            {
                string exeFullName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                var path = Path.GetDirectoryName(exeFullName);
                string jsonStr = System.IO.File.ReadAllText(path + "/SystemConfig.json", Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(jsonStr)) return false;

                JObject jobj = JsonConvert.DeserializeObject<JObject>(jsonStr);
                var jtooken = jobj.SelectToken(key);
                if (jtooken == null ) return false;

                string text = jtooken?.Parent.ToString();
                string newJsonStr = jsonStr.Replace(text,$@"""{key}"":""{value}""");
                lock (ClockObject)
                {
                    using (FileStream stream = new FileStream(path + "/SystemConfig.json", FileMode.Create))
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.Write(newJsonStr);
                        }
                    }
                }
               
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
