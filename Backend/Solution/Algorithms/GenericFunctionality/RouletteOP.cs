namespace Backend.Solution.Algorithms.GenericFunctionality
{
    public class RouletteOP : SelectionOperator
    {
        public RouletteOP() : base()
        {
        }
        public RouletteOP(int seed) : base(seed)
        {
        }

        public override ref Chromosome Select(ref Chromosome[] population)
        {
            double sumScore = 0;
            foreach (Chromosome sol in population)
                sumScore += 1 / sol.PenaltyPoints;

            double val = Rnd.NextDouble();

            double endPoint = (1 / population[0].PenaltyPoints) / sumScore;
            double startPoint = 0;

            if (startPoint <= val && val < endPoint)
                return ref population[0];

            for (int i = 1; i < population.Length; i++)
            {
                startPoint = endPoint;
                endPoint = startPoint + (1 / population[i].PenaltyPoints) / sumScore;
                if (startPoint <= val && val < endPoint)
                    return ref population[i];
            }
            return ref population[population.Length - 1];
        }
    }
}
