﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWinOverlay
{
    public class ConfigClass
    {
        public bool FirstRun;
        public bool Blur;
        public int Theme;
        public string Color;
        public List<JsonWindow> Windows { get; set; }
    }

    public class FileUpdateClass
    {
        public SoftFileData[] Data;
    }

    public class SoftFileData
    {
        public string Name;
        public int Size;
    }

    public class JsonWindow
    {
        public string Name;
        public WindowPosition Position;
    }

    public class WindowPosition
    {
        public double X;
        public double Y;
    }

    public class Config
    {
        public static ConfigClass GetConfig()
        {
            string json_out = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Tiels\\config.json");
            ConfigClass config = JsonConvert.DeserializeObject<ConfigClass>(json_out);
            return config;
        }
        public static bool SetConfig(ConfigClass config)
        {
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
                return false;
            }
        }
    }
}
