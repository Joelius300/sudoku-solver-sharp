using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    [Serializable]
    public class MultipleSolutionsException : Exception
    {
        public MultipleSolutionsException() { }
        public MultipleSolutionsException(string message) : base(message) { }
        public MultipleSolutionsException(string message, Exception inner) : base(message, inner) { }
        protected MultipleSolutionsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
