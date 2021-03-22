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
            //Chromosome[] population = ChromosomeTesting.GetBasicSolutions().ToArray();
            //population[0].Paths[0].FixPath();
            ProblemPCB problem = new ProblemPCB(Globals.PathFile + "\\zad1.txt");

            GenericSolutionPCB genAlg = new GenericSolutionPCB()
            {
                AmountOfPopulation = 100,
                Pm = 0.2,
                Px = 0.5,
                TourmanteSize = 5
            };
            var populations = genAlg.GetSolutionData(problem, 4000);
            Console.WriteLine("\n------------------\n");


            int ind = 1;
            int bestGenSolInd = 1;
            Chromosome bestSolOfAll = populations[0][0];
            foreach (Chromosome[] population in populations)
            {
                double sum = 0;
                Chromosome bestSol = population[0];
                foreach (Chromosome solution in population)
                {
                    sum += solution.PenaltyPoints;
                    if (bestSol.PenaltyPoints > solution.PenaltyPoints)
                        bestSol = solution;
                }
                
                Console.WriteLine("\n------------------");
                Console.WriteLine("Generation number " + ind++);
                Console.WriteLine("Avg penalty points (lower the better): " + sum / population.Length);
                Console.WriteLine("Best solution in generation: " + bestSol.GetSolutionInfo());
                Console.WriteLine("\n------------------");

                if (bestSolOfAll.PenaltyPoints > bestSol.PenaltyPoints)
                {
                    bestSolOfAll = bestSol;
                    bestGenSolInd = ind - 1;
                }
            }
            Console.WriteLine("\n------------------");
            Console.WriteLine("Generation number of best solution" + bestGenSolInd);
            Console.WriteLine("Best solution in generations: " + bestSolOfAll.GetSolutionInfo());



            var soloSolution = genAlg.GetSolution(problem, 10000);
            Console.WriteLine("\n------------------");
            Console.WriteLine("Generation number of best solution" + soloSolution);
            Console.WriteLine("Best solution in generations: " + soloSolution.GetSolutionInfo());
        }

        private static void Test1()
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
        }
    }
}
