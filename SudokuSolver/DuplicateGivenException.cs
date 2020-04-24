using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    [Serializable]
    public class DuplicateGivenException : Exception
    {
        public DuplicateGivenException() { }
        public DuplicateGivenException(string message) : base(message) { }
        public DuplicateGivenException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateGivenException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
