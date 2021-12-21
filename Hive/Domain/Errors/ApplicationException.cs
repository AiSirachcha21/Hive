using System;

namespace Hive.Domain.Errors
{
    public class ApplicationException : Exception
    {
        public string Title { get; }

        public ApplicationException(string title, string message) : base(message)
        {
            Title = title;
        }
    }
}
