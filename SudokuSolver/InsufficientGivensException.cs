using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    [Serializable]
    public class InsufficientGivensException : Exception
    {
        public InsufficientGivensException() { }
        public InsufficientGivensException(string message) : base(message) { }
        public InsufficientGivensException(string message, Exception inner) : base(message, inner) { }
        protected InsufficientGivensException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
