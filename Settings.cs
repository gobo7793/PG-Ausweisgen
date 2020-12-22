using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace PG_Ausweisgen
{
    [Serializable]
    class Settings : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Settings instance
        /// </summary>
        public static Settings Instance { private set; get; }

        private const string _SettingsFile = "settings.json";

        private string _InkscapePath = @"C:\Program Files\Inkscape";
        private string _InputFileFront;
        private string _InputFileBack;
        private string _OutputFileFormats;

        /// <summary>
        /// Path to inkscape installation directory, default C:\Program Files\Inkscape
        /// </summary>
        public string InkscapePath
        {
            get => _InkscapePath;
            set
            {
                _InkscapePath = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Input SVG file for front page
        /// </summary>
        public string InputFileFront
        {
            get => _InputFileFront;
            set
            {
                _InputFileFront = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Input SVG file for back page
        /// </summary>
        public string InputFileBack
        {
            get => _InputFileBack;
            set
            {
                _InputFileBack = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Output file formats, seperated by commata
        /// </summary>
        public string OutputFileFormats
        {
            get => _OutputFileFormats;
            set
            {
                _OutputFileFormats = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Saves the settings to <see cref="_SettingsFile"/>
        /// </summary>
        public static bool Serialize()
        {
            try
            {
                var json = JsonSerializer.Serialize(Instance);
                File.WriteAllText(_SettingsFile, json);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deserializes the settings from <see cref="_SettingsFile"/>
        /// </summary>
        public static bool Deserialize()
        {
            try
            {
                var json = File.ReadAllText(_SettingsFile);
                Instance = JsonSerializer.Deserialize<Settings>(json);
            }
            catch
            {
                if(Instance == null)
                    Instance = new Settings();
                return false;
            }
            return true;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
