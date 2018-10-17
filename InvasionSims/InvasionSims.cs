using System;
using System.IO;
using System.Diagnostics;
//using System.Threading;
using System.Threading.Tasks;
using MathNet.Numerics;
using SongEvolutionModelLibrary;
using System.Text;
using CsvHelper;
using System.Globalization;

namespace InvasionSimulation
{  
    class InvasionSims
    {
        static void Main(String[] args){
            Stopwatch Global = new Stopwatch();
            Global.Start();
            Console.WriteLine("Start");
            //do not check the distributions.
            Control.CheckDistributionParameters = false;

            String ParamPath="C:/Users/Karar/Desktop/PTest/";
            String OutputPath="C:/Users/karar/Desktop/PTest/Out/";
            float InvaderStat = .25f;
            //String ParamPath = args[0];
            //String OutputPath = args[1];
            //float InvaderStat = float.Parse(args[2], CultureInfo.InvariantCulture);
            int NumInvaders = args.Length>3?
                            System.Convert.ToInt32(args[3]):1;
            int MaxParallel = args.Length>4?
                            System.Convert.ToInt32(args[4]):4;
            int Repeats = args.Length>5?
                            System.Convert.ToInt32(args[5]):50;
            int BurnIn = args.Length>5?
                            System.Convert.ToInt32(args[5]):500;
            
            ParallelOptions opt = new ParallelOptions();
            opt.MaxDegreeOfParallelism = MaxParallel;
            

            String[] ParamFiles = Directory.GetFiles(ParamPath);
            
            //string i2 = ParamFiles[0];
            Parallel.ForEach(ParamFiles, opt, (FileName) => Run(FileName, Repeats, InvaderStat, NumInvaders, BurnIn, OutputPath));
            Console.WriteLine(Global.ElapsedMilliseconds);
            Console.WriteLine("All simulations completed.");
        }

        static void Run(String fileName, int repeats, float invaderStat, int numInvaders, int burnIn, String outputPath){
            Stopwatch Local = new Stopwatch();
            Local.Start();
            Console.WriteLine(fileName);

            //Prepare naming Scheme
            fileName.Replace("\\","/");
            String[] Tag = fileName.Split("/");
            Tag = Tag[Tag.Length-1].Split(".");
            StringBuilder Output = new StringBuilder();
            int[] Steps = new int[repeats];
            float[] TraitAve = new float[repeats];
            Simulations.InvasionData Temp = new Simulations.InvasionData();
            //Get Parameters and run appropriate Simulation
            SimParams Par = new SimParams(reload:true, path: fileName);
            for(int j=0;j<repeats;j++){
                Temp = Simulations.InvasionLrnThrsh(Par, invaderStat, numInvaders, burnIn);
                Output.Append(Temp.Steps.ToString());
                Output.AppendLine(string.Format(",{0}",Temp.TraitAve));
            }
            File.WriteAllText(outputPath+"/"+Tag+"-"+invaderStat+".csv", Output.ToString());
            Console.WriteLine("{0}-{1}", fileName, Local.ElapsedMilliseconds);
        }
    }
}

