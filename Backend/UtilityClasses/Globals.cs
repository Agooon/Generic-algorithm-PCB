using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.UtilityClasses
{
    public static class Globals
    {
        public const char Up = 'U';
        public const char Down = 'D';
        public const char Right = 'R';
        public const char Left = 'L';
        public const string PathFile = "D:\\1MojeProjekty\\6_Semestr\\2.Ai\\LAB\\Ai_lab1\\Backend\\Files";

        public const double CrossSegmentPW = 90;
        public const double PathLengthPW = 1;
        public const double AmountOfSegmentsPW = 1;
        public const double PathsOffBoardPW = 90;
        public const double PathsLengthOffBoardPW = 20;
        public const int MaxNumberOfSegments = 30;


        // Pm - for making any mutatation possible
        public const double Pm = 0.3;


        // Generic Algorithm
        public const int Iteration = 100;
        public const int AmountOfPopulation = 150;

        public const int TournamentSize = 20;
        public const double Px = 0.3;
        
    }
}
