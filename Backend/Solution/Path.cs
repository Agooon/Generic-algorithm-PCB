using Backend.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Solution
{
    public class Path
    {
        public Point StartingPoint { get; set; }
        public List<Segment> Segments { get; set; } = new List<Segment>();

        public List<Point> Points { get; set; }


        public Point[] GetAllPoints()
        {
            if (Segments.Count == 0)
                return new Point[1] { StartingPoint };

            // +1 becouse of StartingPoint
            int numberOfPoints = GetLength() + 1;
            Point[] points = new Point[numberOfPoints];

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
    }
}
