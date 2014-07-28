/*****************************************************************************
 * Abstract: Manage PA related configuration. most code is same to CheckInFormResourceManager.cs
 * added by Yi Shao at 2009-06-19
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Threading;
using System.Web;
using System.Collections;

namespace MSRA.SpringField.Components
{
    public class PAResourceManager
    {   
        private static ReaderWriterLock rwl = new ReaderWriterLock();
        static string resourcePath = HttpContext.Current.Server.MapPath("~/PAResource.xml");
        public static DataSet GetTypeDataSet(string type)
        {

            DataSet ds = new DataSet();
            ds.Tables.Add("Type");

            ds.Tables[0].Columns.Add("Id");
            ds.Tables[0].Columns.Add("Name");
            ds.Tables[0].Columns.Add("Display");

            rwl.AcquireReaderLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string typePath = String.Format("/PAResource/Type[@name='{0}']/Item", type);


                XmlNodeList itemNodes = xmlDocument.SelectNodes(typePath);

                foreach (XmlNode item in itemNodes)
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["Id"] = item.Attributes["id"].Value;
                    dr["Name"] = item.Attributes["name"].Value;
                    dr["Display"] = item.Attributes["display"].Value;
                    ds.Tables[0].Rows.Add(dr);
                }

                return ds;
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }


        public static void NewItem(string type)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string typePath = String.Format("/PAResource/Type[@name='{0}']/Item", type);


                XmlNodeList itemNodes = xmlDocument.SelectNodes(typePath);
                int idMax = 1;
                foreach (XmlNode item in itemNodes)
                {
                    if (idMax < Convert.ToInt32(item.Attributes["id"].Value))
                    {
                        idMax = Convert.ToInt32(item.Attributes["id"].Value);
                    }
                    //dr["Id"] = item.Attributes["id"].Value;
                    //dr["Name"] = item.Attributes["name"].Value;
                    //dr["Display"] = item.Attributes["display"].Value;
                    //ds.Tables[0].Rows.Add(dr);
                }

                string typeNodePath = String.Format("/PAResource/Type[@name='{0}']", type);
                XmlNode typeNode = xmlDocument.SelectSingleNode(typeNodePath);
                XmlNode itemNode = xmlDocument.CreateNode(XmlNodeType.Element, "Item", "");
                XmlAttribute attrId = xmlDocument.CreateAttribute("id");
                XmlAttribute attrName = xmlDocument.CreateAttribute("name");
                XmlAttribute attrDisplay = xmlDocument.CreateAttribute("display");
                attrName.Value = "_NEW";
                attrId.Value = (idMax + 1).ToString();
                attrDisplay.Value = "false";
                itemNode.Attributes.Append(attrName);
                itemNode.Attributes.Append(attrId);
                itemNode.Attributes.Append(attrDisplay);
                typeNode.AppendChild(itemNode);
                xmlDocument.Save(resourcePath);
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }

        public static void RemoveItem(string type, string id)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string itemNodePath = String.Format("/PAResource/Type[@name='{0}']/Item[@id='{1}']", type, id);
                XmlNode itemNode = xmlDocument.SelectSingleNode(itemNodePath);
                itemNode.ParentNode.RemoveChild(itemNode);
                xmlDocument.Save(resourcePath);
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }


        public static void UpdateItem(string type, string id, string name, string display)
        {
            rwl.AcquireWriterLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string itemNodePath = String.Format("/PAResource/Type[@name='{0}']/Item[@id='{1}']", type, id);
                XmlNode itemNode = xmlDocument.SelectSingleNode(itemNodePath);
                itemNode.Attributes["name"].Value = name;
                itemNode.Attributes["display"].Value = display;
                xmlDocument.Save(resourcePath);
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }

        public static ArrayList GetTypeDisplayItems(string type)
        {
            ArrayList list = new ArrayList();

            rwl.AcquireReaderLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string typePath = String.Format("/PAResource/Type[@name='{0}']/Item[@display='true']",type);


                XmlNodeList itemNodes = xmlDocument.SelectNodes(typePath);

                foreach (XmlNode item in itemNodes)
                {
                    list.Add(item.Attributes["name"].Value);
                }

                return list;
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }

        public static ArrayList GetTypes()
        {
            ArrayList list = new ArrayList();

            rwl.AcquireReaderLock(-1);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(resourcePath);

                string typePath = String.Format("/PAResource/Type");


                XmlNodeList itemNodes = xmlDocument.SelectNodes(typePath);

                foreach (XmlNode item in itemNodes)
                {
                    list.Add(item.Attributes["name"].Value);
                }

                return list;
            }
            finally
            {
                rwl.ReleaseReaderLock();
            }
        }

        public static string IdToText(string type, int id)
        {
            DataSet ds = PAResourceManager.GetTypeDataSet(type);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if ( Convert.ToInt32(dr["id"].ToString()) == id)
                {
                    return dr["name"].ToString();
                }
            }

            return "Unknown";
        }
        public static int TextToId(string type, string text)
        {
            DataSet ds = PAResourceManager.GetTypeDataSet(type);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["name"].ToString() == text)
                {
                    return Convert.ToInt32(dr["id"]);
                }
            }
            throw new Exception(String.Format("Type:{0} Name:{1} is not found!", type, text));
        }
    }
}
