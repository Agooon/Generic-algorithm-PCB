namespace Backend.Solution.Algorithms.GenericFunctionality
{
    public class TournamentOP : SelectionOperator
    {
        public TournamentOP() : base()
        {
        }
        public TournamentOP(int seed) : base(seed)
        {
        }

        public override ref Chromosome Select(ref Chromosome[] population)
        {
            int bestInd = Rnd.Next(0, population.Length);
            int newInd;
            // Getting solution to fight
            for (int i=1;i< (population.Length/2) + 1; i++)
            {
                newInd = Rnd.Next(0, population.Length );
                if (population[newInd].PenaltyPoints < population[bestInd].PenaltyPoints)
                    bestInd = newInd;
            }
            return ref population[bestInd];
        }
        public ref Chromosome Select(ref Chromosome[] population, int tournamentSize)
        {
            int bestInd = Rnd.Next(0, population.Length);
            int newInd;
            // Getting solution to fight
            for (int i = 1; i < tournamentSize; i++)
            {
                newInd = Rnd.Next(0, population.Length);
                if (population[newInd].PenaltyPoints < population[bestInd].PenaltyPoints)
                    bestInd = newInd;
            }
            return ref population[bestInd];
        }

        
    }
}
