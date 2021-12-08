using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
