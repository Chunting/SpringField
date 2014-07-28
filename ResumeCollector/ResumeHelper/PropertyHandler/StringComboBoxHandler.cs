using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeCollector.ResumeHelper
{
    public class StringComboBoxHandler : IPropertyHandler
    {

        #region IPropertyHandler Members

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObject)
        {
            propertyInfo.SetValue(parentObject, resumeScheme.GetComboBoxValue(1, propertyStruct.MappingName), null);
        }

        #endregion
    }
}
