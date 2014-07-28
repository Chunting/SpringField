/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Advisor.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store Advisor information

Remarks:
        This class should be used out of the MSRA.SpringField.Components.BizObjects assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        MSRA.SpringField.Components.BizObjects for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		30/Apr/2006 Created by Yuan Chen

*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace MSRA.SpringField.Components.BizObjects
{
    [Serializable]
    public class Advisor
    {
        private Guid applicantId;
        private string firstName;
        private string lastName;
        private string fullName;
        private string email;
        private string organization;

        public Advisor()
        {
            applicantId = Guid.Empty;
            firstName = string.Empty;
            lastName = string.Empty;
            fullName = string.Empty;
            email = string.Empty;
            organization = string.Empty;
        }

        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = ((value == null) ? string.Empty : value); }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = ((value == null) ? string.Empty : value); }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = ((value == null) ? string.Empty : value); }
        }

        public string Email
        {
            get { return email; }
            set { email = ((value == null) ? string.Empty : value); }
        }

        public string Organization
        {
            get { return organization; }
            set { organization = ((value == null) ? string.Empty : value); }
        }
    }
}