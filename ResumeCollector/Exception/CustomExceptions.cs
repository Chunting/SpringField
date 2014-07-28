using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeCollector.CustomExceptions
{
    public class PropertyNotExistExceptions : System.Exception
    {
        public PropertyNotExistExceptions(string propertyName)
            : base("the property " + propertyName + " is not exist in the current node")
        {
        }
    }

    public class PropertyHandlerNotExistException : System.Exception
    {
        public PropertyHandlerNotExistException(string handlerName)
            : base("the property handler " + handlerName + " can not be created")
        {
        }
    }

    public class ResumeLocationNotExistException : System.Exception
    {
        public ResumeLocationNotExistException(string location)
            : base("the resume folder [" + location + "] can not be found")
        {
        }
    }

    public class TaskHasStartedException : System.Exception
    {
        public TaskHasStartedException() : base ("current task has started")
        { }
    }

    public class ResumeFileNotExistException : System.Exception
    {
        public ResumeFileNotExistException()
            : base("can't find the excel resume file")
        { }
    }

    public class PropertyIsEmptyException : System.Exception
    {
        public PropertyIsEmptyException(string propertyName)
            : base("the required property [" + propertyName + "] is empty")
        { }
    }

    public class PropertyIsInvalidException : System.Exception
    {
        public PropertyIsInvalidException(string propertyName)
            : base("the property [" + propertyName + "] is invalid")
        { }
    }

    public class ResumeInvailidException : System.Exception
    {
        public ResumeInvailidException()
            : base("system can't find resume document named by Resume textbox's value in Application Form, please check whether it is correct")
        { }
    }

    public class PpaerInvailidException : System.Exception
    {
        public PpaerInvailidException()
            : base("system can't find paper document named by Paper textbox's value in Application Form, please check whether it is correct")
        { }
    }
}
