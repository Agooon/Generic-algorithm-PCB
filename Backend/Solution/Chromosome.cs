using Backend.Problem;
using Backend.UtilityClasses;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Solution
{
    public class Chromosome
    {
        
        public double PenaltyPoints { get; set; }
        // PW - Penalty weight, for evaluation function
        public double CrossSegmentPW { get; set; } = Globals.CrossSegmentPW;
        public double PathLengthPW { get; set; } = Globals.PathLengthPW;
        public double AmountOfSegmentsPW { get; set; } = Globals.AmountOfSegmentsPW;
        public double PathsOffBoardPW { get; set; } = Globals.PathsOffBoardPW;
        public double PathsLengthOffBoardPW { get; set; } = Globals.PathsLengthOffBoardPW;
        public int MaxNumberOfSegments { get; set; } = Globals.MaxNumberOfSegments;
        // To remember values for evaluation function
        public int CrossCount { get; set; }
        public int PathsLength { get; set; }
        public int AmountOfSegments { get; set; }
        public int PathsOffBoard { get; set; }
        public int PathsLengthOffBoard { get; set; }

        public List<Path> Paths { get; set; }
        public ProblemPCB Problem { get; set; }

        public Chromosome(ProblemPCB problem)
        {
            Problem = problem;
        }

        public Chromosome(Chromosome solution)
        {
            Paths = solution.Paths.ConvertAll(x => x.Clone());
            Problem = solution.Problem;
            PenaltyPoints = solution.PenaltyPoints;
        }
        // 1
        public int GetCrossCount()
        {
            int crossAmount = 0;
            List<Point> points = new List<Point>();
            foreach (Path path in Paths)
            {
                points.AddRange(path.RestartPoints());
            }

            crossAmount += points.GroupBy(x => new { x.X, x.Y }).Where(x => x.Count() > 1).Sum(_ => _.Count() - 1);

            return crossAmount;
        }
        // 2
        public int GetPathsLength()
        {
            int length = 0;
            foreach (Path path in Paths)
            {
                length += path.GetLength();
            }
            return length;
        }
        // 3
        public int GetAmountOfSegments()
        {
            int amountOfSegments = 0;
            foreach (Path path in Paths)
            {
                amountOfSegments += path.Segments.Count();
            }
            return amountOfSegments;
        }
        // 4
        public int GetPathsOffBoard()
        {
            int amountOfPaths = 0;
            foreach (Path path in Paths)
            {
                if (path.IsOffBoard(Problem.Width, Problem.Height))
                    amountOfPaths++;
            }
            return amountOfPaths;
        }
        // 5
        public int GetPathsLengthOffBoard()
        {
            int pathOffLength = 0;
            foreach (Path path in Paths)
            {
                pathOffLength += path.GetLengthOutside(Problem.Width, Problem.Height);
            }
            return pathOffLength;
        }

        public void SetPenaltyPoints()
        {
            CrossCount = GetCrossCount();
            PathsLength = GetPathsLength();
            AmountOfSegments = GetAmountOfSegments();
            PathsOffBoard = GetPathsOffBoard();
            PathsLengthOffBoard = GetPathsLengthOffBoard();

            PenaltyPoints = CrossCount * CrossSegmentPW +
                            PathsLength * PathLengthPW +
                            AmountOfSegments * AmountOfSegmentsPW +
                            PathsOffBoard * PathsOffBoardPW +
                            PathsLengthOffBoard * PathsLengthOffBoardPW;
        }

        public string GetSolutionInfo()
        {
            SetPenaltyPoints();
            string info = "";
            info += ("\nCrossCount:          " + CrossCount);
            info += ("\nPathLength:          " + PathsLength);
            info += ("\nAmountOfSegments:    " + AmountOfSegments);
            info += ("\nPathsOffBoard:       " + PathsOffBoard);
            info += ("\nPathsLengthOffBoard: " + PathsLengthOffBoard);
            info += ("\n\nPenaltyPoints:       " + PenaltyPoints);
            info += GetPathsString();

            return info;
        }

        public string GetPathsString()
        {
            string pathString = "";
            int ind = 1;
            foreach (Path path in Paths)
            {
                pathString += "\nPath nr "+ind++ +":\n" + path.GetStringPath();
            }
            return pathString;
        }
    }
}
