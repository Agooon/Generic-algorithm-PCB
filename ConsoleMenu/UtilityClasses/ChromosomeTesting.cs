﻿using Backend.Problem;
using Backend.Solution;
using Backend.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMenu.UtilityClasses
{
    public static class ChromosomeTesting
    {
        public static List<Chromosome> GetBasicSolutions()
        {
            ProblemPCB problem = new ProblemPCB(Globals.PathFile + "\\zad0.txt");
            Chromosome chromosome = new Chromosome(problem);
            chromosome.Paths = new List<Path>(){
                new Path()
                {
                   StartingPoint = chromosome.Problem.PointPairs[0].Item1,
                   Segments = new List<Segment>(){
                       new Segment()
                       {
                           Direction = Globals.Up,
                           Length = 3
                       },
                       new Segment()
                       {
                           Direction = Globals.Right,
                           Length = 4
                       },
                       new Segment()
                       {
                           Direction = Globals.Down,
                           Length = 3
                       }
                   }
                },
                new Path()
                {
                   StartingPoint = chromosome.Problem.PointPairs[1].Item1,
                   Segments = new List<Segment>(){
                       new Segment()
                       {
                           Direction = Globals.Left,
                           Length = 3
                       },
                       new Segment()
                       {
                           Direction = Globals.Up,
                           Length = 2
                       },
                       new Segment()
                       {
                           Direction = Globals.Right,
                           Length = 3
                       }
                   }
                }
            };

            Chromosome chromosome2 = new Chromosome(problem);
            chromosome2.Paths = new List<Path>(){
                new Path()
                {
                   StartingPoint = chromosome.Problem.PointPairs[0].Item1,
                   Segments = new List<Segment>(){
                       new Segment()
                       {
                           Direction = Globals.Right,
                           Length = 2
                       },
                       new Segment()
                       {
                           Direction = Globals.Left,
                           Length = 2
                       },
                       new Segment()
                       {
                           Direction = Globals.Up,
                           Length = 3
                       },
                       new Segment()
                       {
                           Direction = Globals.Right,
                           Length = 4
                       },
                       new Segment()
                       {
                           Direction = Globals.Down,
                           Length = 3
                       }
                   }
                },
                new Path()
                {
                   StartingPoint = chromosome.Problem.PointPairs[1].Item1,
                   Segments = new List<Segment>(){
                       new Segment()
                       {
                           Direction = Globals.Left,
                           Length = 3
                       },
                       new Segment()
                       {
                           Direction = Globals.Up,
                           Length = 2
                       },
                       new Segment()
                       {
                           Direction = Globals.Right,
                           Length = 3
                       }
                   }
                }
            };

            Chromosome chromosome3 = new Chromosome(problem);
            chromosome3.Paths = new List<Path>(){
                new Path()
                {
                   StartingPoint = chromosome.Problem.PointPairs[0].Item1,
                   Segments = new List<Segment>(){
                       new Segment()
                       {
                           Direction = Globals.Up,
                           Length = 3
                       },
                       new Segment()
                       {
                           Direction = Globals.Right,
                           Length = 4
                       },
                       new Segment()
                       {
                           Direction = Globals.Down,
                           Length = 3
                       }
                   }
                },
                new Path()
                {
                   StartingPoint = chromosome.Problem.PointPairs[1].Item1,
                   Segments = new List<Segment>(){
                       new Segment()
                       {
                           Direction = Globals.Up,
                           Length = 2
                       }
                   }
                }
            };

            return new List<Chromosome>(){
                chromosome,
                chromosome2,
                chromosome3
            };
        }
    }
}
