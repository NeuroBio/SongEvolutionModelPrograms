using System;
using System.IO;
using System.Diagnostics;
//using System.Threading;
using System.Threading.Tasks;
using MathNet.Numerics;
using SongEvolutionModelLibrary;

namespace SongEvolutionModel
{  
    class IntervalSims
    {
        static void Main(String[] args){
            Stopwatch Global = new Stopwatch();
            Global.Start();
            Console.WriteLine("Start");
            //do not check the distributions.
            Control.CheckDistributionParameters = false;

            //String ParamPath="C:/Users/Karar/Desktop/OverviewSweep/";
            //String OutputPath="C:/Users/karar/Desktop/PTest/Out/";
            String ParamPath = args[0];
            String OutputPath = args[1];
            int MaxParallel = args.Length>2?
                            System.Convert.ToInt32(args[2]):4;
            int Frequency = args.Length>3?
                            System.Convert.ToInt32(args[3]):200;
            int Repeats = args.Length>4?
                            System.Convert.ToInt32(args[4]):50;
            
            ParallelOptions opt = new ParallelOptions();
            opt.MaxDegreeOfParallelism = MaxParallel;
            

            String[] ParamFiles = Directory.GetFiles(ParamPath);
            
            //string i2 = ParamFiles[0];
            Parallel.ForEach(ParamFiles, opt, (FileName) => Run(FileName, Repeats, Frequency, OutputPath));
            Console.WriteLine(Global.ElapsedMilliseconds);
            Console.WriteLine("All simulations completed.");
        }

        static void Run(String FileName, int Repeats, int Frequency, String OutputPath){
            Stopwatch Local = new Stopwatch();
            Local.Start();
            Console.WriteLine(FileName);

            //Prepare naming Scheme
            FileName.Replace("\\","/");
            String[] Tag = FileName.Split("/");
            Tag = Tag[Tag.Length-1].Split(".");

            //Get Parameters and run appropriate Simulation
            SimParams Par = new SimParams(reload:true, path: FileName);
            WriteData Temp;  
            WriteData Full = Simulations.Interval(Par, Frequency, false);
            for(int j=1;j<Repeats;j++){
                Temp = Simulations.Interval(Par, Frequency, false);
                Full.ConCat(Temp, Par);
            }
            Full.Output(Par,OutputPath,Tag[0], true);
            Console.WriteLine("{0}-{1}", FileName, Local.ElapsedMilliseconds);
        }
    }
}

