/*****************************************************************************
 * entity class for data table InternPublication
 * added by Yi Shao at 2009-06-19
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public class InternPublication
    {
        #region Private Properties
        private System.Guid _publicationId = Guid.Empty;
        private System.Guid _applicantId = Guid.Empty;
        private System.Guid _pAId = Guid.Empty;
        private string _name = String.Empty;
        private int _currentStatus;
        private DateTime _summitDate = Convert.ToDateTime("12/30/9999");
        private DateTime _modifyDate = Convert.ToDateTime("12/30/9999");
        #endregion

        #region Public Properties
        public System.Guid PublicationId
        {
            get { return _publicationId; }
            set { _publicationId = value; }
        }
        public System.Guid ApplicantId
        {
            get { return _applicantId; }
            set { _applicantId = value; }
        }
        public System.Guid PAId
        {
            get { return _pAId; }
            set { _pAId = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int CurrentStatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; }
        }
        public DateTime SummitDate
        {
            get { return _summitDate; }
            set { _summitDate = value; }
        }
        public DateTime ModifyDate
        {
            get { return _modifyDate; }
            set { _modifyDate = value; }
        }
        #endregion

        #region Constructor
        public InternPublication()
        {
        }
        #endregion

        #region Public Method
        public bool Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.UpdateInternPublication(this);
        }
        public Guid Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.InsertInternPublication(this);
        }
        public static DataSet GetInternPublicationByPAId(Guid PAId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetInternPublicationByPAId(PAId);
        }
        public static bool DeleteInternPublicationById(Guid PublicationId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.DeleteInternPublicationById(PublicationId);
        }

        public static InternPublication GetInternPublicationById(Guid PublicationId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetInternPublicationById(PublicationId);
        }
        #endregion
    }
}
