using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MSRA.SpringField.Controls
{
    public class ParameterSetting
    {

        #region
        public static int GetPageSize()
        {
            ParaConfig ps = ConfigurationManager.GetSection("ParaConfig") as ParaConfig;
            return ps.PageSize;
        }
        #endregion
    }

    class ParaConfig : ConfigurationSection
    {
        [ConfigurationProperty("PageSize")]
        public int PageSize
        {
            get { return (int)this["PageSize"]; }
            set { this["PageSize"] = value; }
        }
    }
}
