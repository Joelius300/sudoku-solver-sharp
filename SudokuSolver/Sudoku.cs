using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    public class Sudoku
    {
        private readonly int _side;
        private readonly byte[,] _field;

        public Sudoku(string field)
        {
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentException("The field can't be null or whitespace.");

            _field = SudokuHelper.FieldFromString(field);
            _side = _field.GetLength(0);
        }

        public string Solve()
        {
            HashSet<byte>[] rowDigits = new HashSet<byte>[_side];
            HashSet<byte>[] columnDigits = new HashSet<byte>[_side];
            HashSet<byte>[] quadrantDigits = new HashSet<byte>[_side];

            for (int i = 0; i < _side; i++)
            {
                rowDigits[i] = new HashSet<byte>();
                columnDigits[i] = new HashSet<byte>();
                quadrantDigits[i] = new HashSet<byte>();
            }

            int index = 0;
            int givenDigits = 0;
            for (int i = 0; i < _side; i++)
            {
                for (int j = 0; j < _side; j++)
                {
                    if (_field[i, j] > 0)
                    {
                        if (AddDigit(index, _field[i, j], rowDigits, columnDigits, quadrantDigits))
                        {
                            givenDigits++;
                        }
                        else
                        {
                            throw new DuplicateGivenException($"Duplicate entry '{_field[i, j]}' at index {index + 1} (row: {i + 1}, col: {j + 1}).");
                        }
                    }

                    index++;
                }
            }

            if (givenDigits == 0 ||
                (_side == 9 && givenDigits < 17)) // 9x9 Sudokus need to have at least 17 givens
            {
                throw new InsufficientGivensException();
            }

            if (Solve(0, rowDigits, columnDigits, quadrantDigits))
            {
                return SudokuHelper.FieldToString(_field);
            }
            else
            {
                throw new UnsolvableSudokuException();
            }
        }

        private bool Solve(int index, HashSet<byte>[] rowDigits, HashSet<byte>[] columnDigits, HashSet<byte>[] quadrantDigits)
        {
            if (index == _side * _side)
                return true;

            (int row, int col, _) = GetLocation(index);

            if (_field[row, col] == 0)
            {
                for (byte i = 1; i <= 9; i++)
                {
                    if (AddDigit(index, i, rowDigits, columnDigits, quadrantDigits))
                    {
                        _field[row, col] = i;

                        if (Solve(index + 1, rowDigits, columnDigits, quadrantDigits))
                            return true;

                        // if not a valid solution, backtrack
                        _field[row, col] = 0;
                        RemoveDigit(index, i, rowDigits, columnDigits, quadrantDigits);
                    }
                }
            }
            else
            {
                // if already occupied, go to the next one
                return Solve(index + 1, rowDigits, columnDigits, quadrantDigits);
            }

            return false;
        }

        private bool AddDigit(int index,
                              byte digit,
                              HashSet<byte>[] rowDigits,
                              HashSet<byte>[] columnDigits,
                              HashSet<byte>[] quadrantDigits)
        {
            (int row, int col, int quad) = GetLocation(index);

            if (!rowDigits[row].Contains(digit) &&
                !columnDigits[col].Contains(digit) &&
                !quadrantDigits[quad].Contains(digit))
            {
                rowDigits[row].Add(digit);
                columnDigits[col].Add(digit);
                quadrantDigits[quad].Add(digit);

                return true;
            }

            return false;
        }

        private void RemoveDigit(int index,
                                 byte digit,
                                 HashSet<byte>[] rowDigits,
                                 HashSet<byte>[] columnDigits,
                                 HashSet<byte>[] quadrantDigits)
        {
            (int row, int col, int quad) = GetLocation(index);

            rowDigits[row].Remove(digit);
            columnDigits[col].Remove(digit);
            quadrantDigits[quad].Remove(digit);
        }

        private (int row, int col, int quad) GetLocation(int index)
        {
            int row = index / _side;
            int col = index % _side;
            int quad = index / (3 * _side)  // row in quadrants-grid
                       * (_side / 3)        // count of quadrants per side
                       + index % _side / 3;

            return (row, col, quad);
        }
    }
}
