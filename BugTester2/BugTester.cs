using System;
using System.IO;
using System.Diagnostics;
//using System.Threading;
using System.Threading.Tasks;
using MathNet.Numerics;
using SEMStandard;

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

            String ParamPath="D:/Documents/R/AgentBasedModel/Comparison/";
            String OutputPath="C:/Users/Karar/Desktop/Output";
            String[] ParamFiles = Utils.GetValidParams(ParamPath);

            //Prepare naming Scheme
            string Tag = Utils.GetTag(ParamFiles[0]);

            SimParams Par = new SimParams(localBreeding:true);
            WriteData Full = Simulations.Basic(Par);
            /* WriteData Temp;  
            WriteData Full = Simulations.Interval(Par, 200, false); 
            for(int j=1;j<2;j++){
                Temp = Simulations.Interval(Par, 200, false);
                Full.ConCat(Temp, Par);
            }*/
            Full.Output(Par,OutputPath,Tag, true);

            Console.WriteLine(Global.ElapsedMilliseconds);
            Console.WriteLine("All simulations completed.");
        }
    }
}

