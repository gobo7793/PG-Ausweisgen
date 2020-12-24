using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PG_Ausweisgen
{
    class ScriptExecutor
    {
        private static FileInfo InkscapeExe => new FileInfo(String.Format(@"{0}\{1}",
            new DirectoryInfo(Settings.Instance.InkscapePath).FullName,
            Settings.Instance.InkscapeExe));

        /// <summary>
        /// Executes Inkscape to build the Member ID card
        /// </summary>
        /// <param name="firstName">First name of the Member</param>
        /// <param name="lastName">Last name of the Member</param>
        /// <param name="memberNo">Membership number of the Member</param>
        /// <param name="entryDate">Entry date of the Member</param>
        /// <returns>True if no Exceptions occured, otherwise the Exception</returns>
        public static bool ExecuteInkscape(string firstName, string lastName, string memberNo, string entryDate)
        {
            if(!InkscapeExe.Exists)
                throw new FileNotFoundException("Inkscape file not found: " + InkscapeExe.FullName);

            var frontTempFile = new FileInfo(String.Format(@"{0}\{1}", Environment.CurrentDirectory, "tempFront.svg"));
            BuildFrontPage(frontTempFile.FullName, firstName, lastName, memberNo, entryDate);

            var cmdOptions = BuildCmdLineOptions(firstName, lastName, memberNo, frontTempFile.FullName);

            var frontPorcess = Process.Start(InkscapeExe.FullName, cmdOptions.Item1);
            var backProcess = Process.Start(InkscapeExe.FullName, cmdOptions.Item2);

            frontPorcess.WaitForExit();
            backProcess.WaitForExit();

            if(frontTempFile.Exists)
                frontTempFile.Delete();

            return true;
        }

        /// <summary>
        /// Builds the front SVG and saves it to given file name
        /// </summary>
        private static void BuildFrontPage(string frontTempFile, string firstName, string lastName, string memberNo, string entryDate)
        {
            var svg = new StringBuilder(File.ReadAllText(Settings.Instance.InputFileFront));

            svg.Replace(Settings.Instance.FirstNameWildcard, firstName);
            svg.Replace(Settings.Instance.LastNameWildcard, lastName);
            svg.Replace(Settings.Instance.MemberNumberWildcard, memberNo);
            svg.Replace(Settings.Instance.EntryDateWildcard, entryDate);

            File.WriteAllText(frontTempFile, svg.ToString());
        }

        /// <summary>
        /// Builds the command line options to generate the final files from SVG
        /// </summary>
        /// <returns>The cmd line options for front and back page</returns>
        private static (string, string) BuildCmdLineOptions(string firstName, string lastName, string memberNo, string inputFileFront)
        {
            var outFileFront = String.Format(Settings.Instance.OutputFileName,
                memberNo, firstName, lastName, Settings.Instance.OutputFileNameFront);
            var outFileBack = String.Format(Settings.Instance.OutputFileName,
                memberNo, firstName, lastName, Settings.Instance.OutputFileNameBack);

            var outFullFileFront = String.Format(@"{0}\{1}", Settings.Instance.OutputDir, outFileFront);
            var outFullFileBack = String.Format(@"{0}\{1}", Settings.Instance.OutputDir, outFileBack);

            var cmdLineOptionsFront = String.Format(Settings.Instance.InkscapeOptions,
                Settings.Instance.OutputFileFormats, outFullFileFront, inputFileFront);
            var cmdLineOptionsBack = String.Format(Settings.Instance.InkscapeOptions,
                Settings.Instance.OutputFileFormats, outFullFileBack, Settings.Instance.InputFileBack);

            return (cmdLineOptionsFront, cmdLineOptionsBack);
        }
    }
}
