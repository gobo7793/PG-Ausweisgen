using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace PG_Ausweisgen
{
    [Serializable]
    class Settings
    {

        /// <summary>
        /// Settings instance
        /// </summary>
        public static Settings Instance { private set; get; }

        private const string _SettingsFile = "settings.json";

        /// <summary>
        /// Path to inkscape installation directory, default C:\Program Files\Inkscape
        /// </summary>
        public string InkscapePath { get; set; }= @"C:\Program Files\Inkscape";

        /// <summary>
        /// Input SVG file for front page
        /// </summary>
        public string InputFileFront { get; set; }

        /// <summary>
        /// Input SVG file for back page
        /// </summary>
        public string InputFileBack { get; set; }

        /// <summary>
        /// Output file formats, seperated by commata
        /// </summary>
        public string OutputFileFormats { get; set; }

        /// <summary>
        /// Saves the settings to <see cref="_SettingsFile"/>
        /// </summary>
        public static void Serialize()
        {
            var json = JsonSerializer.Serialize(Instance);
            File.WriteAllText(_SettingsFile, json);
        }

        /// <summary>
        /// Deserializes the settings from <see cref="_SettingsFile"/>
        /// </summary>
        public static void Deserialize()
        {
            var json = File.ReadAllText(_SettingsFile);
            Instance = JsonSerializer.Deserialize<Settings>(json);
        }
    }
}
