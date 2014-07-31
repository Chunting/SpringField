using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public class CheckInForm
    {
        private int formId;
        private int groupId;
        private int projectId;
        private int positionId;
        private int internTypeId;
        private string mentorAlias;
        private DateTime preferCheckInDate;
        private DateTime preferLastWorkingDay;
        private bool advisorApproved;
        //private DateTime enrollDate;
        //private DateTime graduateDate;
        private string comments;

        public int FormId
        {
            get
            {
                return formId;
            }
            set
            {
                formId = value;
            }
        }
        public int GroupId
        {
            get
            {
                return groupId;
            }
            set
            {
                groupId = value;
            }
        }

        public int ProjectId
        {
            get
            {
                return projectId;
            }
            set
            {
                projectId = value;
            }
        }
        public int PositionId
        {
            get
            {
                return positionId;
            }
            set
            {
                positionId = value;
            }
        }
        public int InternTypeId
        {
            get
            {
                return internTypeId;
            }
            set
            {
                internTypeId = value;
            }
        }
        public string MentorAlias
        {
            get
            {
                return mentorAlias;
            }
            set
            {
                mentorAlias = value;
            }
        }
        public DateTime PreferCheckInDate
        {
            get
            {
                return preferCheckInDate;
            }
            set
            {
                preferCheckInDate = value;
            }
        }
        public DateTime PreferLastWorkingDay
        {
            get
            {
                return preferLastWorkingDay;
            }
            set
            {
                preferLastWorkingDay = value;
            }
        }
        public bool AdvisorApproved
        {
            get
            {
                return advisorApproved;
            }
            set
            {
                advisorApproved = value;
            }
        }
        //public DateTime EnrollDate
        //{
        //    get
        //    {
        //        return enrollDate;
        //    }
        //    set
        //    {
        //        enrollDate = value;
        //    }
        //}
        
        //public DateTime GraduateDate
        //{
        //    get
        //    {
        //        return graduateDate;
        //    }
        //    set
        //    {
        //        graduateDate = value;
        //    }
        //}
        public string Comments
        {            
            get
            {
                return comments;
            }
            set
            {
                comments = value;
            }
        }

        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            formId = dp.InsertCheckInForm(this);
        }

        public static CheckInForm GetCheckInFormById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetCheckInFormById(id);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateCheckInForm(this);
        }

        public static DataSet GetAllCheckinFormforHiringReport(DateTime StartDate, DateTime EndDate)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetAllCheckinFormforHiringReport(StartDate, EndDate);
        }
    }
}
