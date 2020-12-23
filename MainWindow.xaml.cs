using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PG_Ausweisgen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if(!Settings.Deserialize())
                MessageBox.Show("Fehler beim Laden der Einstellungen. Bitte Pfade erneut eingeben!",
                    "Fehler beim Laden der Einstellungen", MessageBoxButton.OK, MessageBoxImage.Information);

            DataContext = Settings.Instance;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!Settings.Serialize())
            {
                MessageBox.Show("Die Einstellungen konnten nicht gespeichert werden und müssen beim nächsten Start erneut eingegeben werden.",
                    "Fehler beim Speichern der Einstellungen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtInkscape_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                DefaultDirectory = Settings.Instance.InkscapePath
            };
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
                Settings.Instance.InkscapePath = dialog.FileName;
        }

        private void BtFront_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog(){
                Filter = "SVG-Dateien (*.svg)|*.svg"
            };
            if(dialog.ShowDialog() == true)
                Settings.Instance.InputFileFront = dialog.FileName;
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "SVG-Dateien (*.svg)|*.svg"
            };
            if(dialog.ShowDialog() == true)
                Settings.Instance.InputFileBack = dialog.FileName;
        }

        private void BtOutDir_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                DefaultDirectory = Settings.Instance.InkscapePath
            };
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
                Settings.Instance.OutputDir = dialog.FileName;
        }

        private void BtCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScriptExecutor.ExecuteInkscape(tbFirstName.Text, tbLastName.Text, tbMemberNo.Text, tbEntryDate.DisplayDate);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler bei der Ausführung", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
