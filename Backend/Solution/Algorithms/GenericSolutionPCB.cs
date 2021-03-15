using Backend.Problem;
using System;

namespace Backend.Solution.Algorithms
{
    public class GenericSolutionPCB : SolutionPCB
    {
        public double Px { get; set; }
        public override Chromosome GetSolution(ProblemPCB problem)
        {
            //if (Rnd.NextDouble() > Px)
            //{
            //    if (Rnd.NextDouble() >= 0.5)
            //        newChild.Paths = par1.Paths;
            //    else
            //        newChild.Paths = par2.Paths;

            //    return newChild;
            //}
            throw new NotImplementedException();
        }

        public override Chromosome GetSolution(ProblemPCB problem, int iterations)
        {
            throw new NotImplementedException();
        }
    }
}
