using Backend.UtilityClasses;

namespace Backend.Solution
{
    public class Segment
    {
        // Up = 'U'
        // Down = 'D'
        // Right = 'R'
        // Left = 'L'
        public char Direction { get; set; }
        public int Length { get; set; }

        public bool OppositeDir(char direction)
        {
            if (Direction == Globals.Up && direction == Globals.Down)
                return true;
            if (Direction == Globals.Down && direction == Globals.Up)
                return true;
            if (Direction == Globals.Right && direction == Globals.Left)
                return true;
            if (Direction == Globals.Left && direction == Globals.Right)
                return true;
            return false;
        }
    }
}
