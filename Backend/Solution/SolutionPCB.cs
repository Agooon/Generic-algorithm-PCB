using Backend.Problem;
using System;

namespace Backend.Solution.Algorithms
{
    public abstract class SolutionPCB
    {
        public Random Rnd { get; set; }
        public SolutionPCB()
        {
            Rnd = new Random();
        }
        public SolutionPCB(int seed)
        {
            Rnd = new Random(seed);
        }
        public abstract Chromosome GetSolution(ProblemPCB problem);
        public abstract Chromosome GetSolution(ProblemPCB problem, int iterations);
    }
}
