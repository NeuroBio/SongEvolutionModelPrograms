using System;
using System.Diagnostics;
using System.IO;

namespace LearningInvasion
{  
    class LearningInvasion
    {
        static void Main(String[] args){
            //Closed Runs
            //string[] args = new string[2];
            //args[1] = "C:/Users/karar/Desktop/InvasionParams/";
            //args[0] = "D:/Documents/C#/SongEvolutionModelPrograms/InvasionSims/bin/Release/netcoreapp2.1/";
            //InvasionSet("Closed", "1", "1", "C:/Users/karar/Desktop/LearningInvasion/Closed/", "D:/Documents/C#/SongEvolutionModelPrograms/InvasionSims/bin/Release/netcoreapp2.1/", 20, 10);
            InvasionSet("Closed", "1", "1", args[1], args[0], 20, 1000);
            InvasionSet("Closed", "1", "4", args[1], args[0], 20, 1000);
            InvasionSet("Closed", "2", "1", args[1], args[0], 20, 1000);
            InvasionSet("Closed", "2", "4", args[1], args[0], 20, 1000);

            //Delyaed Closed Runs
            InvasionSet("DelayedClosed", ".25", "1", args[1], args[0], 20, 1000);
            InvasionSet("DelayedClosed", ".25", "4", args[1], args[0], 20, 1000);
            InvasionSet("DelayedClosed", "2", "1", args[1], args[0], 20, 1000);
            InvasionSet("DelayedClosed", "2", "4", args[1], args[0], 20, 1000);

            //Open Runs
            InvasionSet("Open", ".25", "1", args[1], args[0], 20, 1000);
            InvasionSet("Open", ".25", "4", args[1], args[0], 20, 1000);
            InvasionSet("Open", "1", "1", args[1], args[0], 20, 1000);
            InvasionSet("Open", "1", "4", args[1], args[0], 20, 1000);
        }
        static void InvasionSet(string type, string invadeStat, string numInvade,
                                string paramsFolder, string programFolder, int maxPar, int repeats){
            string ProgramPath = Path.Combine(programFolder,"InvasionSims.dll");
            string ParamPath = Path.Combine(paramsFolder+type);
            string OutputPath = Path.Combine(ParamPath,string.Format(type+"-"+invadeStat+"-"+numInvade));
            Directory.CreateDirectory(OutputPath);
            string SubArgs = string.Format("{0} {1} {2} {3} {4} {5} {6}", ProgramPath, ParamPath,
                                                                    OutputPath, invadeStat, numInvade, maxPar, repeats);
         
            //Set up process and run
            Process ClosedtoDelayed = new Process();
            ClosedtoDelayed.StartInfo.FileName = "dotnet";//FinalPath;
            ClosedtoDelayed.StartInfo.Arguments = SubArgs;
            ClosedtoDelayed.EnableRaisingEvents =true;
            ClosedtoDelayed.StartInfo.UseShellExecute=false;
            ClosedtoDelayed.StartInfo.RedirectStandardOutput=false;
            ClosedtoDelayed.StartInfo.WorkingDirectory=programFolder;
            ClosedtoDelayed.Start();
            //Process.Start("dotnet", SubArgs);
            ClosedtoDelayed.WaitForExit();
        }
    }
}

