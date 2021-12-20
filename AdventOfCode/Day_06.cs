using AoCHelper;

namespace AdventOfCode
{
    public class Day_06 : BaseDay
    {
        private readonly string[] _input;

        public Day_06()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {CountLanternFish(numberOfDays: 80)}");

        public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {CountLanternFish(numberOfDays: 256)}");

        private long CountLanternFish(int numberOfDays)
        {
            var lanternFishs = _input[0].Split(',').Select(int.Parse).ToList();

            long[] fishSchool = new long[9];

            foreach (var fish in lanternFishs)
            {
                fishSchool[fish]++;
            }

            foreach (var _ in Enumerable.Range(0, numberOfDays))
            {
                var newFish = fishSchool[0];
                fishSchool[0] = fishSchool[1];
                fishSchool[1] = fishSchool[2];
                fishSchool[2] = fishSchool[3];
                fishSchool[3] = fishSchool[4];
                fishSchool[4] = fishSchool[5];
                fishSchool[5] = fishSchool[6];
                fishSchool[6] = fishSchool[7];
                fishSchool[7] = fishSchool[8];
                fishSchool[8] = newFish;
                fishSchool[6] += newFish;
            }

            long sum = 0;
            
            foreach (var fish in fishSchool)
            {
                sum += fish;
            }

            return sum;
        }
    }
}
