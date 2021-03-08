using Backend.Problem;
using Backend.UtilityClasses;
using System;
using System.IO;
using System.Reflection;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int[] xd = new int[100];
            Console.WriteLine(xd.Length);

            ProblemPCB problem = new ProblemPCB(Globals.PathFile+"\\zad0.txt");
            int x = 0;
        }
    }
}
