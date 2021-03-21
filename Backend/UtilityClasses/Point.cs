using Backend.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.UtilityClasses
{
    public class Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public Point MoveToPoint(ref Segment[] segments)
        {
            foreach (Segment segment in segments)
            {
                Go(segment.Length, segment.Direction);
            }
            return this;
        }
        public Point Go(int length, char direction)
        {
            if (direction == Globals.Up)
                Y += length;
            else if (direction == Globals.Down)
                Y -= length;
            else if (direction == Globals.Right)
                X += length;
            else if (direction == Globals.Left)
                X -= length;
            return this;
        }

        public Point GoBack(int length, char direction)
        {
            if (direction == Globals.Up)
                Y -= length;
            else if (direction == Globals.Down)
                Y += length;
            else if (direction == Globals.Right)
                X -= length;
            else if (direction == Globals.Left)
                X += length;
            return this;
        }

        public bool Equals(Point other)
        {
            if (other == null)
                return false;

            if (other.X == X && other.Y == Y)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Point point = obj as Point;
            if (point == null)
                return false;
            else
                return Equals(point);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(X, Y).GetHashCode();
        }
    }
}
