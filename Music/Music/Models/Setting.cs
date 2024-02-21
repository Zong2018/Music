using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
    public class Setting
    {
        private Setting() { }
        [JsonIgnore]
        public static Setting Instance { get { return Inner.instance; } }

        private class Inner
        {
            static Inner() {
                instance = null;
                string json = File.ReadAllText("SystemConfig.json");
                instance = string.IsNullOrWhiteSpace(json) ? new Setting() : JsonConvert.DeserializeObject<Setting>(json);
            }
            internal static readonly Setting instance;
        }

        public int Volume
        {
            get;set;
        }
       
        public string ThemeColor
        {
            get; set;
        }

        public string ThemeType
        {
            get; set;
        }

        public double ThemeColorSliderValue
        {
            get; set;
        }
        public double ThemeColorTraSliderValue
        {
            get; set;
        }

    }
}
