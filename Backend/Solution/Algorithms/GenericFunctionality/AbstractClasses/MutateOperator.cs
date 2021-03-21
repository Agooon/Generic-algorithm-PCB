using Backend.UtilityClasses;
using System;

namespace Backend.Solution.Algorithms.GenericFunctionality
{
    public abstract class MutateOperator
    {
        public MutateOperator()
        {
            Rnd = new Random();
        }
        public MutateOperator(int seed)
        {
            Rnd = new Random(seed);
        }
        
        public Random Rnd { get; set; }
        public abstract ref Chromosome Mutate(ref Chromosome solution, double Pm);
    }
}
