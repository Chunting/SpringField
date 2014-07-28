/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		FilterGenerator.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to generate the row filter string of a DataSet

Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		27/Apr/2006 Created by Yuan Chen

*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using MSRA.SpringField.Components.Enumerations;

namespace MSRA.SpringField.Components
{
    /// <summary>
    /// Class used to generate the row filter string of a DataSet
    /// </summary>
    public class FilterGenerator
    {
        private const string AND = " AND ";
        public Hashtable filterTable = new Hashtable();

        /// <summary>
        /// Method used to generate filter expression string from the properties
        /// </summary>
        /// <returns>Filter expression</returns>
        public string BuildFilterExpression()
        {
            StringBuilder strBuilder = new StringBuilder();
            string temp;
            int count = 0;
            foreach (object obj in filterTable.Values)
            {
                temp = obj as string;
                if (temp != string.Empty)
                {
                    if (count == 0)
                    {
                        strBuilder.Append(temp);
                    }
                    else 
                    {
                        strBuilder.Append(AND);
                        strBuilder.Append(temp);
                    }
                    count++;
                }
                temp = string.Empty;
            }

            return strBuilder.ToString();
        }

        private bool ValidateDateTime(object obj)
        {
            try
            {
                Convert.ToDateTime(obj);
            }
            catch
            {
                return false;
            }
            return true;
        }

        #region Properties
        public string Status
        {
            set {
                if (value == string.Empty)
                {
                    filterTable["status"] = string.Empty;
                }
                else
                {
                    Int32 status = Convert.ToInt32(value);
                    filterTable["status"] = ConstructStatusFilter(status);
                }
            }
        }
        public String ConstructStatusFilter(Int32 status)
        {
            String result = "";
            if (status == 1)//Available
            {
                result = String.Format("(Status ={0})", Convert.ToInt32(ApplicationStatusEnum.Available));
            }
            else if (status == 2)//Waiting For Interview Feedback
            {
                result = String.Format("(InterviewStatus ={0})", Convert.ToInt32(InterviewStatusEnum.WaitingForInterviewFeedback));
            }
            else if (status == 3)//Wating For Mentor Decision
            {
                result = String.Format("(InterviewStatus ={0})", Convert.ToInt32(InterviewStatusEnum.WaitingForMentorDecision));
            }
            else if (status == 4)//Waiting For Group Manager Decision
            {
                result = String.Format("(InterviewStatus ={0})", Convert.ToInt32(InterviewStatusEnum.WaitingForGroupManagerDecision));
            }
            else if (status == 5)//Hired
            {
                result = String.Format("(Status ={0})", Convert.ToInt32(ApplicationStatusEnum.Hired));
            }

            /*
             * Add by Yuanqin, 2011.5.5
             * Add new status
             */
            else if (status == 6)//QualifiedButNotMatched
            {
                result = String.Format("(Status ={0})", Convert.ToInt32(ApplicationStatusEnum.QualifiedButNotMatched));
            }

            else if (status == 7)//Rejected
            {
                result = String.Format("(Status ={0})", Convert.ToInt32(ApplicationStatusEnum.Rejected));
            }
            else if (status == 8)//Decline Offer
            {
                result = String.Format("(Status ={0})", Convert.ToInt32(ApplicationStatusEnum.OfferDeclined));
            }
            else if (status == 9)//On Board
            {
                result = String.Format("(Status ={0})", Convert.ToInt32(ApplicationStatusEnum.OnBoard));
            }
            return result;
        }
        public string InterestedGroup
        {
            set {
                if (value == string.Empty)
                {
                    filterTable["interested_group"] = string.Empty;
                }
                else
                {
                    filterTable["interested_group"] = "(InterestedGroup Like '%" + value + "%')";
                }
            }
        }

        public string MajorCategory
        {
            set {
                if (value == string.Empty)
                {
                    filterTable["major"] = string.Empty;
                }
                else
                {
                    filterTable["major"] = "(MajorCategory = " + value + ")";
                }
            }
        }

        public string BeginDate
        {
            set {
                if (value == string.Empty)
                {
                    filterTable["begin_date"] = string.Empty;
                }
                else
                {
                    if (!ValidateDateTime(value))
                    {
                        filterTable["begin_date"] = string.Empty;
                    }
                    else
                    {
                        //Modify by Yuanqin,2011.1.24
                        //filterTable["begin_date"] = "(PreferredStartDate > #" + value + "# OR SecondaryStartDate > #" + value + "#)";
                        filterTable["begin_date"] = "(PreferredStartDate <= #" + value + "# and SecondaryStartDate >= #" + value + "#)"; //PreferredStartDate
                    }
                }
            }
        }

        public string Area
        {
            set {
                if (value == string.Empty)
                {
                    filterTable["area"] = string.Empty;
                }
                else
                {
                    filterTable["area"] = "(InterestedAreas Like '%" + value.ToLower() + "%')";
                }
            }
        }

        public string Degree
        {
            set {
                if (value == string.Empty)
                {
                    filterTable["degree"] = string.Empty;
                }
                else
                {
                    filterTable["degree"] = "(Degree = " + value + ")";
                }
            }
        }

        public string ApplyStartDate
        {
            set {
                if (value == string.Empty)
                {
                    filterTable["apply_start_date"] = string.Empty;
                }
                else
                {
                    if (ValidateDateTime(value))
                    {
                        filterTable["apply_start_date"] = "(ApplicationDate >= #" + value + "#)";
                    }
                    else
                    { 
                        filterTable["apply_start_date"] = string.Empty;
                    }
                }
            }
        }

        public string ApplyEndDate
        { 
            set {
                if (value == string.Empty)
                {
                    filterTable["apply_end_date"] = string.Empty;
                }
                else
                {
                    if (ValidateDateTime(value))
                    {
                        filterTable["apply_end_date"] = "(ApplicationDate <= #" + value + "#)";
                    }
                    else
                    { 
                        filterTable["apply_end_date"] = string.Empty;
                    }
                }
            }
        }

        public String University
        {
            set
            {
                if (value == string.Empty)
                {
                    filterTable["university"] = string.Empty;
                }
                else
                {
                    filterTable["university"] = "(HighestEducationalInstitution='" + value + "')";
                }
            }
        }

        public String FullName
        {
            set
            {
                if (value == string.Empty)
                {
                    filterTable["fullname"] = string.Empty;
                }
                else
                {
                    filterTable["fullname"] = "(FullName like '%" + value + "%')";
                }
            }
        }

        /*
         * Add for TimeSpan
         * Author: Yuanqin
         * Date: 2011.3.15
         */
        public string TimeSpan
        {
            set
            {
                if (value == string.Empty)
                {
                    filterTable["timespan"] = string.Empty;
                }
                else
                {
                    Int32 timespan = Convert.ToInt32(value);
                    filterTable["timespan"] = ConstructTimeSpanFilter(timespan);
                }
            }
        }
        public String ConstructTimeSpanFilter(Int32 timespan)
        {
            String result = "";
            if (timespan == 1)  //0-3M
            {
                result = String.Format("TimeSpan <= 90");
            }
            else if (timespan == 2)   //3-6M
            {
                result = String.Format("TimeSpan > 90 and TimeSpan <= 180");
            }
            else if (timespan == 3)   //6-12M
            {
                result = String.Format("TimeSpan > 180 and TimeSpan <= 360");
            }
            else if (timespan == 4)   //One year above
            {
                result = String.Format("TimeSpan > 360");
            }
            return result;
        }

        /*
         * Add by Yuanqin
         * 2011.422
         */
        public string Position
        {
            set
            {
                if (value == string.Empty)
                {
                    filterTable["position"] = string.Empty;
                }
                else
                {
                    filterTable["position"] = "(PreferredPosition = " + value + ")";
                }
            }
        }

        #endregion
    }
}
