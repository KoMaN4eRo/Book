using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTests.Infrastructure
{
    public class NonNumericSequenceException: Exception
    {
        public string Property { get; protected set; }
        public NonNumericSequenceException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
