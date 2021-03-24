using System.Collections.Generic;

namespace Backend.Solution.Algorithms.GenericFunctionality
{
    public class SimpleCrossOP : CrossOperator
    {
        public SimpleCrossOP() : base()
        {

        }
        public SimpleCrossOP(int seed) : base(seed)
        {
        }

        public override Chromosome GetChild(ref Chromosome par1, ref Chromosome par2)
        {
            Chromosome newChild = new Chromosome(par1.Problem);

            List<Path> childPaths = new List<Path>();
            for (int i = 0; i < par1.Paths.Count; i++)
            {
                if (Rnd.NextDouble() > 0.5)
                    childPaths.Add(par1.Paths[i].Clone());
                else
                    childPaths.Add(par2.Paths[i].Clone());
            }
            newChild.Paths = childPaths;

            return newChild;
        }
    }
}
