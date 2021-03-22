using Backend.Solution;
using Backend.UtilityClasses;
using System;
using System.Collections.Generic;
using System.IO;

namespace Backend.Problem
{
    public class ProblemPCB
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public List<Tuple<Point, Point>> PointPairs { get; set; }

        public Chromosome ActualSolution { get; set; }

        public ProblemPCB(string filePath)
        {
            PointPairs = new List<Tuple<Point, Point>>();
            ReadData(filePath);
        }

        private void ReadData(string filePath)
        {
            using StreamReader sr = File.OpenText(filePath);
            string line;
            bool firstLine = true;

            while ((line = sr.ReadLine()) != null)
            {
                var lineVals = line.Split(';');
                if (firstLine)
                {
                    firstLine = false;

                    Width = int.Parse(lineVals[0]);
                    Height = int.Parse(lineVals[1]);
                }
                else
                {
                    PointPairs.Add(new Tuple<Point, Point>
                        (new Point(int.Parse(lineVals[0]), int.Parse(lineVals[1])),
                        new Point(int.Parse(lineVals[2]), int.Parse(lineVals[3]))
                        ));
                }
            }
        }
    }
}
