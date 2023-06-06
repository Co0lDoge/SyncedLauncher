using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AppLauncher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string path = "AppLaunchConfig.txt";
            if (!File.Exists(path))
                FailedExit("AppLaunchConfig.txt is not found.");

            // String containing all text from file
            string[] content = File.ReadAllLines(path);

            for (int i = 0; i < content.Length; i++) {

                // Get delay in seconds and sleeps before launch
                int delay = Int32.Parse(Regex.Match(content[i], @"\(([^)]*)\)").Groups[1].Value) * 1000;
                Thread.Sleep(delay);

                // Start process
                Process proc = new Process();
                proc.StartInfo.FileName = Regex.Match(content[i], @"\{([^}]*)\}").Groups[1].Value;
                StartProcess(proc, i);
            }
        }

        private static void StartProcess(Process proc, int shortcutNum)
        {
            try
            {
                proc.Start();
            }

            catch (Exception)
            {
                FailedExit($"Shortcut {shortcutNum} not found.");
            }
        }

        // Call if program failed to run properly
        private static void FailedExit(string error)
        {
            Console.WriteLine(error);
            Console.WriteLine("Press Any button to continue...");
            Console.ReadKey();
            Environment.Exit(-1);
        }
    }
}
