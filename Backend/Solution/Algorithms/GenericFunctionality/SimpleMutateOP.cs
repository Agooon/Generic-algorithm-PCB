using Backend.UtilityClasses;
using System;

namespace Backend.Solution.Algorithms.GenericFunctionality
{
    public class SimpleMutateOP : MutateOperator
    {
        public override ref Chromosome Mutate(ref Chromosome solution, double Pm)
        {
            // Checking whether mutation will occur
            if (Rnd.NextDouble() > Pm)
                return ref solution;

            Path path = solution.Paths[new Random().Next(0, solution.Paths.Count)];

            int chosenSegmentInd = Rnd.Next(0, path.Segments.Count);
            Segment chosenSegment = new Segment()
            {
                Direction = path.Segments[chosenSegmentInd].Direction,
                Length = path.Segments[chosenSegmentInd].Length
            };
            Segment[] segmentsUntil = path.Segments.GetRange(0, chosenSegmentInd).ToArray();

            // To split the segment
            Point startSegmentP = new Point(path.StartingPoint).MoveToPoint(ref segmentsUntil);

            // Mutation will just fix the solution
            if (path.Segments[chosenSegmentInd].Length == 0)
            {
                path.FixPath();
                return ref solution;
            }

            int splitPointVal = Rnd.Next(1, chosenSegment.Length);
            Point chosenPoint = startSegmentP.Go(splitPointVal, chosenSegment.Direction);

            // Checking whether change will be orizontal or not
            if (chosenSegment.Direction == Globals.Left ||
                chosenSegment.Direction == Globals.Right)
            {
                // Moving Segment up
                if (Rnd.NextDouble() <= 0.5)
                {
                    if (chosenPoint.Y < solution.Problem.Height - 1)
                    {
                        SegmentUp(ref solution, ref path, ref chosenPoint, ref chosenSegment, ref chosenSegmentInd, ref splitPointVal);
                    }
                }
                // Moving Segment down
                // Checking whether it's possible
                else if (chosenPoint.Y > 0)
                {
                    SegmentDown(ref solution, ref path, ref chosenPoint, ref chosenSegment, ref chosenSegmentInd, ref splitPointVal);
                }
            }
            else
            {
                // Moving Segment Right
                if (Rnd.NextDouble() <= 0.5)
                {
                    // Checking whether it's possible
                    if (chosenPoint.X < solution.Problem.Width - 1)
                    {
                        SegmentRight(ref solution, ref path, ref chosenPoint, ref chosenSegment, ref chosenSegmentInd, ref splitPointVal);
                    }
                }
                // Moving Segment Left
                // Checking whether it's possible
                else if (chosenPoint.X > 0)
                {
                    SegmentLeft(ref solution, ref path, ref chosenPoint, ref chosenSegment, ref chosenSegmentInd, ref splitPointVal);
                }
            }

            path.FixPath();
            return ref solution;
        }

        private void SegmentUp(ref Chromosome solution, ref Path path, ref Point chosenPoint, ref Segment chosenSegment, ref int chosenSegmentInd, ref int splitPointVal)
        {
            // Checking whether it's possible

            int dirLength = Rnd.Next(1, solution.Problem.Height - chosenPoint.Y);
            // Choosing the left or right part of segment
            // Left part
            if (Rnd.Next(0, 2) == 1)
            {
                if (chosenSegment.Direction == Globals.Right)
                {
                    path.Segments[chosenSegmentInd].Length = chosenSegment.Length - splitPointVal;
                    path.Segments.Insert(chosenSegmentInd, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = dirLength
                    });
                }
                else
                {
                    path.Segments[chosenSegmentInd].Length = splitPointVal;
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = chosenSegment.Length - splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 3, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = dirLength
                    });
                }
            }
            // Right part
            else
            {
                if (chosenSegment.Direction == Globals.Right)
                {
                    path.Segments[chosenSegmentInd].Length = splitPointVal;
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = chosenSegment.Length - splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 3, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = dirLength
                    });
                }
                else
                {
                    path.Segments[chosenSegmentInd].Length = chosenSegment.Length - splitPointVal;
                    path.Segments.Insert(chosenSegmentInd, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = dirLength
                    });
                }
            }
        }

        private void SegmentDown(ref Chromosome solution, ref Path path, ref Point chosenPoint, ref Segment chosenSegment, ref int chosenSegmentInd, ref int splitPointVal)
        {
            int dirLength = Rnd.Next(1, chosenPoint.Y);
            // Choosing the left or right part of segment
            // Left part
            if (Rnd.Next(0, 2) == 1)
            {
                if (chosenSegment.Direction == Globals.Right)
                {
                    path.Segments[chosenSegmentInd].Length = chosenSegment.Length - splitPointVal;
                    path.Segments.Insert(chosenSegmentInd, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = dirLength
                    });
                }
                else
                {
                    path.Segments[chosenSegmentInd].Length = splitPointVal;
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = chosenSegment.Length - splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = dirLength
                    });
                }
            }
            // Right part
            else
            {
                if (chosenSegment.Direction == Globals.Right)
                {
                    path.Segments[chosenSegmentInd].Length = splitPointVal;
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = chosenSegment.Length - splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 3, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = dirLength
                    });
                }
                else
                {
                    path.Segments[chosenSegmentInd].Length = chosenSegment.Length - splitPointVal;
                    path.Segments.Insert(chosenSegmentInd, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = dirLength
                    });
                }
            }
        }

        private void SegmentRight(ref Chromosome solution, ref Path path, ref Point chosenPoint, ref Segment chosenSegment, ref int chosenSegmentInd, ref int splitPointVal)
        {
            int dirLength = Rnd.Next(1, solution.Problem.Width - chosenPoint.X);
            // Choosing the upper or lower part of segment
            // Upper
            if (Rnd.Next(0, 2) == 1)
            {
                // Right turn
                if (chosenSegment.Direction == Globals.Up)
                {
                    path.Segments[chosenSegmentInd].Length = splitPointVal;
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = chosenSegment.Length - splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 3, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = dirLength
                    });
                }
                else
                {
                    path.Segments[chosenSegmentInd].Length = chosenSegment.Length - splitPointVal;
                    path.Segments.Insert(chosenSegmentInd, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = dirLength
                    });
                }
            }
            // Lower
            else
            {
                // Right turn
                if (chosenSegment.Direction == Globals.Up)
                {
                    path.Segments[chosenSegmentInd].Length = chosenSegment.Length - splitPointVal;
                    path.Segments.Insert(chosenSegmentInd, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = dirLength
                    });
                }
                else
                {
                    path.Segments[chosenSegmentInd].Length = splitPointVal;
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = chosenSegment.Length - splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 3, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = dirLength
                    });
                }
            }
        }

        private void SegmentLeft(ref Chromosome solution, ref Path path, ref Point chosenPoint, ref Segment chosenSegment, ref int chosenSegmentInd, ref int splitPointVal)
        {
            int dirLength = Rnd.Next(1, chosenPoint.X);
            // Choosing the Upper or lower part of segment
            // Upper
            if (Rnd.Next(0, 2) == 1)
            {
                // Left turn
                if (chosenSegment.Direction == Globals.Up)
                {
                    path.Segments[chosenSegmentInd].Length = splitPointVal;
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = chosenSegment.Length - splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 3, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = dirLength
                    });
                }
                else
                {
                    path.Segments[chosenSegmentInd].Length = chosenSegment.Length - splitPointVal;
                    path.Segments.Insert(chosenSegmentInd, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = dirLength
                    });
                }
            }
            // Lower
            else
            {
                // Left turn
                if (chosenSegment.Direction == Globals.Up)
                {
                    path.Segments[chosenSegmentInd].Length = chosenSegment.Length - splitPointVal;
                    path.Segments.Insert(chosenSegmentInd, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Up,
                        Length = splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = dirLength
                    });
                }
                else
                {
                    path.Segments[chosenSegmentInd].Length = splitPointVal;
                    path.Segments.Insert(chosenSegmentInd + 1, new Segment()
                    {
                        Direction = Globals.Left,
                        Length = dirLength
                    });
                    path.Segments.Insert(chosenSegmentInd + 2, new Segment()
                    {
                        Direction = Globals.Down,
                        Length = chosenSegment.Length - splitPointVal
                    });
                    path.Segments.Insert(chosenSegmentInd + 3, new Segment()
                    {
                        Direction = Globals.Right,
                        Length = dirLength
                    });
                }
            }
        }
    }
}
