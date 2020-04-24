using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SudokuSolver.Tests
{
    // http://sudopedia.enjoysudoku.com/Test_Cases.html
    public class SudokuSolverTests
    {
        [Fact]
        public void CompletedPuzzle()
        {
            // Arrange
            const string Givens = "974236158638591742125487936316754289742918563589362417867125394253649871491873625";
            Sudoku sudoku = new Sudoku(Givens);

            // Act
            string solution = sudoku.Solve();

            // Assert
            Assert.Equal(Givens, solution);
        }

        [Fact]
        public void LastEmptySquare()
        {
            // Arrange
            const string Givens = "2564891733746159829817234565932748617128.6549468591327635147298127958634849362715";
            const string Solution = "256489173374615982981723456593274861712836549468591327635147298127958634849362715";
            Sudoku sudoku = new Sudoku(Givens);

            // Act
            string solution = sudoku.Solve();

            // Assert
            Assert.Equal(Solution, solution);
        }

        [Fact]
        public void NakedSingles()
        {
            // Arrange
            const string Givens = "3.542.81.4879.15.6.29.5637485.793.416132.8957.74.6528.2413.9.655.867.192.965124.8";
            const string Solution = "365427819487931526129856374852793641613248957974165283241389765538674192796512438";
            Sudoku sudoku = new Sudoku(Givens);

            // Act
            string solution = sudoku.Solve();

            // Assert
            Assert.Equal(Solution, solution);
        }

        [Fact]
        public void HiddenSingles()
        {
            // Arrange
            const string Givens = "..2.3...8.....8....31.2.....6..5.27..1.....5.2.4.6..31....8.6.5.......13..531.4..";
            const string Solution = "672435198549178362831629547368951274917243856254867931193784625486592713725316489";
            Sudoku sudoku = new Sudoku(Givens);

            // Act
            string solution = sudoku.Solve();

            // Assert
            Assert.Equal(Solution, solution);
        }

        [Fact]
        public void Empty_ThrowsInsufficientGivens()
        {
            // Arrange
            string givens = new string('.', 81);
            Sudoku sudoku = new Sudoku(givens);

            // Act & Assert
            Assert.Throws<InsufficientGivensException>(() => sudoku.Solve());
        }

        [Fact]
        public void SingleGiven_ThrowsInsufficientGivens()
        {
            // Arrange
            const string Givens = "........................................1........................................";
            Sudoku sudoku = new Sudoku(Givens);

            // Act & Assert
            Assert.Throws<InsufficientGivensException>(() => sudoku.Solve());
        }

        [Fact]
        public void InsufficientGiven_ThrowsInsufficientGivens()
        {
            // Arrange
            // 9x9 Sudokus need to have at least 17 givens, this one has 16
            const string Givens = "...........5....9...4....1.2....3.5....7.....438...2......9.....1.4...6..........";
            Sudoku sudoku = new Sudoku(Givens);

            // Act & Assert
            Assert.Throws<InsufficientGivensException>(() => sudoku.Solve());
        }

        [Theory]
        [InlineData("..9.7...5..21..9..1...28....7...5..1..851.....5....3.......3..68........21.....87")]
        [InlineData("6.159.....9..1............4.7.314..6.24.....5..3....1...6.....3...9.2.4......16..")]
        [InlineData(".4.1..35.............2.5......4.89..26.....12.5.3....7..4...16.6....7....1..8..2.")]
        public void DuplicateGiven_ThrowsDuplicateGiven(string givens)
        {
            // Arrange
            Sudoku sudoku = new Sudoku(givens);

            // Act & Assert
            Assert.Throws<DuplicateGivenException>(() => sudoku.Solve());
        }

        [Fact]
        public void UnsolvableSquare_ThrowsNoUniqueSolution()
        {
            // Arrange
            const string Givens = "..9.287..8.6..4..5..3.....46.........2.71345.........23.....5..9..4..8.7..125.3..";
            Sudoku sudoku = new Sudoku(Givens);

            // Act & Assert
            Assert.Throws<UnsolvableSudokuException>(() => sudoku.Solve());
        }

        [Fact]
        public void UnsolvableBox_ThrowsNoUniqueSolution()
        {
            // Arrange
            const string Givens = ".9.3....1....8..46......8..4.5.6..3...32756...6..1.9.4..1......58..2....2....7.6.";
            Sudoku sudoku = new Sudoku(Givens);

            // Act & Assert
            Assert.Throws<UnsolvableSudokuException>(() => sudoku.Solve());
        }

        [Fact]
        public void UnsolvableColumn_ThrowsNoUniqueSolution()
        {
            // Arrange
            const string Givens = "....41....6.....2...2......32.6.........5..417.......2......23..48......5.1..2...";
            Sudoku sudoku = new Sudoku(Givens);

            // Act & Assert
            Assert.Throws<UnsolvableSudokuException>(() => sudoku.Solve());
        }

        [Fact]
        public void UnsolvableRow_ThrowsNoUniqueSolution()
        {
            // Arrange
            const string Givens = "9..1....4.14.3.8....3....9....7.8..18....3..........3..21....7...9.4.5..5...16..3";
            Sudoku sudoku = new Sudoku(Givens);

            // Act & Assert
            Assert.Throws<UnsolvableSudokuException>(() => sudoku.Solve());
        }

        [Theory]
        [InlineData(".39...12....9.7...8..4.1..6.42...79...........91...54.5..1.9..3...8.5....14...87.")]
        [InlineData("..3.....6...98..2.9426..7..45...6............1.9.5.47.....25.4.6...785...........")]
        [InlineData("....9....6..4.7..8.4.812.3.7.......5..4...9..5..371..4.5..6..4.2.17.85.9.........")]
        [InlineData("59.....486.8...3.7...2.1.......4.....753.698.....9.......8.3...2.6...7.934.....65")]
        [InlineData("...3165..8..5..1...1.89724.9.1.85.2....9.1....4.263..1.5.....1.1..4.9..2..61.8...")]
        public void MultipleSolutions_ThrowsMultipleSolutions(string givens)
        {
            // Arrange
            Sudoku sudoku = new Sudoku(givens);

            // Act & Assert
            Assert.Throws<MultipleSolutionsException>(() => sudoku.Solve());
        }
    }
}
