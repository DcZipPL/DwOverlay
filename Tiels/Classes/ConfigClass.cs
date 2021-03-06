﻿using Tiels.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiels
{
    public class ConfigClass
    {
        public string Version;
        public bool FirstRun;
        public bool Blur;
        public int Theme;
        public string Color;
        public bool HideAfterStart;
        public bool SpecialEffects;
        public List<JsonWindow> Windows { get; set; }
    }

    public class IconClass
    {
        public int Id;
        public string Icon;
    }

    public class JsonWindow
    {
        public int Id;
        public string Name;
        public int Height;
        public int Width;
        public bool Hidden;
        public bool EditBar;
        public WindowPosition Position;
    }

    public class WindowPosition
    {
        public double X;
        public double Y;
    }

    public class Config
    {
        private static string config_path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "Tiels";
        public static ConfigClass GetConfig()
        {
            try
            {
                string json_out = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Tiels\\config.json");
                if (json_out != "")
                {
                    ConfigClass config = JsonConvert.DeserializeObject<ConfigClass>(json_out);

                    if (config == null)
                        ErrorHandler.Error();

                    return config;
                }
                else
                {
                    ErrorHandler.Error();
                    return null;
                }
            }
            catch (FileNotFoundException fex)
            {
                ErrorHandler.Error();
                return null;
            }
            catch (Exception ex)
            {
                File.AppendAllText(config_path + "\\Error.log", "\r\n[Error: " + DateTime.Now + "] " + ex.ToString());
                ErrorWindow ew = new ErrorWindow();
                ew.ExceptionReason = ex;
                ew.ShowDialog();
                return null;
            }
        }
        public static bool SetConfig(ConfigClass config)
        {
            test++;
            ErrorHandler.Info(" Configuration saved: +"+test);
            try
            {
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Tiels"))
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Tiels\\config.json", json);
                else
                    return false;
                return true;
            }
            catch
            {
                return true;
            }
        }

        private static int test = 0;
    }
}
