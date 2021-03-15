using System;

namespace Backend.Solution.Algorithms.GenericFunctionality
{
    public abstract class CrossOperator
    {
        public CrossOperator()
        {
            Rnd = new Random();
        }
        public CrossOperator(int seed)
        {
            Rnd = new Random(seed);
        }
        public Random Rnd { get; set; }
        public abstract Chromosome GetChild(ref Chromosome par1, ref Chromosome par2);
    }
}
