using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public static class CashedApplicantInfo
    {
        public static bool AddCashedApplicant(Guid ApplicantId, int InterviewId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.AddCashedApplicant(ApplicantId, InterviewId);
        }
        public static DataSet GetAllApplcantsforSourcingReport(DateTime StartTime, DateTime EndTime)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetAllApplcantsforSourcingReport(StartTime, EndTime);
        }
    }
}
