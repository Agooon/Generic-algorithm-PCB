using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public abstract Chromosome Select(Chromosome[] population);
    }
}
