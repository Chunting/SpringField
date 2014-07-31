/*****************************************************************************

Microsoft Research Asia
Copyright (c) 2006 Microsoft Corporation

Module Name: 
		Favorite.cs

Author:
        Yuan Chen (f-yuchen@msrchina.research.microsoft.com)  
        MSRA/MS^2.3/Compass Team
 
Abstract:
        Class used to store favorite information for SiteUsers
 
Remarks:
        This class should be used out of the Springfield.Components assembly, so it should be declared as an public class.
             
Environment:
		Class Library/.NET Framework 2.0 

Project:
        Springfield.Components for MSRA Intern Application Tracking System (MIATS), codename: Springfield;
        MSRA/MS^2.3/Compass Team

Revision History:			
		30/Apr/2006 Created by Yuan Chen

*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MSRA.SpringField.Components.Configuration;

namespace MSRA.SpringField.Components.BizObjects
{
    public class Favorite
    {
        #region Private Members
        private int favoriteId;
        private Guid ownerId;
        private Guid applicantId; 
        #endregion

        #region Constructors
        public Favorite()
        {
            favoriteId = 0;
            ownerId = Guid.Empty;
            applicantId = Guid.Empty;
        }

        //public Favorite(int id)
        //{

        //} 
        #endregion

        #region Methods
        public void Insert()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            favoriteId = dp.InsertFavorite(this);
        }

        public void Update()
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.UpdateFavorite(this);
        } 
        #endregion

        #region Static Helper Methods
        public static void DeleteFavoriteById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteFavorite(id);
        }

        public static DataSet GetUserFavorite(Guid userId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetFavoritesByUserId(userId);
        }

        public static Favorite GetFavoriteById(int id)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.GetFavoriteById(id);
        }

        public static bool IsFavoriteExists(Guid ownerId, Guid applicantId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            return dp.IsFavoriteExist(ownerId,applicantId);
        }

        public static void DeleteFavorite(Guid ownerId, Guid applicantId)
        {
            IDataProvider dp = DataProviderFactory.GetDataProvider();
            dp.DeleteFavorite(ownerId, applicantId);
        }
        #endregion

        #region Properties
        public int FavoriteId
        {
            get { return favoriteId; }
            set { favoriteId = value; }
        }

        public Guid OwnerId
        {
            get { return ownerId; }
            set { ownerId = value; }
        }

        public Guid ApplicantId
        {
            get { return applicantId; }
            set { applicantId = value; }
        } 
        #endregion
    }
}
