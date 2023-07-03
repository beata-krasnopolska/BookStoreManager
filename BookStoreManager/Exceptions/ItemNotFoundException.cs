using System;

namespace BookStoreManager.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message)
        {

        }
    }
}
