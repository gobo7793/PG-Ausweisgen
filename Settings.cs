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
        private string _InkscapeExe = @"bin\inkscape.com";
        private string _InkscapeOptions = "--export-type={0} -o \"{1}\" \"{2}\"";
        private string _InputFileFront;
        private string _InputFileBack;
        private string _OutputFileFormats = "png";
        private string _OutputFileName = "Mitgliedsausweis_{0}_{1}_{2}_{3}";
        private string _OutputFileNameFront = "vorne";
        private string _OutputFileNameBack = "hinten";
        private string _OutputDir;

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
        /// The Inkscape exe for command line, default bin\inkscape.com
        /// </summary>
        public string InkscapeExe
        {
            get => _InkscapeExe;
            set
            {
                _InkscapeExe = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The Inkscape command line options
        /// {0} = <see cref="OutputFileFormats"/>
        /// {1} = <see cref="OutputFileName"/>
        /// {2} = Input file name
        /// </summary>
        public string InkscapeOptions
        {
            get => _InkscapeOptions;
            set
            {
                _InkscapeOptions = value;
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
        /// Output file name, without file extension.
        /// {0} = Member Number
        /// {1} = First Name
        /// {2} = Last Name
        /// {3} = Front/Back Page
        /// </summary>
        public string OutputFileName
        {
            get => _OutputFileName;
            set
            {
                _OutputFileName = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Front page file name indicator
        /// </summary>
        public string OutputFileNameFront
        {
            get => _OutputFileNameFront;
            set
            {
                _OutputFileNameFront = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Back page file name indicator
        /// </summary>
        public string OutputFileNameBack
        {
            get => _OutputFileNameBack;
            set
            {
                _OutputFileNameBack = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Output directory
        /// </summary>
        public string OutputDir
        {
            get => _OutputDir;
            set
            {
                _OutputDir = value;
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
