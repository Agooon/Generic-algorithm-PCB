using Backend.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Solution.Algorithms.GenericFunctionality
{
    public class TournamentOP : SelectionOperator
    {
        public override Chromosome Select(Chromosome[] population)
        {
            int bestInd = Rnd.Next(0, population.Length - 1);
            int newInd;
            // Getting solution to fight
            for (int i=1;i< Globals.DefaultTournamentSize; i++)
            {
                newInd = Rnd.Next(0, population.Length - 1);
                if (population[newInd].PenaltyPoints < population[bestInd].PenaltyPoints)
                    bestInd = newInd;
            }
            return population[bestInd];
        }
        public Chromosome Select(Chromosome[] population, int tournamentSize)
        {
            int bestInd = Rnd.Next(0, population.Length - 1);
            int newInd;
            // Getting solution to fight
            for (int i = 1; i < tournamentSize; i++)
            {
                newInd = Rnd.Next(0, population.Length - 1);
                if (population[newInd].PenaltyPoints < population[bestInd].PenaltyPoints)
                    bestInd = newInd;
            }
            return population[bestInd];
        }

        
    }
}
