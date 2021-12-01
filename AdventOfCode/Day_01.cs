using AoCHelper;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        private readonly string[] _input;

        public Day_01()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {CalculateIncreasedDepthCount()}");

        public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {CalculateThreeMeasurementSlidingWindowDepthCount()}");

        private int CalculateIncreasedDepthCount()
        {
            var inputAsInt = _input.Select(i => int.Parse(i));

            int? earlierValue = null;
            var depthIncreasedCounter = 0;

            foreach (var value in inputAsInt)
            {
                if (earlierValue.HasValue && earlierValue.Value < value)
                {
                    depthIncreasedCounter++;
                }

                earlierValue = value;
            }

            return depthIncreasedCounter;
        }

        private int CalculateThreeMeasurementSlidingWindowDepthCount()
        {
            var inputAsInt = _input.Select(i => int.Parse(i)).ToList();

            int? earlierValue = null;
            var threeMeasurementSlidingWindowDepthCounter = 0;

            for (int i = 0; i < inputAsInt.Count - 2; i++)
            {
                var sum = inputAsInt[i] + inputAsInt[i + 1] + inputAsInt[i + 2];

                if (earlierValue.HasValue && earlierValue.Value < sum)
                {
                    threeMeasurementSlidingWindowDepthCounter++;
                }

                earlierValue = sum;
            }

            return threeMeasurementSlidingWindowDepthCounter;
        }
    }
}
