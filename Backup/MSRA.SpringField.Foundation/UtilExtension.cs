using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSRA.SpringField.Foundation
{
    public static class UtilExtension
    {
        public static string ParseString(this object para1)
        {
            if (para1 == null || para1 == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return para1.ToString();
            }
        }

        public static int ParseInt(this object para1)
        {
            if (para1 == null || para1 == DBNull.Value)
            {
                return -1;
            }
            else
            {
                return int.Parse(para1.ToString());
            }
        }

        public static decimal ParseDecimal(this object para1)
        {
            if (para1 == null || para1 == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(para1);
            }
        }

        public static DateTime ParseDateTime(this object para1)
        {
            if (para1 == null || para1 == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(para1);
            }
        }
    }
}
