using Backend.Problem;
using Backend.Solution;
using Backend.Solution.Algorithms;
using Backend.Solution.Algorithms.GenericFunctionality;
using Backend.UtilityClasses;
using System;
using System.IO;
using System.Linq;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            ProblemPCB problem = new ProblemPCB(Globals.PathFile + "\\zad3.txt");

            RandomSolutionPCB randAlg = new RandomSolutionPCB()
            {
                Rnd = new Random(1)
            };

            GenericSolutionPCB genAlg = new GenericSolutionPCB()
            {
                AmountOfPopulation = 200,
                Pm = 0.3,
                Px = 0.9,
                TourmanteSize = 10,
                Rnd = new Random(1)
            };
            //genAlg.SelectionOperator = new RouletteOP()
            //{
            //    Rnd = genAlg.Rnd
            //};
            int algAmount = 10;
            int iterations = 150;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            FileStream fs = new FileStream("data.txt", FileMode.Create);
            using StreamWriter writeText = new StreamWriter(fs);
            //string toWrite = "";
            //double[][] finalScores = new double[iterations][];
            //double[] bestValues = new double[algAmount];

            //for (int i = 0; i < finalScores.Length; i++)
            //{
            //    finalScores[i] = new double[4];
            //    finalScores[i][0] = double.PositiveInfinity;
            //}
            //for (int i = 0; i < algAmount; i++)
            //{
            //    var result = genAlg.GetSolutionDataExcel(problem, iterations);


            //    toWrite += writeText.NewLine+ i + writeText.NewLine;
            //    int ind = 1;
            //    for (int z = 0; z < result.Item2.Length; z++)
            //    {
            //        toWrite += ind++ + writeText.NewLine;
            //        toWrite += "Best\tWorst\tAvg\tStd" + writeText.NewLine;
            //        for (int j = 0; j < result.Item2[z].Length; j++)
            //        {
            //            if (j == 0)
            //            {
            //                if (result.Item2[z][j] < finalScores[z][j])
            //                    finalScores[z][j] = result.Item2[z][j];
            //            }
            //            else if (j == 1)
            //            {
            //                if (result.Item2[z][j] > finalScores[z][j])
            //                    finalScores[z][j] = result.Item2[z][j];
            //            }
            //            else
            //            {
            //                finalScores[z][j] += result.Item2[z][j];
            //            }
            //            // Best
            //            // Worst
            //            // Avg
            //            // Std
            //            toWrite += result.Item2[z][j] + "\t";
            //        }
            //        toWrite += writeText.NewLine;
            //        bestValues[i] = result.Item1.PenaltyPoints;
            //    }
            //};
            //writeText.WriteLine(toWrite);


            //toWrite = "AVERAGES";

            //toWrite += writeText.NewLine + "Best\tWorst\tAvg\tStd" + writeText.NewLine;
            //for (int i = 0; i < finalScores.Length; i++)
            //{
            //    for (int j = 0; j < finalScores[0].Length; j++)
            //    {
            //        if (j != 0 && j != 1)
            //        {
            //            finalScores[i][j] = finalScores[i][j] / algAmount;
            //        }
            //        toWrite += finalScores[i][j] + "\t";
            //    }
            //    toWrite += writeText.NewLine;
            //}
            //writeText.WriteLine(toWrite);
            //toWrite = writeText.NewLine+"Best Values:"+ writeText.NewLine;

            //for (int i = 0; i < bestValues.Length; i++)
            //{
            //    toWrite += (i+1) + "\t";
            //}

            //toWrite += writeText.NewLine;

            //for (int i = 0; i < bestValues.Length; i++)
            //{
            //    toWrite += bestValues[i] + "\t";
            //}
            //writeText.WriteLine(toWrite);


            // Random
            int randIter = 60000;
            string toWrite2 = "Random" + writeText.NewLine;
            var result = randAlg.GetSolutionDataExcel(problem, randIter);
            for (int i = 0; i < result.Item2[0].Length; i++)
            {
                toWrite2 += result.Item2[0][i]+"\t";
            }
            writeText.WriteLine(toWrite2);
        }

        private static void Test1()
        {
            Chromosome[] population = ChromosomeTesting.GetBasicSolutions().ToArray();
            foreach (Chromosome sol in population)
            {
                Console.WriteLine("\n---Before--------\n");
                Console.WriteLine(sol.GetSolutionInfo());
                foreach (Backend.Solution.Path path in sol.Paths)
                {
                    path.FixPath();
                }
                Console.WriteLine("\n---After--------\n");
                Console.WriteLine(sol.GetSolutionInfo());
            }
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine("\n---------Torunament--------\n");
            Console.WriteLine("\n---Torunament 2---------\n");
            Chromosome tSelection = new TournamentOP(1).Select(ref population, 2);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelection.GetSolutionInfo());


            Console.WriteLine("\n---Torunament 2---------\n");
            tSelection = new TournamentOP(2).Select(ref population, 2);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelection.GetSolutionInfo());

            Console.WriteLine("\n---Torunament 2---------\n");
            tSelection = new TournamentOP(2).Select(ref population, 2);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelection.GetSolutionInfo());

            Console.WriteLine("\n---Torunament 4---------\n");
            tSelection = new TournamentOP(2).Select(ref population, 4);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelection.GetSolutionInfo());
            Console.WriteLine("\n---Torunament 4---------\n");
            tSelection = new TournamentOP(2).Select(ref population, 4);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelection.GetSolutionInfo());

            Console.WriteLine("\n------------------------\n");
            Console.WriteLine("\n---------Roulette--------\n");
            Chromosome rSelection = new RouletteOP(1).Select(ref population);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(rSelection.GetSolutionInfo());
            Chromosome rSelection2 = new RouletteOP(2).Select(ref population);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(rSelection2.GetSolutionInfo());
            Chromosome rSelection3 = new RouletteOP(55).Select(ref population);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(rSelection3.GetSolutionInfo());
            Chromosome rSelection4 = new RouletteOP(455).Select(ref population);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(rSelection4.GetSolutionInfo());
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine("\n-------Crossing---\n");

            Chromosome tSelectionChild11 = new SimpleCrossOP().GetChild(ref population[0], ref population[0]);
            Chromosome tSelectionChild13_1 = new SimpleCrossOP().GetChild(ref population[0], ref population[2]);
            Chromosome tSelectionChild13_2 = new SimpleCrossOP().GetChild(ref population[1], ref population[2]);
            Chromosome tSelectionChild14_1 = new SimpleCrossOP().GetChild(ref population[1], ref population[3]);
            Chromosome tSelectionChild14_2 = new SimpleCrossOP().GetChild(ref population[2], ref population[3]);

            Console.WriteLine("\n11------------------------\n");
            Console.WriteLine(tSelectionChild11.GetSolutionInfo());

            Console.WriteLine("\n31------------------------\n");
            Console.WriteLine(tSelectionChild13_1.GetSolutionInfo());

            Console.WriteLine("\n32------------------------\n");
            Console.WriteLine(tSelectionChild13_2.GetSolutionInfo());

            Console.WriteLine("\n41------------------------\n");
            Console.WriteLine(tSelectionChild14_1.GetSolutionInfo());

            Console.WriteLine("\n42------------------------\n");
            Console.WriteLine(tSelectionChild14_2.GetSolutionInfo());

            Console.WriteLine("\n------------------------\n");
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine("\n-------  Mutate --------\n");
            Console.WriteLine(population[0].GetSolutionInfo());
            Console.WriteLine("\n------------------------\n");

            for (int i = 0; i < 10; i++)
            {
                Chromosome mutated1 = new SimpleMutateOP().Mutate(ref population[0], 0.9);
                mutated1.SetPenaltyPoints();
                Console.WriteLine(mutated1.GetSolutionInfo());
                int ind = 1;
                foreach (var path in mutated1.Paths)
                {
                    Console.WriteLine("Path: " + ind++);
                    foreach (var segment in path.Segments)
                    {
                        Console.WriteLine("Direction: " + segment.Direction + " | " + segment.Length);
                    }
                }
            }
        }
    }
}
