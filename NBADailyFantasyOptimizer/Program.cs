using NBADailyFantasyOptimizer.DataAccess;
using NBADailyFantasyOptimizer.DataTransfer;
using NBADailyFantasyOptimizer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBADailyFantasyOptimizer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var service = new OptimizationService();
            var dao = new PlayerDao();


            //var str = "";
            //foreach (var q in Enumerable.Range(1, 5))
            //{


            //    var alldaysPlayers = Enumerable.Range(5, 13).SelectMany(s => dao.GetAllPlayers(s)).Where(x => x.Projection > q * 10 && x.Projection < q * 10 + 10).ToList();
            //    var pgs = alldaysPlayers.Where(s => s.Position == "PG").ToList();
            //    var sgs = alldaysPlayers.Where(s => s.Position == "SG").ToList();
            //    var sfs = alldaysPlayers.Where(s => s.Position == "SF").ToList();
            //    var pfs = alldaysPlayers.Where(s => s.Position == "PF").ToList();
            //    var cs = alldaysPlayers.Where(s => s.Position == "C").ToList();

            //    var pgsTot = Math.Round(pgs.Average(s => Math.Abs(s.Projection - s.ActualPoints)), 2);
            //    var sgsTot = Math.Round(sgs.Average(s => Math.Abs(s.Projection - s.ActualPoints)), 2);
            //    var sfsTot = Math.Round(sfs.Average(s => Math.Abs(s.Projection - s.ActualPoints)), 2);
            //    var pfsTot = pfs.Any() ? Math.Round(pfs.Average(s => Math.Abs(s.Projection - s.ActualPoints)), 2) : 0;
            //    var csTot = cs.Any() ? Math.Round(cs.Average(s => Math.Abs(s.Projection - s.ActualPoints)), 2) : 0;

            //    str += "" + q + ", " + pgsTot + ", " + sgsTot + ", " + sfsTot + ", " + pfsTot + ", " + csTot + ", " + alldaysPlayers.Count + "\n";
            //}
            //var dfs = 3;









            Dictionary<int, List<double>> allTotals = new Dictionary<int, List<double>>();

            var numRostersWanted = 13;
            var startDay = 100;
            var endDay = 100;
            var removalRoi = .75;
            double minuteCutoff = 22.0;
            int projDivision = 10;
            int projMin = 19;
            int playerMinMinutesPrevGame = 19;

            //foreach (var playerMinMinutesPrevGame in Enumerable.Range(10, 25).ToList())
            //{

                var allTopRosters = new List<RosterDto>();

                for (var i = startDay; i <= endDay; i++)
                {
                    Console.WriteLine();
                    Console.WriteLine(string.Format("Week {0}", i));
                    var weeksTopRosters = service.Optimize(i, numRostersWanted, removalRoi, minuteCutoff, projDivision, projMin, playerMinMinutesPrevGame, allTotals);
                    if (weeksTopRosters.Any())
                    {
                        Console.WriteLine();
                        Console.WriteLine(string.Format("Count 280+: {0}", weeksTopRosters.Select(s => s.TotalActual).Count(s => s >= 280)));
                        Console.WriteLine(string.Format("Average Total: {0}", weeksTopRosters.Select(s => s.TotalActual).Average()));
                        Console.WriteLine(string.Format("Highest Total: {0}", weeksTopRosters.Select(s => s.TotalActual).Max()));
                        allTopRosters.AddRange(weeksTopRosters);
                    }
                }
                if (allTopRosters.Any(s => s.TotalActual > 0))
                {
                    Console.WriteLine();
                    Console.WriteLine(string.Format("Final Totals: playerMinMinutesPrevGame {0}", playerMinMinutesPrevGame));
                    Console.WriteLine(string.Format("Count 280+: {0}", allTopRosters.Select(s => s.TotalActual).Count(s => s >= 280)));
                    Console.WriteLine(string.Format("Count 300+: {0}", allTopRosters.Select(s => s.TotalActual).Count(s => s >= 300)));
                    Console.WriteLine(string.Format("Average Total: {0}", allTopRosters.Select(s => s.TotalActual).Average()));
                    Console.WriteLine(string.Format("Highest Total: {0}", allTopRosters.Select(s => s.TotalActual).Max()));
                    //}            Console.WriteLine();
                    foreach (var iteration in allTotals)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Iteration " + iteration.Key + ": Count: " + iteration.Value.Count());
                        Console.WriteLine("Average: " + iteration.Value.Average());
                        Console.WriteLine("Count 280+: " + iteration.Value.Count(x => x >= 280));
                        Console.WriteLine("Count 300+: " + iteration.Value.Count(x => x >= 300));
                    }
                }
            //var rostsWith2Plus = allTopRosters.Where(s => s.Players.GroupBy(x => x.Team).Any(x => x.Count() >= 2));
            //var rostsWith3Plus = allTopRosters.Where(s => s.Players.GroupBy(x => x.Team).Any(x => x.Count() >= 3));
            //var rostsWith4Plus = allTopRosters.Where(s => s.Players.GroupBy(x => x.Team).Any(x => x.Count() >= 4));
            //var rostsWithMax1 = allTopRosters.Where(s => s.Players.GroupBy(x => x.Team).All(x => x.Count() <= 1));
            //var rostsWithMax2 = allTopRosters.Where(s => s.Players.GroupBy(x => x.Team).All(x => x.Count() <= 2));
            //var rostsWithMax3 = allTopRosters.Where(s => s.Players.GroupBy(x => x.Team).All(x => x.Count() <= 3));
            //var rostsWithMax4 = allTopRosters.Where(s => s.Players.GroupBy(x => x.Team).All(x => x.Count() <= 4));
            //var av2Plus = rostsWith2Plus.Average(s => s.TotalActual);
            //var av3Plus = rostsWith3Plus.Average(s => s.TotalActual);
            //var av4Plus = rostsWith4Plus.Average(s => s.TotalActual);
            //var av1orLess = rostsWith2Plus.Average(s => s.TotalActual);
            //var av2orLess = rostsWith3Plus.Average(s => s.TotalActual);
            //var av3orLess = rostsWith4Plus.Average(s => s.TotalActual);
            //var av4orLess = rostsWith4Plus.Average(s => s.TotalActual);
                Console.WriteLine("End.");
                Console.ReadLine();
        }
    }
}
