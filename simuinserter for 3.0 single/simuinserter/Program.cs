using System;
using System.Collections.Generic;
///using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Text;


namespace insertsamplexml
{
    class Program
    {
        public static SqlConnection connection;
        static void Main(string[] args)
        {
            string xmlContent_old = "";
            decimal propostion = 1.0m;
            Random ran = new Random();
            connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["source"].ConnectionString);
            connection.Open();
            int count = Convert.ToInt32(GetAppConfig("F_SDaqID"));
            DateTime dt = DateTime.ParseExact(GetAppConfig("F_time"), "yyyyMMddHHmmss", null);
            Console.WriteLine("上一次的的ID值为：" + GetAppConfig("F_SDaqID")); 
            Console.WriteLine("上一次的的ID值为：" + GetAppConfig("F_time"));
            try
            {
                Console.WriteLine("上一次的的ID值为：" + GetAppConfig("F_SDaqID"));
                StreamReader sr = new StreamReader("xml.txt", Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToString());
                    xmlContent_old = xmlContent_old + line;
                }
            }
            catch
            {
                Console.WriteLine("file not exists!");
            }

            while (1 > 0)
            {
                DateTime dt_temp = dt.AddMinutes(15);
                DateTime dt_temp2 = dt_temp.AddMinutes(-1);
                dt = dt_temp;
                string xmlContent = xmlContent_old;
                xmlContent = xmlContent.Replace("sequencexxxx", count.ToString());
                xmlContent = xmlContent.Replace("time1xxxx", dt.ToString("yyyyMMddHHmmss"));
                xmlContent = xmlContent.Replace("time2xxxx", dt_temp2.ToString("yyyyMMddHHmmss"));
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlContent);
                XmlNode pXmlNode = xDoc.DocumentElement;
                foreach (XmlNode p in pXmlNode)
                {
                    if (p.Name == "data")
                    {
                        XmlNodeList pXmlNodeList = p.ChildNodes;
                        foreach (XmlNode p2 in pXmlNodeList)
                        {
                            if (p2.Name == "meter")
                            {
                                XmlNodeList pXmlNodeList2 = p2.ChildNodes;
                                foreach (XmlNode p3 in pXmlNodeList2)
                                {
                                    if (p3.Name == "function")
                                    {
                                        try
                                        {
                                            string s = p3.InnerText;
                                            int RandKey = ran.Next(1, 15);
                                            p3.InnerText = Convert.ToString(Convert.ToDecimal(s) * (propostion + count * 0.005m + RandKey/5000));
                                        }
                                        catch
                                        {
                                            Console.WriteLine("修改function内容失败！");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                count = count + 1;

                SqlCommand cmd2 = new SqlCommand("set identity_insert T_DaqDataForEnergy ON", connection);
                cmd2.ExecuteNonQuery();
                //string ss = xDoc.OuterXml;
                string temp_xml = xDoc.OuterXml;
                xmlContent = temp_xml.Replace("xxxbuilding_idxxx", "440300A002");
                InsertSampleData(count, "440300A002", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), xmlContent, 0);

                count = count + 1;
                temp_xml = xDoc.OuterXml;
                xmlContent = temp_xml.Replace("xxxbuilding_idxxx", "440300A003");
                InsertSampleData(count, "440300A003", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), xmlContent, 0);

                count = count + 1;
                temp_xml = xDoc.OuterXml;
                xmlContent = temp_xml.Replace("xxxbuilding_idxxx", "440300A004");
                InsertSampleData(count, "440300A004", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), xmlContent, 0);

                count = count + 1;
                temp_xml = xDoc.OuterXml;
                xmlContent = temp_xml.Replace("xxxbuilding_idxxx", "440300A006");
                InsertSampleData(count, "440300A006", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), xmlContent, 0);

                count = count + 1;
                temp_xml = xDoc.OuterXml;
                xmlContent = temp_xml.Replace("xxxbuilding_idxxx", "440300A007");
                InsertSampleData(count, "440300A007", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), xmlContent, 0);

                count = count + 1;
                temp_xml = xDoc.OuterXml;
                xmlContent = temp_xml.Replace("xxxbuilding_idxxx", "440300A008");
                InsertSampleData(count, "440300A008", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), xmlContent, 0);

                SetAppConfig("F_SDaqID", count.ToString());
                SetAppConfig("F_time", dt_temp.ToString("yyyyMMddHHmmss"));
                Console.WriteLine("下一次的的ID值为：" + count.ToString());
            }

        }


        public static void InsertSampleData(int F_SDaqID, string F_UBuildID, int F_DataType, string F_DaqDatetime,
            string F_DaqData, int F_State)
        {
            try
            {
                string SQL =
                      "insert into T_DaqDataForEnergy(F_SDaqID,F_UBuildID,F_DataType,F_DaqDatetime,F_DaqData,F_State) " +
                      "values(@F_SDaqID,@F_UBuildID,@F_DataType,@F_DaqDatetime,@F_DaqData,@F_State)";
                SqlParameter[] parameters = new SqlParameter[]
                                            {
                                                new SqlParameter("@F_SDaqID", F_SDaqID),
                                                new SqlParameter("@F_UBuildID", F_UBuildID),
                                                new SqlParameter("@F_DataType", F_DataType),
                                                new SqlParameter("@F_DaqDatetime", F_DaqDatetime),
                                                new SqlParameter("@F_DaqData", F_DaqData), 
                                                new SqlParameter("@F_State", F_State)
                                            };

                using (SqlCommand cmd = new SqlCommand(SQL, connection))
                {
                   // connection.Open();
                    cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("插入样本数据错误：" + "   :" + ex.Message);
            }
        }

        public static void SetAppConfig(string appKey, string appValue)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("data.xml");
            var xNode = xDoc.SelectSingleNode("//mySettings");

            var xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
            if (xElem != null) xElem.SetAttribute("value", appValue);
            else
            {
                var xNewElem = xDoc.CreateElement("add");
                xNewElem.SetAttribute("key", appKey);
                xNewElem.SetAttribute("value", appValue);
                xNode.AppendChild(xNewElem);
            }
            xDoc.Save("data.xml");
        }

        public static string GetAppConfig(string appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load("data.xml");
            }
            catch
            {
                return string.Empty;
            }
            var xNode = xDoc.SelectSingleNode("//mySettings");
            var xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
            if (xElem != null)
            {
                return xElem.Attributes["value"].Value;
            }
            return string.Empty;

        }

    }
}
