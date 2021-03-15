using Backend.Solution;
using Backend.Solution.Algorithms.GenericFunctionality;
using Backend.UtilityClasses;
using System;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {

            //foreach (Chromosome solution in ChromosomeTesting.GetBasicSolutions())
            //{
            //    Console.WriteLine(solution.GetSolutionInfo());
            //    Console.WriteLine("-----------------------------");
            //}
            //ProblemPCB problem = new ProblemPCB(Globals.PathFile + "\\zad0.txt");
            //// Random Algorithm
            //Chromosome randSolution = new RandomSolutionPCB().GetSolution(problem,100);

            //randSolution.GetSolutionInfo();

            //problem = new ProblemPCB(Globals.PathFile + "\\zad1.txt");
            //// Random Algorithm
            //randSolution = new RandomSolutionPCB().GetSolution(problem, 100);

            //randSolution.GetSolutionInfo();
            Chromosome[] population = ChromosomeTesting.GetBasicSolutions().ToArray();
            foreach (Chromosome sol in population)
            {
                sol.SetPenaltyPoints();
                Console.WriteLine(sol.GetSolutionInfo());
            }
            //Chromosome tSelection = new TournamentOP(1).Select(ref population, 2);
            //Console.WriteLine("\n------------------------\n");
            //Console.WriteLine(tSelection.GetSolutionInfo());


            //tSelection = new TournamentOP(2).Select(ref population, 2);
            //Console.WriteLine("\n------------------------\n");
            //Console.WriteLine(tSelection.GetSolutionInfo());

            //tSelection = new TournamentOP(2).Select(ref population, 4);
            //Console.WriteLine("\n------------------------\n");
            //Console.WriteLine(tSelection.GetSolutionInfo());

            Console.WriteLine("\n------------------------\n");
            Console.WriteLine("\n------------------------\n");
            Chromosome tSelection = new RouletteOP(1).Select(ref population);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelection.GetSolutionInfo());
            Chromosome tSelection2 = new RouletteOP(2).Select(ref population);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelection2.GetSolutionInfo());
            Chromosome tSelection3 = new RouletteOP(3).Select(ref population);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelection3.GetSolutionInfo());
            Chromosome tSelection4 = new RouletteOP(4).Select(ref population);
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelection4.GetSolutionInfo());
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine("\n------------------------\n");

            Chromosome tSelectionChild13_1 = new SimpleCrossOP().GetChild(ref population[0], ref population[2]);
            Chromosome tSelectionChild11 = new SimpleCrossOP().GetChild(ref population[0], ref population[0]);
            Chromosome tSelectionChild13_2 = new SimpleCrossOP().GetChild(ref population[1], ref population[2]);
            Chromosome tSelectionChild14_1 = new SimpleCrossOP().GetChild(ref population[1], ref population[3]);
            Chromosome tSelectionChild14_2 = new SimpleCrossOP().GetChild(ref population[2], ref population[3]);

            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelectionChild11.GetSolutionInfo());
            
            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelectionChild13_1.GetSolutionInfo());

            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelectionChild13_2.GetSolutionInfo());

            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelectionChild14_1.GetSolutionInfo());

            Console.WriteLine("\n------------------------\n");
            Console.WriteLine(tSelectionChild14_2.GetSolutionInfo());

        }
    }
}
