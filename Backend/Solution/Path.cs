using Backend.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Solution
{
    public class Path
    {
        public Point StartingPoint { get; set; }
        public List<Segment> Segments { get; set; } = new List<Segment>();

        public Point[] Points { get; set; } = null;

        public Path Clone()
        {
            return new Path
            {
                StartingPoint = new Point(StartingPoint),
                Segments = Segments.ConvertAll(x => x.Clone()),
                Points = Points.Select(x => new Point(x)).ToArray()
            };
        }

        public string GetStringPath()
        {
            string pathString = "";
            int ind = 1;
            foreach (Segment segment in Segments)
            {
                pathString += ind++ + ". " + segment.Direction + ": " + segment.Length + "\n";
            }
            return pathString;
        }

        public Point[] RestartPoints()
        {
            Point[] points;
            if (Segments.Count == 0)
            {
                points = new Point[1] { StartingPoint };
                Points = points;
                return points;
            }

            // +1 becouse of StartingPoint
            int numberOfPoints = GetLength() + 1;
            points = new Point[numberOfPoints];

            int index = 0;
            points[index++] = StartingPoint;

            foreach (Segment segment in Segments)
            {
                for (int j = 0; j < segment.Length; j++)
                {
                    Point newPoint;
                    if (segment.Direction == Globals.Up)
                    {
                        newPoint = new Point(points[index - 1].X, points[index - 1].Y + 1);
                    }
                    else if (segment.Direction == Globals.Down)
                    {
                        newPoint = new Point(points[index - 1].X, points[index - 1].Y - 1);
                    }
                    else if (segment.Direction == Globals.Right)
                    {
                        newPoint = new Point(points[index - 1].X + 1, points[index - 1].Y);
                    }
                    else
                    {
                        newPoint = new Point(points[index - 1].X - 1, points[index - 1].Y);
                    }
                    points[index++] = newPoint;
                }
            }

            Points = points;
            return points;
        }

        public Point[] GetAllPoints()
        {
            if (Points != null)
                return Points;
            Point[] points;
            if (Segments.Count == 0)
            {
                points = new Point[1] { StartingPoint };
                Points = points;
                return points;
            }

            // +1 becouse of StartingPoint
            int numberOfPoints = GetLength() + 1;
            points = new Point[numberOfPoints];

            int index = 0;
            points[index++] = StartingPoint;

            foreach (Segment segment in Segments)
            {
                for (int j = 0; j < segment.Length; j++)
                {
                    Point newPoint;
                    if (segment.Direction == Globals.Up)
                    {
                        newPoint = new Point(points[index - 1].X, points[index - 1].Y + 1);
                    }
                    else if (segment.Direction == Globals.Down)
                    {
                        newPoint = new Point(points[index - 1].X, points[index - 1].Y - 1);
                    }
                    else if (segment.Direction == Globals.Right)
                    {
                        newPoint = new Point(points[index - 1].X + 1, points[index - 1].Y);
                    }
                    else
                    {
                        newPoint = new Point(points[index - 1].X - 1, points[index - 1].Y);
                    }
                    points[index++] = newPoint;
                }
            }

            Points = points;
            return points;
        }
        public int GetLength()
        {
            int length = 0;
            foreach (Segment segment in Segments)
            {
                length += segment.Length;
            }
            return length;
        }
        public bool IsOffBoard(int width, int height)
        {
            List<Point> points = new List<Point>();
            points.AddRange(GetAllPoints());
            foreach (Point point in points)
            {
                if (point.X < 0 || point.Y < 0 || point.X > width - 1 || point.Y > height - 1)
                {
                    return true;
                }
            }
            return false;
        }
        public int GetLengthOutside(int width, int height)
        {
            int lengthOffBoard = 0;
            int numberOfPoints = GetLength() + 1;
            Point[] points = new Point[numberOfPoints];

            int index = 0;
            points[index++] = StartingPoint;

            bool inBoard = true;
            foreach (Segment segment in Segments)
            {
                for (int j = 0; j < segment.Length; j++)
                {
                    Point newPoint;
                    if (segment.Direction == Globals.Up)
                    {
                        newPoint = new Point(points[index - 1].X, points[index - 1].Y + 1);
                    }
                    else if (segment.Direction == Globals.Down)
                    {
                        newPoint = new Point(points[index - 1].X, points[index - 1].Y - 1);
                    }
                    else if (segment.Direction == Globals.Right)
                    {
                        newPoint = new Point(points[index - 1].X + 1, points[index - 1].Y);
                    }
                    else
                    {
                        newPoint = new Point(points[index - 1].X - 1, points[index - 1].Y);
                    }
                    points[index++] = newPoint;
                    if (newPoint.X < 0 || newPoint.Y < 0 || newPoint.X > width - 1 || newPoint.Y > height - 1)
                    {
                        lengthOffBoard++;
                        inBoard = false;
                    }
                    else if (!inBoard)
                    {
                        // We need to add point to length for path to comeback to board
                        lengthOffBoard++;
                        inBoard = true;
                    }
                }
            }
            return lengthOffBoard;
        }

        public void FixPath()
        {
            if (Segments.Count == 0)
                return;
            int crossAmount = 0;
            List<Point> points = new List<Point>();
            points.AddRange(RestartPoints());
            crossAmount += points.GroupBy(x => new { x.X, x.Y }).Where(x => x.Count() > 1).Sum(_ => _.Count() - 1);

            // Deleting non usefull segments
            for (int i = 1; i < Segments.Count; i++)
            {
                if (Segments[i - 1].Direction == Segments[i].Direction)
                {
                    Segments[i - 1].Length += Segments[i].Length;
                    Segments[i].Length = 0;
                }
            }
            for (int i = Segments.Count - 1; i >= 0; i--)
            {
                if (Segments[i].Length <= 0)
                    Segments.RemoveAt(i);
            }

            if (crossAmount == 0)
                return;

            List<Segment> newSegments = new List<Segment>();
            List<Point> currentPoints = new List<Point>() {
            StartingPoint
            };
            int length;
            Point currentPoint = new Point(StartingPoint);

            for (int segInd = 0; segInd < Segments.Count; segInd++)
            {
                length = Segments[segInd].Length;
                bool found = false;
                for (int i = 1; i <= Segments[segInd].Length; i++)
                {
                    currentPoint = new Point(currentPoint).Go(1, Segments[segInd].Direction);
                    if (currentPoints.Contains(currentPoint))
                    {
                        found = true;
                        length -= i;
                        if (currentPoint.Equals(StartingPoint))
                        {
                            newSegments.Clear();
                        }
                        else
                        {
                            // go until met, leave segments in path
                            Point currentPointFrom = new Point(StartingPoint);
                            for (int j = 0; j < newSegments.Count; j++)
                            {
                                bool foundAtWay = false;
                                for (int z = 1; z <= newSegments[j].Length; z++)
                                {
                                    currentPointFrom = currentPointFrom.Go(1, newSegments[j].Direction);
                                    if (currentPointFrom.Equals(currentPoint))
                                    {
                                        newSegments.RemoveRange(j + 1, newSegments.Count - j - 1);
                                        newSegments[j].Length = z;
                                        if (newSegments[j].Length == 0)
                                        {
                                            newSegments.RemoveAt(j);
                                        }
                                        foundAtWay = true;
                                        break;
                                    }
                                }
                                if (foundAtWay)
                                    break;
                            }
                        }
                    }
                    if (!found)
                        currentPoints.Add(currentPoint);
                    else
                        break;

                }

                if (found)
                {
                    if (length != 0)
                    {
                        Segments[segInd].Length = length;
                        segInd--;
                    }
                    Path tempPath = new Path()
                    {
                        Segments = newSegments,
                        StartingPoint = StartingPoint
                    };
                    currentPoints.Clear();
                    currentPoints.AddRange(tempPath.GetAllPoints());

                }
                // Adding new Segment, CrossPoint may happend at the end of segment
                // checking if path is needed or can be skipped
                else if (length > 0)
                {
                    // Merging two segemnts with the same direction
                    if (newSegments.Count != 0 && newSegments.Last().Direction == Segments[segInd].Direction)
                    {
                        newSegments[newSegments.Count - 1].Length += length;
                    }
                    else
                    {
                        newSegments.Add(new Segment()
                        {
                            Length = length,
                            Direction = Segments[segInd].Direction
                        });
                    }
                }


            }

            Segments = newSegments;

            // Restarting the points
            points.Clear();
            points.AddRange(RestartPoints());
            crossAmount += points.GroupBy(x => new { x.X, x.Y }).Where(x => x.Count() > 1).Sum(_ => _.Count() - 1);
        }

    }
}
