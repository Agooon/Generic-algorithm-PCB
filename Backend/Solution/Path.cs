using Backend.UtilityClasses;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Solution
{
    public class Path
    {
        public Point StartingPoint { get; set; }
        public List<Segment> Segments { get; set; } = new List<Segment>();

        public Point[] Points { get; set; } = null;


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
                if(Segments[i - 1].Direction == Segments[i].Direction)
                {
                    Segments[i-1].Length += Segments[i].Length;
                    Segments[i].Length = 0;
                }
            }
            for (int i = Segments.Count-1; i >=0 ; i--)
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
            Point currentPointB = null;

            foreach (Segment segment in Segments)
            {
                length = segment.Length;
                for (int i = 1; i <= segment.Length; i++)
                {
                    bool foundOD = false;
                    bool foundCP = false;
                    currentPoint = new Point(currentPoint).Go(1, segment.Direction);
                    if (currentPoints.Contains(currentPoint))
                    {
                        currentPointB = new Point(currentPoint).GoBack(i, segment.Direction);
                        for (int j = newSegments.Count - 1; j >= 0; j--)
                        {
                            int sgLen = (segment.Length);
                            // Going backwards to find the cross point
                            for (int z = 0; z < sgLen; z++)
                            {
                                currentPointB = new Point(currentPointB).GoBack(1, newSegments[j].Direction);
                                if (currentPoints.Contains(currentPointB))
                                {
                                    if (newSegments[j].OppositeDir(segment.Direction))
                                    {
                                        // When it's opposite direction we are gonna to subtract length until it reaches 0
                                        newSegments[j].Length--;
                                        length--;
                                        foundOD = true;
                                    }
                                    else
                                    {
                                        length -= i;
                                        foundCP = true;
                                        currentPointB = new Point(currentPointB).Go(1, newSegments[j].Direction);
                                        break;
                                    }
                                }
                            }
                            if (foundOD)
                            {
                                currentPoint = new Point(currentPointB);
                                if (newSegments[j].Length <= 0)
                                    newSegments.RemoveAt(j);
                                break;
                            }
                            if (foundCP)
                            {
                                break;
                            }
                            newSegments.RemoveAt(j);
                        }
                    }
                    if (foundCP)
                    {
                        // Go until we find the crossPoint
                        for (int j = newSegments.Count-1; j >=0 ; j--)
                        {
                            int sgLen = newSegments[j].Length;
                            bool found = false;
                            // Going backwards to find the cross point
                            for (int z = 0; z < sgLen; z++)
                            {
                                currentPointB = new Point(currentPointB).GoBack(1, newSegments[j].Direction);
                                newSegments[j].Length--;
                                if (currentPointB.Equals(currentPoint))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (newSegments[j].Length <= 0)
                                newSegments.RemoveAt(j);
                            else
                                currentPoint = new Point(currentPointB).Go(length, segment.Direction);
                            if (found)
                                break;
                        }
                    }
                    if (foundCP || foundOD)
                    {
                        currentPoints.Clear();

                        Path tempPath = new Path()
                        {
                            StartingPoint = StartingPoint,
                            Segments = newSegments
                        };
                        currentPoints.AddRange(tempPath.GetAllPoints());
                        break;
                    }
                    else
                    {   
                        currentPoints.Add(currentPoint);
                    }
                }

                // Adding new Segment, CrossPoint may happend at the end of segment
                // checking if path is needed or can be skipped
                if (length > 0)
                {
                    // Merging two segemnts with the same direction
                    if (newSegments.Count != 0 && newSegments.Last().Direction == segment.Direction)
                    {
                        newSegments[newSegments.Count - 1].Length += length;
                    }
                    else
                    {
                        newSegments.Add(new Segment()
                        {
                            Length = length,
                            Direction = segment.Direction
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
