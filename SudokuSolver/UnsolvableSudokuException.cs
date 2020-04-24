using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    [Serializable]
    public class UnsolvableSudokuException : Exception
    {
        public UnsolvableSudokuException() { }
        public UnsolvableSudokuException(string message) : base(message) { }
        public UnsolvableSudokuException(string message, Exception inner) : base(message, inner) { }
        protected UnsolvableSudokuException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
