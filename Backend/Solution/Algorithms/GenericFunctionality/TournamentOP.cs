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
            return ref Select(ref population, population.Length / 2);
        }

        public override ref Chromosome Select(ref Chromosome[] population, int value)
        {
            int bestInd = Rnd.Next(0, population.Length);
            int newInd;
            // Getting solution to fight
            for (int i = 1; i < value; i++)
            {
                newInd = Rnd.Next(0, population.Length);
                if (population[newInd].PenaltyPoints < population[bestInd].PenaltyPoints)
                    bestInd = newInd;
            }
            return ref population[bestInd];
        }
    }
}
