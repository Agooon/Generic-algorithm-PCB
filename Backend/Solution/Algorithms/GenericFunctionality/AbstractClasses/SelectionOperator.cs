using System;

namespace Backend.Solution.Algorithms.GenericFunctionality
{
    public abstract class SelectionOperator
    {
        public SelectionOperator()
        {
            Rnd = new Random();
        }
        public SelectionOperator(int seed)
        {
            Rnd = new Random(seed);
        }
        public Random Rnd { get; set; }
        public abstract ref Chromosome Select(ref Chromosome[] population);
    }
}
