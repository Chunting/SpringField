using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;
using System.Data;
using System.Web.Caching;
using System.Threading;
using MSRA.SpringField.Components.Configuration;
using MSRA.SpringField.Components.Exceptions;

namespace MSRA.SpringField.Components
{
    public static class ResourceManager
    {
        public const string COUNTRY_LIST_PATH = "/resource/countrylist";
        public const string UNIV_LIST_PATH = "/resource/universities";
        public const string CATEGORY_PATH = "/resource/jobinfocategory";
        public const string SOURCE_PATH = "/resource/jobinfosource";
        public const string CHANNEL_PATH = "/resource/jobinfochannel";
        public const string GROUP_LIST_PATH = "/resource/grouplist";
        public const string DEGREE_LIST_PATH = "/resource/degreelist";
        public const string MSRA_FTE_PATH = "/resource/msrafte";
        private const string VALUE = "value";

        private static string resourcePath = string.Empty;

        private static ReaderWriterLock rwl = new ReaderWriterLock();

        static ResourceManager()
        {
            if (HttpContext.Current != null)
            {
                //web application
                resourcePath = HttpContext.Current.Server.MapPath(SiteConfiguration.GetConfig().SiteAttributes["webResource"]);
            }
            else
            {
                //local application
                resourcePath = SiteConfiguration.GetConfig().SiteAttributes["localResource"];
            }

        }

        /// <summary>
        /// Fetch full name of group by short name of this group
        /// Author: Yin.P
        /// Date: 2010-1-6
        /// </summary>
        /// <param name="shortname"></param>
        /// <returns></returns>
        public static string GetGroupFullNameByShoutName(string shortname)
        {
            rwl.AcquireReaderLock(-1);
            string result = string.Empty;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(resourcePath);
                XmlNode snode = doc.SelectSingleNode(GROUP_LIST_PATH + "/Item[@short=\"" + shortname + "\"]");
                if (snode != null)
                {
                    if (snode.Attributes["value"] != null)
                    {
                        result = snode.Attributes["value"].Value;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }

            return result;
        }

        public static void GetDataFromResource(string xpath)
        {
            rwl.AcquireReaderLock(-1);
            try
            {
                List<string> curList = null;
                string cacheKey = xpath;
                XmlDocument resDoc = new XmlDocument();
                resDoc.Load(resourcePath);
                XmlNode curNode = resDoc.SelectSingleNode(xpath);
                if (curNode != null && curNode.HasChildNodes)
                {
                    if (xpath.Equals(UNIV_LIST_PATH))
                    {
                        curList = new List<string>();
                        foreach (XmlNode continentNode in curNode.ChildNodes)
                        {
                            curList.Add(continentNode.Attributes[0].Value);
                            XmlNodeList countryNodes = continentNode.ChildNodes;
                            if (continentNode.Attributes[0].Value.Equals("Europe") == false)
                            {
                                foreach (XmlNode countryNode in countryNodes)
                                {
                                    curList.Add("--" + countryNode.Attributes[0].Value);
                                    XmlNodeList universityNodes = countryNode.ChildNodes;
                                    foreach (XmlNode universityNode in universityNodes)
                                    {
                                        curList.Add("----" + universityNode.ChildNodes[0].InnerText);
                                    }
                                }
                            }
                            else
                            {
                                XmlNodeList universityNodes = continentNode.ChildNodes;
                                foreach (XmlNode universityNode in universityNodes)
                                {
                                    curList.Add("----" + universityNode.ChildNodes[0].InnerText);
                                }
                            }

                        }
                        curList.Insert(0, StaticData.OTHER_COLLEGE);
                    }
                    else if (xpath.Equals(MSRA_FTE_PATH))
                    {
                        Dictionary<string, Boolean> FTEDict = new Dictionary<string, bool>();
                        foreach (XmlNode item in curNode.ChildNodes)
                        {
                            if (!FTEDict.ContainsKey(item.InnerText))
                            {
                                FTEDict.Add(item.InnerText, true);
                            }
                        }
                        SiteCache.Insert(cacheKey, FTEDict, resourcePath);
                    }
                    else
                    {
                        curList = new List<string>();
                        foreach (XmlNode item in curNode.ChildNodes)
                        {
                            curList.Add(item.Attributes[VALUE].Value);
                        }
                    }
                }

                if (curList != null)
                {
                    if (xpath.Equals(UNIV_LIST_PATH) || xpath.Equals(GROUP_LIST_PATH))
                    {
                        SiteCache.Insert(cacheKey, curList, resourcePath);
                    }
                    else
                    {
                        SiteCache.InsertMax(cacheKey, curList);
                    }
                }
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }

        #region FTE List Management
        public static bool AddAlias(string alias)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                XmlNode rootFTENode = xmlDocument.SelectNodes("/resource/msrafte").Item(0);
                XmlNodeList FTENodes = xmlDocument.SelectNodes("/resource/msrafte/item");

                XmlNode newNode = xmlDocument.CreateNode(XmlNodeType.Element, "item", "");
                newNode.InnerText = alias;

                XmlNode addPosition = null;
                string currentText = "";
                for (int i = 0; i < FTENodes.Count; i++)
                {
                    currentText = FTENodes[i].InnerText;
                    if (alias == currentText)
                    {
                        throw new ResourceAlreadyExist(MSRA_FTE_PATH, alias);
                    }
                    if (alias.CompareTo(currentText) == 1)
                    {
                        addPosition = FTENodes[i];
                    }
                }
                if (addPosition == null)
                {
                    rootFTENode.PrependChild(newNode);
                }
                else
                {
                    rootFTENode.InsertAfter(newNode, addPosition);
                }
                xmlDocument.Save(resourcePath);
                return true;
            }
            catch 
            {
                throw;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
 
        }
        #endregion

        #region Process University

        public static DataSet GetUniversityDataSet()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("University");

            ds.Tables[0].Columns.Add("Continent");
            ds.Tables[0].Columns.Add("Country");
            ds.Tables[0].Columns.Add("University");

            rwl.AcquireReaderLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string universityPath = "/resource/universities/continent/country/university";
                XmlNodeList universityNodes = xmlDocument.SelectNodes(universityPath);

                foreach (XmlNode universityNode in universityNodes)
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["Continent"] = universityNode.ParentNode.ParentNode.Attributes[0].Value;
                    dr["Country"] = universityNode.ParentNode.Attributes[0].Value;
                    dr["University"] = universityNode.ChildNodes[0].InnerText;

                    ds.Tables[0].Rows.Add(dr);
                }

                return ds;
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }
        
        public static DataSet GetUniversityDataSet(string Continent, string Country)
        {
            if (Continent == "All")
            {
                return GetUniversityDataSet();
            }
            DataSet ds = new DataSet();
            ds.Tables.Add("University");

            ds.Tables[0].Columns.Add("Continent");
            ds.Tables[0].Columns.Add("Country");
            ds.Tables[0].Columns.Add("University");

            rwl.AcquireReaderLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string universityPath = string.Format("/resource/universities/continent[@name='{0}']/university", Continent);
                if (Country.Length == 0)
                {
                    universityPath = string.Format("/resource/universities/continent[@name='{0}']/university", Continent);
                }
                else
                {
                    universityPath = string.Format("/resource/universities/continent[@name='{0}']/country[@name='{1}']/university", Continent, Country);
                }
                XmlNodeList universityNodes = xmlDocument.SelectNodes(universityPath);

                if (Country.Length == 0)
                {
                    foreach (XmlNode universityNode in universityNodes)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["Continent"] = universityNode.ParentNode.Attributes[0].Value;
                        //dr["Country"] = universityNode.Attributes[0].Value;
                        dr["University"] = universityNode.InnerText;

                        ds.Tables[0].Rows.Add(dr);
                    }
                }
                else
                {
                    foreach (XmlNode universityNode in universityNodes)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr["Continent"] = universityNode.ParentNode.ParentNode.Attributes[0].Value;
                        dr["Country"] = universityNode.ParentNode.Attributes[0].Value;
                        dr["University"] = universityNode.ChildNodes[0].InnerText;

                        ds.Tables[0].Rows.Add(dr);
                    }
                }

                return ds;
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }
        
        public static List<string> GetCountryData(string Continent)
        {
            List<string> resultList = new List<string>();
            string xPath = string.Format("/resource/universities/continent[@name='{0}']/country", Continent);
            if (Continent == "All")
            {
                resultList.Add("All");
                return resultList;
            }
            resultList = GetData(xPath);
            return resultList;
        }

        public static List<string> GetContinentData()
        {
            string xPath = "/resource/universities/continent";
            List<string> resultList = GetData(xPath);
            resultList.Insert(0, "All");
            return resultList;
        }

        public static bool AddUniversity(string Continent, string Country, string University)
        {
            bool ret = false;
            try
            {
                AddAUniversity(Continent, Country, University);
                ret = true;
            }
            catch (ResourceNotExist e)
            {
                if (e.Category == "Continent")
                {
                    AddAContinent(Continent);
                    AddACountry(Continent, Country);
                    AddAUniversity(Continent, Country, University);
                }
                else if (e.Category == "Country")
                {
                    AddACountry(Continent, Country);
                    AddAUniversity(Continent, Country, University);
                }
                ret = true;
            }
            catch (ResourceAlreadyExist)
            {
                ret = false;
            }

            return ret;
        }
        #endregion

        #region Delete a university
        public static bool DeleteUniversity(string Continent, string Country, string University)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string universityPath = string.Format("/resource/universities/continent[@name='{0}']/country[@name='{1}']//university[name='{2}']", Continent, Country, University);
                XmlNode universityNode = xmlDocument.SelectSingleNode(universityPath);

                if (universityNode == null)
                {
                    return false;
                }

                XmlNode countryNode = universityNode.ParentNode;
                countryNode.RemoveChild(universityNode);
                if (countryNode.ChildNodes.Count == 0)
                {
                    XmlNode contientNode = countryNode.ParentNode;
                    contientNode.RemoveChild(countryNode);
                    if (contientNode.ChildNodes.Count == 0)
                    {
                        XmlNode topNode = contientNode.ParentNode;
                        topNode.RemoveChild(contientNode);
                    }
                }
                xmlDocument.Save(resourcePath);
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }
        #endregion

        #region modify method
        //public static bool ModifyContinent(string oldContinent, string newContinent)
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(xmlPath);

        //    //use xpath
        //    string continentPath = string.Format("//continent[@name='{0}']", oldContinent);
        //    XmlNode continentNode = xmlDocument.SelectNodes(continentPath)[0];
        //    continentNode.Attributes[0].Value = newContinent;
        //    //use xmlnode.
        //    //XmlNodeList continentNodes = xmlDocument.GetElementsByTagName("continent");
        //    //foreach (XmlNode continentNode in continentNodes)
        //    //{
        //    //    if (oldContinent == continentNode.Attributes[0].Value)
        //    //    {
        //    //        continentNode.Attributes[0].Value = newContinent;
        //    //        break;
        //    //    }
        //    //}

        //    xmlDocument.Save(xmlPath);
        //    return true;

        //}

        //public static bool ModifyCountry(string Continent, string oldCountry, string newCountry)
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(xmlPath);

        //    string countryPath = string.Format("//continent[@name='{0}']//country[@name='{1}']", Continent, oldCountry);
        //    XmlNode countryNode = xmlDocument.SelectNodes(countryPath)[0];
        //    countryNode.Attributes[0].Value = newCountry;
        //    //XmlNodeList countryNodes = xmlDocument.GetElementsByTagName("country");
        //    //foreach (XmlNode countryNode in countryNodes)
        //    //{
        //    //    if (oldCountry == countryNode.Attributes[0].Value && countryNode.ParentNode.Attributes[0].Value == Continent)
        //    //    {
        //    //        countryNode.Attributes[0].Value = newCountry;
        //    //        break;
        //    //    }
        //    //}
        //    xmlDocument.Save(xmlPath);
        //    return true;
        //}

        //public static bool ModifyUniversity(string Continent, string Country, string oldUniversity, string newUniversity)
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(xmlPath);

        //    string countryPath = string.Format("//continent[@name='{0}']//country[@name='{1}']//university[name='{2}']", Continent, Country, oldUniversity);
        //    XmlNode countryNode = xmlDocument.SelectNodes(countryPath)[0];
        //    countryNode.ChildNodes[0].InnerText = newUniversity;
        //    //XmlNodeList universityNodes = xmlDocument.GetElementsByTagName("university");
        //    //foreach (XmlNode universityNode in universityNodes)
        //    //{
        //    //    if (oldUniversity == universityNode.ChildNodes[0].InnerText && universityNode.ParentNode.Attributes[0].Value == Country && universityNode.ParentNode.ParentNode.Attributes[0].Value == Continent)
        //    //    {
        //    //        universityNode.ChildNodes[0].InnerText = newUniversity;
        //    //        break;
        //    //    }
        //    //}

        //    xmlDocument.Save(xmlPath);
        //    return true;
        //}
        #endregion

        #region import data from EXCEL file
        //public static void ImportData()
        //{
        //    string connectionString = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + HttpContext.Current.Server.MapPath("CollegeList.xls") + ";Extended Properties=Excel 8.0";
        //    System.Data.OleDb.OleDbConnection dbConnection = new System.Data.OleDb.OleDbConnection(connectionString);
        //    System.Data.OleDb.OleDbDataAdapter dbAdapter = new System.Data.OleDb.OleDbDataAdapter("Select * from [Sheet1$]", dbConnection);
        //    DataSet ds = new DataSet();
        //    dbAdapter.Fill(ds, "Book1");

        //    string Continent = "";
        //    string Country = "";
        //    string University = "";
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        Continent = dr["Area"].ToString().Trim();
        //        Country = dr["Country"].ToString().Trim();
        //        University = dr["University"].ToString().Trim();

        //        AddUniversity(Continent, Country, University);
        //    }
        //}
        #endregion

        #region Private method
        private static bool AddAContinent(string Continent)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                XmlNode univRootNode = xmlDocument.SelectNodes("/resource/universities").Item(0);
                XmlNodeList continentNodes = xmlDocument.SelectNodes("/resource/universities/continent");

                XmlNode newNode = xmlDocument.CreateNode(XmlNodeType.Element, "continent", "");
                XmlAttribute newAttribute = xmlDocument.CreateAttribute("name");
                newAttribute.Value = Continent;
                newNode.Attributes.Append(newAttribute);

                XmlNode addPosition = null;
                string currentText = "";
                for (int i = 0; i < continentNodes.Count; i++)
                {
                    currentText = continentNodes[i].Attributes[0].Value;
                    if (Continent == currentText)
                    {
                        throw new ResourceAlreadyExist(UNIV_LIST_PATH, Continent);
                    }
                    if (Continent.CompareTo(currentText) == 1)
                    {
                        addPosition = continentNodes[i];
                    }
                }
                if (addPosition == null)
                {
                    univRootNode.PrependChild(newNode);
                }
                else
                {
                    univRootNode.InsertAfter(newNode, addPosition);
                }
                xmlDocument.Save(resourcePath);
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }

        private static bool AddACountry(string Continent, string Country)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                XmlNodeList continentNodes = xmlDocument.SelectNodes("/resource/universities/continent");
                XmlNode currentContinent = GetCurrentNode(continentNodes, Continent);
                if (currentContinent == null)
                {
                    throw new ResourceNotExist("Continent", Continent);
                }

                XmlNodeList countryNodes = currentContinent.ChildNodes;

                XmlNode newNode = xmlDocument.CreateNode(XmlNodeType.Element, "country", "");
                XmlAttribute newAttribute = xmlDocument.CreateAttribute("name");
                newAttribute.Value = Country;
                newNode.Attributes.Append(newAttribute);

                XmlNode addPosition = null;
                string currentText = "";
                for (int i = 0; i < countryNodes.Count; i++)
                {
                    currentText = countryNodes[i].Attributes[0].Value;
                    if (Country == currentText)
                    {
                        throw new ResourceAlreadyExist(UNIV_LIST_PATH, Country);
                    }
                    if (Country.CompareTo(currentText) == 1)
                    {
                        addPosition = countryNodes[i];
                    }
                }
                if (addPosition == null)
                {
                    currentContinent.PrependChild(newNode);
                }
                else
                {
                    currentContinent.InsertAfter(newNode, addPosition);
                }

                xmlDocument.Save(resourcePath);
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }

        private static bool AddAUniversity(string Continent, string Country, string University)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                XmlNodeList continentNodes = xmlDocument.SelectNodes("/resource/universities/continent");
                XmlNode currentContinent = GetCurrentNode(continentNodes, Continent);
                if (currentContinent == null)
                {
                    throw new ResourceNotExist("Continent", Continent);
                }

                XmlNodeList countryNodes = currentContinent.ChildNodes;
                XmlNode currentCountry = GetCurrentNode(countryNodes, Country);
                if (currentCountry == null)
                {
                    throw new ResourceNotExist("Country", Country);
                }

                XmlNodeList universityNodes = currentCountry.ChildNodes;
                XmlNode addPosition = null;
                string currentText = "";
                //foreach (XmlNode universityNode in universityNodes)
                for (int i = 0; i < universityNodes.Count; i++)
                {
                    currentText = universityNodes[i].ChildNodes[0].InnerText;
                    if (University == currentText)
                    {
                        throw new ResourceAlreadyExist(UNIV_LIST_PATH, University);
                    }
                    if (University.CompareTo(currentText) == 1)
                    {
                        addPosition = universityNodes[i];
                    }
                }

                XmlNode newNode = xmlDocument.CreateNode(XmlNodeType.Element, "university", "");
                XmlNode nameNode = xmlDocument.CreateNode(XmlNodeType.Element, "name", "");
                nameNode.InnerText = University;
                newNode.AppendChild(nameNode);
                if (addPosition == null)
                {
                    currentCountry.PrependChild(newNode);
                }
                else
                {
                    currentCountry.InsertAfter(newNode, addPosition);
                }
                xmlDocument.Save(resourcePath);
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }

        private static XmlNode GetCurrentNode(XmlNodeList xmlNodeList, string Key)
        {
            XmlNode resultNode = null;
            if (xmlNodeList == null)
            {
                return resultNode;
            }
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (Key == xmlNode.Attributes[0].Value)
                {
                    resultNode = xmlNode;
                    break;
                }
            }
            return resultNode;
        }

        private static List<string> GetData(string xPath)
        {
            List<string> resultList = new List<string>();

            rwl.AcquireReaderLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                XmlNodeList xNodes = xmlDocument.SelectNodes(xPath);

                foreach (XmlNode xNode in xNodes)
                {
                    resultList.Add(xNode.Attributes[0].Value);
                }

                return resultList;
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }
        #endregion


        /// <summary>
        /// Add group and its short name.
        /// </summary>
        /// <param name="Group"></param>
        /// <param name="shortName"></param>
        /// <returns></returns>
        public static bool AddGroup(string Group, string shortName)
        {
            bool ret = false;
            try
            {
                AddAGroup(Group, shortName);
                ret = true;
            }
            catch (ResourceAlreadyExist)
            {
                ret = false;
            }
            return ret;
        }

        private static bool AddAGroup(string Group)
        {
            return AddAGroup(Group, "");
        }

        /// <summary>
        /// Add a group with short name
        /// @Author: Yin.P
        /// @Date: 2009-12-14
        /// </summary>
        /// <param name="Group"></param>
        /// <param name="shortname"></param>
        /// <returns></returns>
        private static bool AddAGroup(string Group, string shortname)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                XmlNode groupNodeRoot = xmlDocument.SelectSingleNode("/resource/grouplist");
                XmlNodeList groupsNodes = groupNodeRoot.ChildNodes;

                XmlNode addPosition = null;
                string currentText = "";
                for (int i = 0; i < groupsNodes.Count; i++)
                {
                    currentText = groupsNodes[i].Attributes["value"].Value;
                    if (Group == currentText)
                    {
                        throw new ResourceAlreadyExist(GROUP_LIST_PATH, Group);
                    }
                    if (Group.CompareTo(currentText) == 1)
                    {
                        addPosition = groupsNodes[i];
                    }
                }

                XmlNode newNode = xmlDocument.CreateNode(XmlNodeType.Element, "Item", "");                
                XmlAttribute nameAttr = xmlDocument.CreateAttribute("value");
                XmlAttribute shortAttr = xmlDocument.CreateAttribute("short");
                nameAttr.Value = Group;
                shortAttr.Value = shortname;

                newNode.Attributes.Append(nameAttr);
                newNode.Attributes.Append(shortAttr);
                if (addPosition == null)
                {
                    groupNodeRoot.PrependChild(newNode);
                }
                else
                {
                    groupNodeRoot.InsertAfter(newNode, addPosition);
                }

                xmlDocument.Save(resourcePath);
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }
        
        public static DataSet GetGroupDataSet()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("Group");

            ds.Tables[0].Columns.Add("Group");

            rwl.AcquireReaderLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string groupPath = "/resource/grouplist/Item";
                XmlNodeList groupNodes = xmlDocument.SelectNodes(groupPath);

                foreach (XmlNode groupNode in groupNodes)
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["Group"] = groupNode.Attributes["value"].Value;
                    ds.Tables[0].Rows.Add(dr);
                }

                return ds;
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Delete the short name of a group when the group is deleting.
        /// </summary>
        /// <param name="Group"></param>
        /// <returns></returns>
        public static bool DeleteGroup(string Group)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);
                string shortName = string.Empty;

                string groupPath = string.Format("/resource/grouplist/Item[@value='{0}']", Group);
                XmlNode groupNode = xmlDocument.SelectSingleNode(groupPath);

                if (groupNode == null)
                {
                    return false;
                }
                else
                {
                    shortName = groupNode.Attributes["short"].Value;
                }

                //delete the short name of current group to keep synchronization between group list and the short name list of the groups.
                CheckInFormResourceManager.RemoveItemByName("Groups", shortName);

                XmlNode groupRootNode = groupNode.ParentNode;
                groupRootNode.RemoveChild(groupNode);
                xmlDocument.Save(resourcePath);
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }

        }
    }

}
