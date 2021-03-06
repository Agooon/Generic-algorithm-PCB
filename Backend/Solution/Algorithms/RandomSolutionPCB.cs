using Backend.Problem;
using Backend.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Solution.Algorithms
{
    public class RandomSolutionPCB : SolutionPCB
    {
        public RandomSolutionPCB():base()
        {

        }
        public RandomSolutionPCB(int seed) : base(seed)
        {

        }
        public override Chromosome GetSolution(ProblemPCB problem)
        {
            Chromosome solution = new Chromosome(problem) { 
                Paths = new List<Path>()
            };

            foreach (Tuple<Point, Point> pointTuple in problem.PointPairs)
            {
                // In the beggining starting point is our last point
                Point lastPoint = pointTuple.Item1;

                // For each pair of points we are looking for a path
                Path newPath = new Path()
                {
                    StartingPoint = lastPoint,
                    Segments = new List<Segment>()
                };

                bool connected = false;
                int numberOfSegments = 0;
                char prevDirection = '-';
                while (!connected && numberOfSegments < Globals.MaxNumberOfSegments)
                {

                    Segment newSegment = new Segment();

                    // Random direction, filtering impossible options
                    List<char> availableDirections = new List<char>();
                    if (lastPoint.Y != problem.Height - 1 && prevDirection!=Globals.Down)
                        availableDirections.Add(Globals.Up);
                    if (lastPoint.Y != 0 && prevDirection != Globals.Up)
                        availableDirections.Add(Globals.Down);
                    if (lastPoint.X != problem.Width - 1 && prevDirection != Globals.Left)
                        availableDirections.Add(Globals.Right);
                    if (lastPoint.X != 0 && prevDirection != Globals.Right)
                        availableDirections.Add(Globals.Left);

                    char direction = availableDirections[Rnd.Next(0, availableDirections.Count - 1)];
                    
                    int length;
                    newSegment.Direction = direction;

                    // Random length
                    if (direction == Globals.Up)
                    {
                        length = Rnd.Next(1, problem.Height - lastPoint.Y);

                        // We have a length and direction, lets check if destination point is on its path
                        for (int i = 1; i <= length; i++)
                        {
                            if (pointTuple.Item2.X == lastPoint.X && pointTuple.Item2.Y == lastPoint.Y + i)
                            {
                                newSegment.Length = i;
                                connected = true;
                                break;
                            }
                        }
                        // If it is connected we no longer need lastPoint
                        if (!connected)
                            lastPoint = new Point(lastPoint.X, lastPoint.Y + length);

                    }
                    else if (direction == Globals.Down)
                    {
                        length = Rnd.Next(1, lastPoint.Y);

                        // We have a length and direction, lets check if destination point is on its path
                        for (int i = 1; i <= length; i++)
                        {
                            if (pointTuple.Item2.X == lastPoint.X && pointTuple.Item2.Y == lastPoint.Y - i)
                            {
                                newSegment.Length = i;
                                connected = true;
                                break;
                            }
                        }
                        // If it is connected we no longer need lastPoint
                        if (!connected)
                            lastPoint = new Point(lastPoint.X, lastPoint.Y - length);
                    }
                    else if (direction == Globals.Right)
                    {
                        length = Rnd.Next(1, problem.Width - lastPoint.X);

                        // We have a length and direction, lets check if destination point is on its path
                        for (int i = 1; i <= length; i++)
                        {
                            if (pointTuple.Item2.X == lastPoint.X + i && pointTuple.Item2.Y == lastPoint.Y)
                            {
                                newSegment.Length = i;
                                connected = true;
                                break;
                            }
                        }
                        // If it is connected we no longer need lastPoint
                        if (!connected)
                            lastPoint = new Point(lastPoint.X + length, lastPoint.Y);
                    }
                    else // Left
                    {
                        length = Rnd.Next(1, lastPoint.X);

                        // We have a length and direction, lets check if destination point is on its path
                        for (int i = 1; i <= length; i++)
                        {
                            if (pointTuple.Item2.X == lastPoint.X - i && pointTuple.Item2.Y == lastPoint.Y)
                            {
                                newSegment.Length = i;
                                connected = true;
                                break;
                            }
                        }
                        // If it is connected we no longer need lastPoint
                        if (!connected)
                            lastPoint = new Point(lastPoint.X - length, lastPoint.Y);
                    }

                    // We still didn't reach our goal
                    if (!connected)
                        newSegment.Length = length;

                    newPath.Segments.Add(newSegment);
                    numberOfSegments++;
                    prevDirection = direction;
                }

                if (!connected)
                {
                    // Forcing connection with 1 or 2 segments
                    if (lastPoint.X != pointTuple.Item2.X)
                    {
                        Segment repairSegment = new Segment();
                        // We go left
                        if (lastPoint.X > pointTuple.Item2.X)
                        {
                            repairSegment.Direction = Globals.Left;
                            repairSegment.Length = lastPoint.X - pointTuple.Item2.X;
                        }
                        else
                        {
                            repairSegment.Direction = Globals.Right;
                            repairSegment.Length = pointTuple.Item2.X - lastPoint.X;
                        }
                        newPath.Segments.Add(repairSegment);
                    }
                    if (lastPoint.Y != pointTuple.Item2.Y)
                    {
                        Segment repairSegment = new Segment();
                        // We go left
                        if (lastPoint.Y > pointTuple.Item2.Y)
                        {
                            repairSegment.Direction = Globals.Down;
                            repairSegment.Length = lastPoint.Y - pointTuple.Item2.Y;
                        }
                        else
                        {
                            repairSegment.Direction = Globals.Up;
                            repairSegment.Length = pointTuple.Item2.Y - lastPoint.Y;
                        }
                        newPath.Segments.Add(repairSegment);
                    }
                }


                // Adding new path
                solution.Paths.Add(newPath);
                var xd = newPath.Segments.ConvertAll(x => x.Clone()).ToArray();
                var checkEnd = new Point(pointTuple.Item1).MoveToPoint(ref xd);
                if (!checkEnd.Equals(pointTuple.Item2))
                {
                    int x = 1;
                }
            }
            
            return solution;
        }

        public override Chromosome GetSolution(ProblemPCB problem, int iterations)
        {
            // Random Algorithm
            Chromosome bestSolution = new RandomSolutionPCB().GetSolution(problem);
            bestSolution.SetPenaltyPoints();
            for (int i = 0; i < iterations; i++)
            {
                //Console.WriteLine("...RandomSolution starts...");
                Chromosome newSolution = new RandomSolutionPCB().GetSolution(problem);
                newSolution.SetPenaltyPoints();
                if (newSolution.PenaltyPoints < bestSolution.PenaltyPoints)
                {
                    bestSolution = newSolution;
                }
                Console.WriteLine(newSolution.GetSolutionInfo());
            }
            Console.WriteLine("\n\nBest Solution: \n" + bestSolution.GetSolutionInfo());

            return bestSolution;
        }

        public Tuple<Chromosome, double[][]> GetSolutionDataExcel(ProblemPCB problem, int iterations)
        {
            Chromosome bestSolution = null;
            double[][] scores = new double[iterations][];

            double[] savedScores = new double[iterations];

            double bestSolPop = double.PositiveInfinity;
            double worstSolPop = 0;
            double sumPen = 0;

            for (int i = 0; i < iterations; i++)
            {
                //Console.WriteLine("...RandomSolution starts...");
                Chromosome newSolution = new RandomSolutionPCB().GetSolution(problem);
                newSolution.SetPenaltyPoints();


                sumPen += newSolution.PenaltyPoints;
                if (bestSolPop > newSolution.PenaltyPoints)
                {
                    bestSolPop = newSolution.PenaltyPoints;
                    bestSolution = newSolution;
                }
                if (worstSolPop < newSolution.PenaltyPoints)
                {
                    worstSolPop = newSolution.PenaltyPoints;
                }
                savedScores[i] = newSolution.PenaltyPoints;
            }
            double avg = sumPen / iterations;
            double std = Math.Sqrt(savedScores.Select(x => Math.Pow((x - avg), 2)).Sum() / iterations);

            scores[0] = new double[4];
            scores[0][0] = bestSolPop;
            scores[0][1] = worstSolPop;
            scores[0][2] = avg;
            scores[0][3] = std;

            

            return new Tuple<Chromosome, double[][]>(bestSolution, scores);
        }
    }
}
