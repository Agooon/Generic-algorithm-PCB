using Backend.Problem;
using Backend.Solution;
using Backend.Solution.Algorithms;
using Backend.Solution.Algorithms.GenericFunctionality;
using Backend.UtilityClasses;
using System;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {

            Chromosome[] population = ChromosomeTesting.GetBasicSolutions().ToArray();
            foreach (Chromosome sol in population)
            {
                Console.WriteLine("\n---Before--------\n");
                Console.WriteLine(sol.GetSolutionInfo());
                foreach (Path path in sol.Paths)
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
            int x = 1;



            //var xd = ChromosomeTesting.GetBasicSolutions();
            //int index = 1;
            //Console.WriteLine(xd[0].GetSolutionInfo());
            //foreach (Path path in xd[0].Paths)
            //{
            //    Console.WriteLine("Path " + index + " Before:");
            //    foreach (var segment in path.Segments)
            //    {
            //        Console.WriteLine("Direction: " + segment.Direction + " | " + segment.Length);
            //    }
            //    path.FixPath();
            //    Console.WriteLine("Path " + index++ + " After:");
            //    foreach (var segment in path.Segments)
            //    {
            //        Console.WriteLine("Direction: " + segment.Direction + " | " + segment.Length);
            //    }
            //}
        }
    }
}
