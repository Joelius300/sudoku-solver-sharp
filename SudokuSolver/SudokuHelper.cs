using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    public static class SudokuHelper
    {
        private const byte ASCII_0 = 48;

        /// <summary>
        /// Transforms a <see cref="string"/> to a 2d <see cref="byte"/>
        /// array which can be used in the <see cref="Sudoku"/> class.
        /// <para>
        /// The <paramref name="value"/> can't contain whitespace and only
        /// consists of values from 0-9 and period (.) where 0 and . mean an
        /// empty field.
        /// </para>
        /// </summary>
        /// <param name="value">The string in the correct format.</param>
        public static byte[,] FieldFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("The value can't be null or empty.", nameof(value));

            double fieldSide = Math.Sqrt(value.Length);
            if (fieldSide % 3 > 0.00001)
                throw new ArgumentException("The number of characters has to be the square of a multiple of 3.");

            int side = Convert.ToInt32(fieldSide);
            byte[,] arr = new byte[side, side];

            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                {
                    char c = value[(i * side) + j];
                    if (c == '.')
                        continue;

                    byte digit = (byte)(c - ASCII_0);

                    if (digit < 0 || digit > 9)
                        throw new ArgumentException("Only 0-9 and '.' are allowed characters.");
                    
                    arr[i, j] = digit;
                }
            }

            return arr;
        }

        public static string FieldToString(byte[,] field)
        {
            if (field == null)
                throw new ArgumentNullException();

            int side = field.GetLength(0);
            if (side != field.GetLength(1))
                throw new ArgumentException("The field has to be square.");

            if (side % 3 != 0)
                throw new ArgumentException("The columns and rows have to be a multiple of 3.");

            StringBuilder sb = new StringBuilder(side * side);
            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                {
                    byte digit = field[i, j];
                    if (digit < 0 || digit > 9)
                        throw new ArgumentException("Only 0-9 are allowed values.");

                    char toAppend = '.';
                    if (digit != 0)
                    {
                        toAppend = (char)(digit + ASCII_0);
                    }

                    sb.Append(toAppend);
                }
            }

            return sb.ToString();
        }
    }
}
