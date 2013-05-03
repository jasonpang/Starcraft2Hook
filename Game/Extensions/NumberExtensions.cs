using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Extensions
{
    public static class NumberExtensions
    {
        public static double RoundTo (this double value, int numFractionalDigits)
        {
            return Math.Round(value, numFractionalDigits);
        }

        public static float RoundTo(this float value, int numFractionalDigits)
        {
            return (float) Math.Round(value, numFractionalDigits);
        }
    }
}
