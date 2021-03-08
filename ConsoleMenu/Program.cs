using Backend.Problem;
using Backend.Solution;
using Backend.Solution.Algorithms;
using Backend.UtilityClasses;
using ConsoleMenu.UtilityClasses;
using System;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach (Chromosome solution in ChromosomeTesting.GetBasicSolutions())
            {
                Console.WriteLine(solution.GetSolutionInfo());
                Console.WriteLine("-----------------------------");
            }
            ProblemPCB problem = new ProblemPCB(Globals.PathFile + "\\zad0.txt");
            // Random Algorithm
            Chromosome randSolution = new RandomSolutionPCB().GetSolution(problem,100);

            randSolution.GetSolutionInfo();

            problem = new ProblemPCB(Globals.PathFile + "\\zad1.txt");
            // Random Algorithm
            randSolution = new RandomSolutionPCB().GetSolution(problem, 100);

            randSolution.GetSolutionInfo();
            int x = 1;
        }
    }
}
