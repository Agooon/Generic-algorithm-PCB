using Backend.Problem;
using Backend.Solution.Algorithms.GenericFunctionality;
using Backend.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Solution.Algorithms
{
    public class GenericSolutionPCB : SolutionPCB
    {
        public double Px { get; set; } = Globals.Px;
        public double Pm { get; set; } = Globals.Pm;
        public int AmountOfPopulation { get; set; } = Globals.AmountOfPopulation;
        public int TourmanteSize { get; set; } = Globals.TournamentSize;

        public CrossOperator CrossOperator { get; set; }
        public SelectionOperator SelectionOperator { get; set; }
        public MutateOperator MutateOperator { get; set; }

        public GenericSolutionPCB() : base()
        {
            SetDefaultOperators();
        }
        public GenericSolutionPCB(int seed) : base(seed)
        {
            SetDefaultOperators();
        }

        private void SetDefaultOperators()
        {
            CrossOperator = new SimpleCrossOP()
            {
                Rnd = Rnd
            };
            SelectionOperator = new TournamentOP()
            {
                Rnd = Rnd
            };
            MutateOperator = new SimpleMutateOP()
            {
                Rnd = Rnd
            };
        }

        public override Chromosome GetSolution(ProblemPCB problem)
        {
            return GetSolution(problem, Globals.Iteration);
        }

        public override Chromosome GetSolution(ProblemPCB problem, int iterations)
        {
            Chromosome[] currentPopulation = new Chromosome[AmountOfPopulation];
            RandomSolutionPCB randSol = new RandomSolutionPCB
            {
                Rnd = Rnd
            };

            // Initalization of population

            currentPopulation[0] = randSol.GetSolution(problem);
            currentPopulation[0].SetPenaltyPoints();
            Chromosome bestSolution = currentPopulation[0];
            for (int i = 1; i < AmountOfPopulation; i++)
            {
                currentPopulation[i] = randSol.GetSolution(problem);
                currentPopulation[i].SetPenaltyPoints();
                if (bestSolution.PenaltyPoints > currentPopulation[i].PenaltyPoints)
                    bestSolution = currentPopulation[i];
            }

            for (int i = 0; i < iterations; i++)
            {
                Chromosome[] nextPopulation = new Chromosome[AmountOfPopulation];
                for (int j = 0; j < AmountOfPopulation; j++)
                {
                    Chromosome parent1 = SelectionOperator.Select(ref currentPopulation, TourmanteSize);
                    Chromosome parent2 = SelectionOperator.Select(ref currentPopulation, TourmanteSize);

                    Chromosome child;
                    if (Rnd.NextDouble() < Px)
                        child = CrossOperator.GetChild(ref parent1, ref parent2);
                    else
                        child = new Chromosome(parent1);

                    MutateOperator.Mutate(ref child, Pm);
                    child.SetPenaltyPoints();
                    nextPopulation[j] = child;
                    if (bestSolution.PenaltyPoints > nextPopulation[j].PenaltyPoints)
                        bestSolution = new Chromosome(nextPopulation[j]);
                }
                currentPopulation = nextPopulation;
            }

            return bestSolution;
        }

        public List<Chromosome[]> GetSolutionData(ProblemPCB problem, int iterations)
        {
            List<Chromosome[]> populations = new List<Chromosome[]>();
            RandomSolutionPCB randSol = new RandomSolutionPCB
            {
                Rnd = Rnd
            };

            // Initalization of population
            populations.Add(new Chromosome[AmountOfPopulation]);

            populations[0][0] = randSol.GetSolution(problem);
            populations[0][0].SetPenaltyPoints();
            Chromosome bestSolution = populations[0][0];

            for (int i = 1; i < AmountOfPopulation; i++)
            {
                populations[0][i] = randSol.GetSolution(problem);
                populations[0][i].SetPenaltyPoints();
                if (bestSolution.PenaltyPoints > populations[0][i].PenaltyPoints)
                    bestSolution = populations[0][i];
            }

            int generation = 0;
            while (generation < iterations)
            {
                Chromosome[] currentPopulation = populations[generation];
                Chromosome[] nextPopulation = new Chromosome[AmountOfPopulation];
                for (int j = 0; j < AmountOfPopulation; j++)
                {
                    Chromosome parent1 = SelectionOperator.Select(ref currentPopulation, TourmanteSize);
                    Chromosome parent2 = SelectionOperator.Select(ref currentPopulation, TourmanteSize);

                    Chromosome child;
                    if (Rnd.NextDouble() < Px)
                        child = CrossOperator.GetChild(ref parent1, ref parent2);
                    else
                        child = new Chromosome(parent1);

                    MutateOperator.Mutate(ref child, Pm);
                    child.SetPenaltyPoints();
                    nextPopulation[j] = child;
                    if (bestSolution.PenaltyPoints > nextPopulation[j].PenaltyPoints)
                        bestSolution = nextPopulation[j];

                }
                populations.Add(nextPopulation);
                generation++;
            }

            return populations;
        }
    }
}
