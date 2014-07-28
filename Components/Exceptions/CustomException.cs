using System;
using System.Collections.Generic;
using System.Text;

namespace MSRA.SpringField.Components.Exceptions
{
    class ResourceNotExist : Exception
    {
        private string resCategory;
        private string resValue;

        public string Category
        {
            get { return resCategory; }
        }

        public string Value
        {
            get { return resValue; }
        }

        public ResourceNotExist(string category, string value)
            : base(String.Format("Value {0} is NOT exist in the resource {1}", category, value))
        {
            resCategory = category;
            resValue = value;
        }
    }

    class ResourceAlreadyExist : Exception
    {
        private string resCategory;
        private string resValue;

        public string Category
        {
            get { return resCategory; }
        }

        public string Value
        {
            get { return resValue; }
        }

        public ResourceAlreadyExist(string category, string value)
            : base(String.Format("Value {0} is already exist in the resource {1}", category, value))
        {
            resCategory = category;
            resValue = value;
        }
    }
}
