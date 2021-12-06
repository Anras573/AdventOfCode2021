using AoCHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day_05 : BaseDay
    {
        private readonly string[] _input;

        public Day_05()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {CalculateNumberOfOverlappingLines()}");

        public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {CalculateNumberOfOverlappingLines(allowDiagonallyLines: true)}");

        private int CalculateNumberOfOverlappingLines(bool allowDiagonallyLines = false)
        {
            var seaFloor = ParseInput(allowDiagonallyLines);

            return seaFloor.NumberOfOverlappingLines();
        }

        private SeaFloor ParseInput(bool allowDiagonallyLines)
        {
            var points = new List<(Point, Point)>();
            var maxX = 0;
            var maxY = 0;

            foreach (var line in _input)
            {
                var data = line.Split(" -> ").SelectMany(x => x.Split(',')).Select(x => int.Parse(x)).ToList();
                var point1 = new Point(data[0], data[1]);
                var point2 = new Point(data[2], data[3]);

                if (point1.X > maxX) maxX = point1.X;
                if (point2.X > maxX) maxX = point2.X;
                if (point1.Y > maxY) maxY = point1.Y;
                if (point2.Y > maxY) maxY = point2.Y;

                points.Add((point1, point2));
            }

            var seaFloor = new SeaFloor(maxX + 1, maxY + 1);

            foreach (var (from, to) in points)
            {
                if (allowDiagonallyLines)
                {
                    seaFloor.DrawLine(from, to);
                }
                else
                {
                    seaFloor.DrawOrthogonalLine(to, from);
                }
                
            }

            return seaFloor;
        }
    }

    public class SeaFloor
    {
        private readonly int[,] Grid;

        public SeaFloor(int maxX, int maxY)
        {
            Grid = new int[maxY, maxX];
        }

        public void DrawOrthogonalLine(Point from, Point to)
        {
            if (from.X == to.X || from.Y == to.Y)
            {
                for (int x = Math.Min(from.X, to.X); x < Math.Max(from.X, to.X) + 1; x++)
                {
                    for (int y = Math.Min(from.Y, to.Y); y < Math.Max(from.Y, to.Y) + 1; y++)
                    {
                        Grid[y, x]++;
                    }
                }
            }
        }

        public void DrawLine(Point from, Point to)
        {
            if (from.X == to.X || from.Y == to.Y)
            {
                for (int x = Math.Min(from.X, to.X); x < Math.Max(from.X, to.X) + 1; x++)
                {
                    for (int y = Math.Min(from.Y, to.Y); y < Math.Max(from.Y, to.Y) + 1; y++)
                    {
                        Grid[y, x]++;
                    }
                }
            }
            else if (Math.Abs(from.X - to.X) == Math.Abs(from.Y - to.Y))
            {
                var incrementX = from.X < to.X ? 1 : from.X > to.X ? -1 : 0;
                var incrementY = from.Y < to.Y ? 1 : from.Y > to.Y ? -1 : 0;
                
                var currentX = from.X;
                var currentY = from.Y;

                while (currentX != to.X && currentY != to.Y)
                {
                    Grid[currentY, currentX]++;
                    currentX += incrementX;
                    currentY += incrementY;
                }

                Grid[currentY, currentX]++;
            }
        }

        public int NumberOfOverlappingLines()
        {
            var count = 0;

            foreach (var numberOfLines in Grid)
            {
                if (numberOfLines > 1) count++; 
            }

            return count;
        }

        public void Print()
        {
            var count = 0;
            foreach (var number in Grid)
            {
                var str = number == 0 ? "." : number.ToString();
                Console.Write($"{str}");
                count++;
                if (count % Grid.GetLength(0) == 0) Console.WriteLine();
            }
        }
    }

    public record Point(int X, int Y);
}
