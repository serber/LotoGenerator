using Loto.Core.Randoms;
using System;

namespace Loto.Core
{
    public class RussianLotoGameGenerator : ILotoGameGenerator
    {
        private readonly IRandomGenerator _randomGenerator;

        public RussianLotoGameGenerator(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public int[] Generate()
        {
            const int min = 1;
            const int max = 90;

            const int rows = 3;
            const int columns = 5;

            const int size = rows * columns;

            var numbers = new int[size];

            for (var i = 0; i < rows; i++)
            {
                var exceptTens = new int[columns];
                var line = new int[columns];
                var exceptTensIndex = 0;

                for (var j = 0; j < columns; j++)
                {
                    var valid = false;

                    do
                    {
                        var number = _randomGenerator.Generate(min, max);

                        var ten = number / 10;

                        if (Array.IndexOf(exceptTens, ten) == -1)
                        {
                            line[j] = number;
                            exceptTens[exceptTensIndex++] = ten;

                            valid = true; ;
                        }
                    }
                    while (!valid);
                }

                Array.Sort(line);
                Array.Copy(line, 0, numbers, i * line.Length, line.Length);
            }

            return numbers;
        }
    }
}