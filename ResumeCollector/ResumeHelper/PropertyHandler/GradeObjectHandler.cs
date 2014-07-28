using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeCollector.ResumeHelper
{
    public class GradeObjectHandler : IPropertyHandler
    {
        #region IPropertyHandler Members

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            int yearOfStudy = Convert.ToInt32(resumeScheme.GetComboBoxValue(1, propertyStruct.MappingName));
            propertyInfo.SetValue(parentObj, yearOfStudy, null);
        }

        #endregion
    }
}
