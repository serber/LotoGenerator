using Loto.Core.Randoms;
using System;

namespace Loto.Core
{
    public class RussianLotoGameGeneratorV2 : ILotoGameGenerator
    {
        private readonly IRandomGenerator _randomGenerator;

        public RussianLotoGameGeneratorV2(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public int[] Generate()
        {
            const int min = 1;
            const int max = 90;

            const int rows = 3;
            const int columns = 5;

            const int rowSize = 9;
            const int maxColumnSize = 2;

            const int size = rows * columns;

            var numbers = new int[size];
            var columnSizes = new int[rowSize];
            
            for (var i = 0; i < rows; i++)
            {
                var line = new int[columns];
                var except = new bool[rowSize];
                                
                for (var j = 0; j < columns; j++)
                {
                    var valid = false;

                    do
                    {
                        var number = _randomGenerator.Generate(min, max);
                        var ten = number / 10;
                        var columnSizeIndex = ten == 9 ? 8 : ten;

                        if (columnSizes[columnSizeIndex] >= maxColumnSize)
                        {
                            continue;
                        }    

                        if (!except[ten])
                        {
                            if (Array.IndexOf(numbers, number) == -1)
                            {
                                line[j] = number;
                                except[ten] = valid = true;
                                columnSizes[columnSizeIndex]++;
                            }
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