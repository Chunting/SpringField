using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSRA.Springfield.IntegrationWorkflow
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
	}
}
