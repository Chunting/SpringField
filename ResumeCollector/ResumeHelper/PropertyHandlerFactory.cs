using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using ResumeCollector.Lib;
using ResumeCollector.CustomExceptions;

namespace ResumeCollector.ResumeHelper
{
    public static class PropertyHandlerFactory
    {
        private static Hashtable handlerTable;
        private const string HANDLER = "Handler";

        static PropertyHandlerFactory()
        {
            handlerTable = new Hashtable();           
        }

        public static IPropertyHandler GetHandler(string control)
        {
            control += HANDLER;
            Type curHandlerType;
            object curHandlerObj;
            if (handlerTable[control] == null)
            {
                try
                {
                    curHandlerType = Type.GetType(AppConfiguration.Settings[control]);
                    curHandlerObj = Activator.CreateInstance(curHandlerType);
                    handlerTable.Add(control, curHandlerObj);
                }
                catch
                {
                    throw new PropertyHandlerNotExistException(control);
                }
            }
            return handlerTable[control] as IPropertyHandler;
        }
    }
}
