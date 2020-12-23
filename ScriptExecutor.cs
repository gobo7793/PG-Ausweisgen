using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PG_Ausweisgen
{
    class ScriptExecutor
    {
        private static FileInfo InkscapeExe => new FileInfo(new DirectoryInfo(Settings.Instance.InkscapePath).FullName
            + @"\" + Settings.Instance.InkscapeExe);

        /// <summary>
        /// Executes Inkscape to build the Member ID card
        /// </summary>
        /// <param name="firstName">First name of the Member</param>
        /// <param name="lastName">Last name of the Member</param>
        /// <param name="memberNo">Membership number of the Member</param>
        /// <param name="entryDate">Entry date of the Member</param>
        /// <returns>True if no Exceptions occured, otherwise the Exception</returns>
        public static bool ExecuteInkscape(string firstName, string lastName, string memberNo, DateTime entryDate)
        {
            if(!InkscapeExe.Exists)
                throw new FileNotFoundException("Inkscape file not found: " + InkscapeExe.FullName);



            return true;
        }
    }
}
