using Music.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Infrastructure.Manager
{
    public class SettingManager
    {
        public static Setting GlobalSetting { get { return Setting.Instance; } }

        public static async void SaveSetting()
        {
            await Task.Factory.StartNew(() => {
                try
                {
                    string json = JsonConvert.SerializeObject(GlobalSetting);
                    File.WriteAllText("SystemConfig.json", json);
                }
                catch (Exception ex)
                {

                }
            });
        }
    }
}
