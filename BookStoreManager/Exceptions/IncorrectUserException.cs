using System;

namespace BookStoreManager.Exceptions
{
    public class IncorrectUserException : Exception
    {
        public IncorrectUserException(string message): base(message)
        {
        }
    }
}
