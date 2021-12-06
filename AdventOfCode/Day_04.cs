using AoCHelper;

namespace AdventOfCode
{
    public class Day_04 : BaseDay
    {
        private readonly string[] _input;

        public Day_04()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1: {PlayBingo()}");

        public override ValueTask<string> Solve_2() => new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2: {LoseBingo()}");

        private int PlayBingo()
        {
            var bingo = ParseInput();

            var boards = bingo.GetScores();

            return boards[0];
        }

        private int LoseBingo()
        {
            var bingo = ParseInput();

            var boards = bingo.GetScores();

            return boards[^1];
        }

        private Bingo ParseInput()
        {
            var numbers = _input[0].Split(',').Select(x => int.Parse(x)).ToArray();

            var bingo = new Bingo(numbers);

            var boardLines = new List<int>();
            foreach (var line in _input.Skip(2))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    bingo.AddBoard(boardLines.ToArray());
                    boardLines.Clear();
                    continue;
                }

                boardLines.AddRange(line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)));
            }

            bingo.AddBoard(boardLines.ToArray());

            return bingo;
        }
    }

    public class Bingo
    {
        private readonly List<Board> _boards;
        private readonly int[] _numbers;

        public Bingo(int[] numbers)
        {
            _boards = new List<Board>();
            _numbers = numbers;
        }

        public int[] GetScores()
        {
            var winningScores = new List<int>();
            foreach (var number in _numbers)
            {
                foreach (var board in _boards.Where(b => !b.HasWon))
                {
                    if (board.IsWinningNumber(number))
                    {
                        board.HasWon = true;
                        winningScores.Add(board.CalculateScore(number));
                    }
                }
            }

            return winningScores.ToArray();
        }

        public void AddBoard(int[] numbers)
        {
            var board = new Board();
            board.Populate(numbers);

            _boards.Add(board);
        }
    }

    public class Board
    {
        private bool[] MarkedNumbers;
        private int[] Numbers;
        private readonly Dictionary<int, int> NumberToIndexDict = new();

        public Board()
        {
            MarkedNumbers = new bool[5 * 5];
            Numbers = new int[5 * 5];
        }

        public bool HasWon { get; set; }

        public void Populate(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];
                Numbers[i] = number;
                NumberToIndexDict[number] = i;
            }
        }

        public bool IsWinningNumber(int number)
        {
            if (NumberToIndexDict.TryGetValue(number, out var index))
            {
                MarkedNumbers[index] = true;

                return
                    (MarkedNumbers[0] && MarkedNumbers[1] && MarkedNumbers[2] && MarkedNumbers[3] && MarkedNumbers[4]) || (MarkedNumbers[0] && MarkedNumbers[5] && MarkedNumbers[10] && MarkedNumbers[15] && MarkedNumbers[20]) ||
                    (MarkedNumbers[5] && MarkedNumbers[6] && MarkedNumbers[7] && MarkedNumbers[8] && MarkedNumbers[9]) || (MarkedNumbers[1] && MarkedNumbers[6] && MarkedNumbers[11] && MarkedNumbers[16] && MarkedNumbers[21]) ||
                    (MarkedNumbers[10] && MarkedNumbers[11] && MarkedNumbers[12] && MarkedNumbers[13] && MarkedNumbers[14]) || (MarkedNumbers[2] && MarkedNumbers[7] && MarkedNumbers[12] && MarkedNumbers[17] && MarkedNumbers[22]) ||
                    (MarkedNumbers[15] && MarkedNumbers[16] && MarkedNumbers[17] && MarkedNumbers[18] && MarkedNumbers[19]) || (MarkedNumbers[3] && MarkedNumbers[8] && MarkedNumbers[13] && MarkedNumbers[18] && MarkedNumbers[23]) ||
                    (MarkedNumbers[20] && MarkedNumbers[21] && MarkedNumbers[22] && MarkedNumbers[23] && MarkedNumbers[24]) || (MarkedNumbers[4] && MarkedNumbers[9] && MarkedNumbers[14] && MarkedNumbers[19] && MarkedNumbers[24]);
            }

            return false;
        }

        public int CalculateScore(int winningNumber)
        {
            var sum = 0;

            for (int i = 0; i < MarkedNumbers.Length; i++)
            {
                if (!MarkedNumbers[i])
                {
                    sum += Numbers[i];
                }
            }

            return sum * winningNumber;
        }

        public override string ToString() => $"{nameof(HasWon)}: {HasWon}";
    }
}
