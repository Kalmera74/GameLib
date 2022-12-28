using UnityEditor;
using UnityEditor.Callbacks;
using System.Linq;
using System.Diagnostics;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;
namespace GameLib.Utility
{

    public class IosPostBuildOperations
    {
        // Set the IDFA request description:
        const string k_TrackingDescription = "Your data will be used to provide you a better and personalized ad experience.";

        [PostProcessBuild(0)]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode)
        {
#if ATT_ACTIVE
        if (buildTarget == BuildTarget.iOS)
        {
            AddPListValues(pathToXcode);
        }
#endif
        }

        // Implement a function to read and write values to the plist file:
        static void AddPListValues(string pathToXcode)
        {
#if UNITY_IOS
            // Retrieve the plist file from the Xcode project directory:
            string plistPath = pathToXcode + "/Info.plist";
            PlistDocument plistObj = new PlistDocument();


            // Read the values from the plist file:
            plistObj.ReadFromString(File.ReadAllText(plistPath));

            // Set values from the root object:
            PlistElementDict plistRoot = plistObj.root;

            // Set the description key-value in the plist:
            plistRoot.SetString("NSUserTrackingUsageDescription", k_TrackingDescription);

            // Save changes to the plist:
            File.WriteAllText(plistPath, plistObj.WriteToString());

            EditPodFile(pathToXcode);
#endif
        }

        static void EditPodFile(string path)
        {
            const string wordToAdd = "use_frameworks!";
            string podFilePath = path + @"\Podfile";

            string content = File.ReadAllText(podFilePath);

            var strList = content.Split("\n").ToList();
            strList.Insert(5, wordToAdd);

            var res = string.Join("\n", strList);

            File.WriteAllText(podFilePath, res);

            InstallPods(path);

        }

        static void InstallPods(string path)
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
    }
}