using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Mobiversite
{
    public class Test : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //   EditPodFile(@"C:\Users\Mobiversite\Downloads");
            RunShell(@"C:\Users\Mobiversite\Downloads");
        }

        private void RunShell(string path)
        {            
            // Create a new Process object
            Process process = new Process();

            // Set the command and arguments for the process
            process.StartInfo.FileName = "pod";
            process.StartInfo.Arguments = "install";

            // Set the working directory for the process
            process.StartInfo.WorkingDirectory = path;


            process.StartInfo.UseShellExecute = false;

            // Start the process
            process.Start();


            // Wait for the process to exit
            process.WaitForExit();

        }

        void EditPodFile(string path)
        {
            const string wordToAdd = "use_frameworks!";
            string podFilePath = path + @"\Podfile";
            string content = File.ReadAllText(podFilePath);
            var res = content.Split("\n").ToList();



            res.Insert(5, wordToAdd);

            var fin = string.Join("\n", res);
            File.WriteAllText(podFilePath, fin);

        }
    }
}
