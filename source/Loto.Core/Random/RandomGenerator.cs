using Loto.Core.Randoms;
using System;
using System.Security.Cryptography;

namespace Loto.Core.Random
{
    public class RandomGenerator : IRandomGenerator
    {
        public int Generate(int minValue, int maxValue)
        {
            return GenerateInternal(minValue, maxValue);
        }

        private int GenerateInternal(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue));
            }

            if (minValue == maxValue)
            {
                return minValue;
            }
            
            using RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            var diff = maxValue - minValue;
            while (true)
            {
                var _uint32Buffer = new byte[4];

                rng.GetBytes(_uint32Buffer);

                var rand = BitConverter.ToUInt32(_uint32Buffer, 0);

                var max = 1 + (long)int.MaxValue;
                var remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (int)(minValue + (rand % diff));
                }
            }
        }
    }
}