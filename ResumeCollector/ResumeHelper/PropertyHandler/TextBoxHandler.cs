using System;
using System.Collections.Generic;
using System.Text;
using ResumeCollector.CustomExceptions;

namespace ResumeCollector.ResumeHelper
{
    public class TextBoxHandler : IPropertyHandler
    {

        #region IPropertyHandler Members

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            string valueObj = resumeScheme.GetTextBoxValue(1, propertyStruct.MappingName);
            object propertyValue = null;
            if(propertyStruct.Required && String.IsNullOrEmpty(valueObj))
            {
                // Add default value for "Other" JobChannel
                throw new PropertyIsEmptyException(String.Format("{0}{1}", !String.IsNullOrEmpty(propertyStruct.DataValue) ? String.Format("{0} - ", propertyStruct.DataValue) : "", propertyStruct.PropertyName));
            }
            try
            {
                propertyValue = Convert.ChangeType(valueObj, Type.GetType(propertyStruct.DataType));
            }
            catch
            {
                if(Type.GetType(propertyStruct.DataType).Equals(typeof(DateTime)))
                {
                    if ("GraduateDate" == propertyStruct.PropertyName)
                    {
                        propertyValue = (object)(DateTime.Today + TimeSpan.FromDays(365));
                    }
                    else if ("PreferredAvailStartDate" == propertyStruct.PropertyName)
                    {
                        propertyValue = (object)(DateTime.Today);
                    }
                    else if ("SecondaryAvailStartDate" == propertyStruct.PropertyName)
                    {
                        propertyValue = (object)(DateTime.Today + TimeSpan.FromDays(90));
                    }
                }

            }
            finally
            {
                propertyInfo.SetValue(parentObj, propertyValue, null);
            }
        }

        #endregion
    }
}
