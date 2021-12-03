using AoCHelper;

namespace AdventOfCode
{
    public class Day_03 : BaseDay
    {
        private readonly string[] _input;

        public Day_03()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {CalculateGammeAndEpsilonRadiation()}");

        public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {CalculateLifeSupportRating()}");

        private int CalculateGammeAndEpsilonRadiation()
        {
            var gamma = string.Empty;
            var epsilon = string.Empty;
            var radiationDict = new SortedDictionary<int, int>();

            foreach (var line in _input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        radiationDict[i] = radiationDict.ContainsKey(i)
                        ? radiationDict[i] + 1
                        : 1;
                    }
                }
            }

            foreach (var kvp in radiationDict)
            {
                if (kvp.Value > _input.Length / 2)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }
            }

            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }

        private int CalculateLifeSupportRating()
        {
            var significantBit = SignificantBit(_input, 0);
            var leastSignificantBit = significantBit == '1' ? '0' : '1';

            var oxygenRating = TraverseInput(_input, 0, significantBit);
            var scrubberRating = TraverseInput(_input, 0, leastSignificantBit, true);

            return Convert.ToInt32(oxygenRating, 2) * Convert.ToInt32(scrubberRating, 2);
        }

        private string TraverseInput(string[] input, int index, char bit, bool useLeastSignificantBit = false)
        {
            var validInput = input.Where(i => i[index] == bit).ToArray();
            
            if (validInput.Length == 1)
            {
                return validInput[0];
            }

            bit = SignificantBit(validInput, index + 1);

            if (useLeastSignificantBit)
            {
                bit = bit == '1' ? '0' : '1';
            }

            return TraverseInput(validInput, index + 1, bit, useLeastSignificantBit);
        }

        private char SignificantBit(string[] input, int index)
        {
            var numberOfOnes = input.Where(i => i[index] == '1').Count();

            return numberOfOnes >= Math.Ceiling(input.Length / 2.0) ? '1' : '0';
        }
    }
}
