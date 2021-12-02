using AoCHelper;

namespace AdventOfCode
{
    public class Day_02 : BaseDay
    {
        private readonly string[] _input;

        public Day_02()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {CalculateFinalDepthAndPosition()}");

        public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {CalculateFinalDepthAndPositionByManualAlgorithm()}");

        private int CalculateFinalDepthAndPosition()
        {
            var depth = 0;
            var position = 0;

            var instructions = _input.Select(MapToInstruction);

            foreach (var instruction in instructions)
            {
                switch (instruction.Command)
                {
                    case "down":
                        depth += instruction.Value;
                        break;
                    case "forward":
                        position += instruction.Value;
                        break;
                    case "up":
                        depth -= instruction.Value;
                        break;
                    default:
                        Console.Error.WriteLine($"Unknown instruction: {instruction}");
                        break;
                }
            }

            return depth * position;
        }

        private int CalculateFinalDepthAndPositionByManualAlgorithm()
        {
            var depth = 0;
            var position = 0;
            var aim = 0;

            var instructions = _input.Select(MapToInstruction);

            foreach (var instruction in instructions)
            {
                switch (instruction.Command)
                {
                    case "down":
                        aim += instruction.Value;
                        break;
                    case "forward":
                        position += instruction.Value;
                        depth += aim * instruction.Value;
                        break;
                    case "up":
                        aim -= instruction.Value;
                        break;
                    default:
                        Console.Error.WriteLine($"Unknown instruction: {instruction}");
                        break;
                }
            }

            return depth * position;
        }

        private Instruction MapToInstruction(string input)
        {
            var instruction = input.Split(' ');

            return new Instruction(instruction[0], int.Parse(instruction[1]));
        }
    }

    public record Instruction(string Command, int Value);
}
