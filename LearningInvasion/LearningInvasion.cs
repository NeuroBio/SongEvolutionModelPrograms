using System;
using System.Diagnostics;
using System.IO;

namespace LearningInvasion
{  
    class LearningInvasion
    {
        static void Main(String[] args){
            //Closed Runs
            InvasionSet("Closed", "1", "1", args[1], args[0]);
            InvasionSet("Closed", "1", "4", args[1], args[0]);
            InvasionSet("Closed", "2", "1", args[1], args[0]);
            InvasionSet("Closed", "2", "4", args[1], args[0]);

            //Delyaed Closed Runs
            InvasionSet("DelayedClosed", ".25", "1", args[1], args[0]);
            InvasionSet("DelayedClosed", ".25", "4", args[1], args[0]);
            InvasionSet("DelayedClosed", "2", "1", args[1], args[0]);
            InvasionSet("DelayedClosed", "2", "4", args[1], args[0]);

            //Open Runs
            InvasionSet("Open", ".25", "1", args[1], args[0]);
            InvasionSet("Open", ".25", "4", args[1], args[0]);
            InvasionSet("Open", "1", "1", args[1], args[0]);
            InvasionSet("Open", "1", "4", args[1], args[0]);
        }
        static void InvasionSet(string type, string invadeStat, string numInvade, string folderPath, string programPath){
            string FinalPath = string.Format(programPath+"/InvasionSims.dll");
            string OutputPath = string.Format(folderPath+"/"+type+"/"+type+"-"+invadeStat+"-"+numInvade);
            string SubArgs = string.Format("{0} {1} {2} {3} {4}", FinalPath, string.Format(folderPath+"/"+type),
                                                                    OutputPath, invadeStat, numInvade);
            Directory.CreateDirectory(OutputPath);

            //Set up process and run
            Process ClosedtoDelayed = new Process();
            ClosedtoDelayed.StartInfo.FileName = "dotnet";//FinalPath;
            ClosedtoDelayed.StartInfo.Arguments = SubArgs;
            ClosedtoDelayed.EnableRaisingEvents =true;
            ClosedtoDelayed.StartInfo.UseShellExecute=false;
            ClosedtoDelayed.StartInfo.RedirectStandardOutput=true;
            ClosedtoDelayed.StartInfo.WorkingDirectory=programPath;
            ClosedtoDelayed.Start();
            ClosedtoDelayed.WaitForExit();
        }
    }
}

