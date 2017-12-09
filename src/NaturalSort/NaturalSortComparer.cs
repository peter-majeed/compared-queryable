using System;
using System.Collections.Generic;

namespace NaturalSort
{
    /// <summary>
    /// Sorts strings "naturally", so that the string "2" shows up before the string "10".
    /// </summary>
    public class NaturalSortComparer : IComparer<string>
    {
        /// <summary>
        /// A global instance of the comparer.
        /// </summary>
        public static readonly NaturalSortComparer Instance = new NaturalSortComparer();

        static NaturalSortComparer()
        {
        }

        private NaturalSortComparer()
        {
        }

        /// <inheritdoc />
        public int Compare(string x, string y)
        {
            var digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var inputX = x;
            var inputY = y;

            if (inputX == null || inputY == null)
            {
                return string.Compare(inputX, inputY, StringComparison.OrdinalIgnoreCase);
            }

            // We'll be shortening x and y as we loop. If either end, we can do a straight comparison
            // of the remaining text.
            while (inputX.Length > 0 && inputY.Length > 0)
            {
                var indexX = inputX.IndexOfAny(digits);
                var indexY = inputY.IndexOfAny(digits);

                if (indexX == -1 || indexY == -1)
                {
                    // If there are no digits, then do a string comparison
                    return string.Compare(inputX, inputY, StringComparison.OrdinalIgnoreCase);
                }

                int compare;

                if (indexX > 0 || indexY > 0)
                {
                    var compareX = inputX.Substring(0, indexX == 0 ? inputX.Length : indexX);
                    var compareY = inputY.Substring(0, indexY == 0 ? inputY.Length : indexY);

                    compare = string.Compare(compareX, compareY, StringComparison.OrdinalIgnoreCase);
                    if (compare != 0)
                    {
                        // If the substring before the first digits are not equal, then return the comparison
                        return compare;
                    }
                }

                // Set x and y to the remaining strings and compare the next section, which is a set of digits
                inputX = inputX.Substring(indexX);
                inputY = inputY.Substring(indexY);

                indexX = LengthOfStringOfDigits(inputX);
                indexY = LengthOfStringOfDigits(inputY);

                var digitX = inputX.Substring(0, indexX);
                var digitY = inputY.Substring(0, indexY);

                if (indexX != indexY)
                {
                    // If the digits are of different length, left-pad them to the same length with zeros
                    var maxLength = indexX > indexY ? indexX : indexY;
                    digitX = PadDigits(digitX, maxLength);
                    digitY = PadDigits(digitY, maxLength);
                }
                compare = string.Compare(digitX, digitY, StringComparison.OrdinalIgnoreCase);

                if (compare != 0)
                {
                    // If the sequence of digits isn't equal, then return the comparison
                    return compare;
                }

                // Loop with the remaining strings after removing the digits
                inputX = inputX.Substring(indexX);
                inputY = inputY.Substring(indexY);
            }
            return string.Compare(inputX, inputY, StringComparison.OrdinalIgnoreCase);
        }

        private static string PadDigits(string input, int minimumLength)
        {
            var length = input.Length;
            if (length < minimumLength)
            {
                return new string('0', minimumLength - length) + input;
            }
            return input;
        }

        private static int LengthOfStringOfDigits(string input)
        {
            var length = input.Length;
            for (var i = 0; i < length; i++)
            {
                if (!char.IsDigit(input[i]))
                {
                    return i;
                }
            }
            return length;
        }
    }
}