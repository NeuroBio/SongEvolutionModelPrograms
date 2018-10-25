using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using MathNet.Numerics;
using System.Text;
using System.Globalization;
using SongEvolutionModelLibrary;


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
            
            //string ParamPath="C:/Users/karar/Desktop/LearningInvasion/Closed";
            //string OutputPath="C:/Users/karar/Desktop/LearningInvasion/Closed/ClosedEnd";
            //float InvaderStat = .25f;
            string ParamPath = args[0];
            string OutputPath = args[1];
            float InvaderStat = float.Parse(args[2], CultureInfo.InvariantCulture);
            int NumInvaders = args.Length>3?
                            System.Convert.ToInt32(args[3]):1;
            int MaxParallel = args.Length>4?
                            System.Convert.ToInt32(args[4]):4;
            int Repeats = args.Length>5?
                            System.Convert.ToInt32(args[5]):50;
            int BurnIn = args.Length>6?
                            System.Convert.ToInt32(args[6]):500;
            
            //Get files and un sims in parallel
            ParallelOptions opt = new ParallelOptions();
            opt.MaxDegreeOfParallelism = MaxParallel;
            string[] ParamFiles = Utils.GetValidParams(ParamPath);
            Parallel.ForEach(ParamFiles, opt, (FileName) => Run(FileName, Repeats, InvaderStat,
                                                                    NumInvaders, BurnIn, OutputPath));

            Console.WriteLine(Global.ElapsedMilliseconds);
            Console.WriteLine("All simulations completed.");
        }

        static void Run(string fileName, int repeats, float invaderStat, int numInvaders, int burnIn, string outputPath){
            Stopwatch Local = new Stopwatch();
            Local.Start();
            Console.WriteLine(fileName);

            //Prepare data holders
            StringBuilder Output = new StringBuilder();
            Simulations.InvasionData Temp = new Simulations.InvasionData();

            //Get Parameters and run appropriate Simulation
            SimParams Par = new SimParams(reload:true, path: fileName);
            for(int j=0;j<repeats;j++){
                Temp = Simulations.InvasionLrnThrsh(Par, invaderStat, numInvaders, burnIn);
                Output.Append(Temp.Steps.ToString());
                Output.AppendLine(string.Format(",{0}",Temp.TraitAve));
            }

            //Save data
            string Tag = Utils.GetTag(fileName);
            File.WriteAllText(outputPath+"/"+Tag+"-"+invaderStat+".csv", Output.ToString());
            Console.WriteLine("{0}-{1}", fileName, Local.ElapsedMilliseconds);
        }
    }
}

