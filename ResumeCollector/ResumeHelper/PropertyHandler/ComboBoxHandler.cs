using System;
using System.Collections.Generic;
using System.Text;
using ResumeCollector.CustomExceptions;

namespace ResumeCollector.ResumeHelper
{
    public class ComboBoxHandler : IPropertyHandler
    {

        #region IPropertyHandler Members

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObject)
        {
            string valueObj = resumeScheme.GetComboBoxValue(1, propertyStruct.MappingName);
            if(propertyStruct.Required && String.IsNullOrEmpty(valueObj))
            {
                throw new PropertyIsEmptyException(String.Format("{0}{1}", !String.IsNullOrEmpty(propertyStruct.DataValue) ? String.Format("{0} - ", propertyStruct.DataValue) : "", propertyStruct.PropertyName));
            }

            propertyInfo.SetValue(parentObject, Convert.ChangeType(valueObj, Type.GetType(propertyStruct.DataType)), null);
        }

        #endregion
    }
}
