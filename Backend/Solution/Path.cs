using Backend.UtilityClasses;
using System.Collections.Generic;

namespace Backend.Solution
{
    public class Path
    {
        public Point StartingPoint { get; set; }
        public List<Segment> Segments { get; set; } = new List<Segment>();

        public Point[] Points { get; set; } = null;


        public Point[] GetAllPoints()
        {
            if (Points != null)
                return Points;
            Point[] points = null;
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
                if (point.X < 0 || point.Y < 0 || point.X > width-1 || point.Y > height-1)
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
                    if (newPoint.X < 0 || newPoint.Y < 0 || newPoint.X > width-1 || newPoint.Y > height-1)
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
    }

}
