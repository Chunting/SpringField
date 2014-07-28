using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResumeCollector.Exception
{
    public class SchemaFormatException : System.Exception
    {
        public SchemaFormatException(string msg)
            : base(msg)
        { }
    }
}
