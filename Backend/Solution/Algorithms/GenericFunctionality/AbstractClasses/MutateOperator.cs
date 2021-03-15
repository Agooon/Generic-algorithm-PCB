using System;

namespace Backend.Solution.Algorithms.GenericFunctionality
{
    public abstract class MutateOperator
    {
        public Random Rnd { get; set; }
        public abstract ref Chromosome Mutate(ref Chromosome solution);
    }
}
