using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ResumeCollector.ResumeHelper
{
    public interface IPropertyHandler
    {
        void PopulateProperty(PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj);
    }
}
