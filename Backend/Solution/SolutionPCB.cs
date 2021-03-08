using Backend.Problem;
using Backend.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Solution.Algorithms
{
    public abstract class SolutionPCB
    {
        public abstract Chromosome GetSolution(ProblemPCB problem);
        public abstract Chromosome GetSolution(ProblemPCB problem, int iterations);
    }
}
