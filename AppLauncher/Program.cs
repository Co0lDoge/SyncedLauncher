using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AppLauncher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] Content = File.ReadAllLines("AppLaunchConfig.txt");

            for (int i = 0; i < Content.Length; i++) {
                // Gets delay in seconds and sleeps before launch
                int Delay = Int32.Parse(Regex.Match(Content[i], @"\(([^)]*)\)").Groups[1].Value) * 1000;
                Thread.Sleep(Delay);

                // Starts process
                Process proc = new Process();
                proc.StartInfo.FileName = Regex.Match(Content[i], @"\{([^}]*)\}").Groups[1].Value;
                proc.Start();
            }    
        }
    }
}
