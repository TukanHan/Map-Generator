using System;
using System.Collections.Generic;

namespace MapGenerator.UnityPort
{
    public class ValidationError
    {
        public ValidationErrorType Error { get; set; }
        public Stack<string> CallStack { get; set; } = new Stack<string>();
        public string Property { get; set; }
        public string Message { get; set; }

        public ValidationError(ValidationErrorType error, string property, string message = null)
        {
            Error = error;
            Property = property;
            Message = message;
        }

        public override string ToString()
        {
            string callStack = String.Join("->", String.Join("->", CallStack), Property);
            string errorWithCallStack = String.Format("{0} at {1}", Error, callStack);
            string errorWithCallStackAndMessage = String.Join("\n", errorWithCallStack, Message);

            return errorWithCallStackAndMessage;
        }
    }
}