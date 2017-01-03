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
        public static decimal propostion = 1.0m;
        public static int count = Convert.ToInt32(GetAppConfig("F_SDaqID"));
        public static int end_count = Convert.ToInt32(GetAppConfig("end"));
        static void Main(string[] args)
        {
            string xmlContent_old = "";
            string xmlContent_old2 = "";
            string xmlContent_old3 = "";
            string xmlContent_old4 = "";
            string xmlContent_old5 = "";
            string xmlContent_old6 = "";
            string xmlContent_old7 = "";
            string xmlContent_old8 = "";
            string xmlContent_old9 = "";
            Random ran = new Random();
            connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["source"].ConnectionString);
            connection.Open();
            DateTime dt = DateTime.ParseExact(GetAppConfig("F_time"), "yyyyMMddHHmmss", null);
            Console.WriteLine("上一次的的ID值为：" + GetAppConfig("F_SDaqID")); 
            Console.WriteLine("上一次的的ID值为：" + GetAppConfig("F_time"));
            //gate1
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

            //gate2
            try
            {
                StreamReader sr = new StreamReader("xml2.txt", Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToString());
                    xmlContent_old2 = xmlContent_old2 + line;
                }
            }
            catch
            {
                Console.WriteLine("file not exists!");
            }

            //gate3
            try
            {
                StreamReader sr = new StreamReader("xml3.txt", Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToString());
                    xmlContent_old3 = xmlContent_old3 + line;
                }
            }
            catch
            {
                Console.WriteLine("file not exists!");
            }

            //gate4
            try
            {
                StreamReader sr = new StreamReader("xml4.txt", Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToString());
                    xmlContent_old4 = xmlContent_old4 + line;
                }
            }
            catch
            {
                Console.WriteLine("file not exists!");
            }


            //gate5
            try
            {
                StreamReader sr = new StreamReader("xml5.txt", Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToString());
                    xmlContent_old5 = xmlContent_old5 + line;
                }
            }
            catch
            {
                Console.WriteLine("file not exists!");
            }

            //gate6
            try
            {
                StreamReader sr = new StreamReader("xml6.txt", Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToString());
                    xmlContent_old6 = xmlContent_old6 + line;
                }
            }
            catch
            {
                Console.WriteLine("file not exists!");
            }



            while (1 > 0)
            {
                //DateTime dt_temp = dt.AddMinutes(15);
                DateTime dt_temp = dt.AddHours(1);
                DateTime dt_temp2 = dt_temp.AddMinutes(-1);
                dt = dt_temp;
                string xmlContent = xmlContent_old;
                string xmlContent2 = xmlContent_old2;
                string xmlContent3 = xmlContent_old3;
                string xmlContent4 = xmlContent_old4;
                string xmlContent5 = xmlContent_old5;
                string xmlContent6 = xmlContent_old6;

                xmlContent = xmlContent.Replace("sequencexxxx", count.ToString());
                xmlContent = xmlContent.Replace("time1xxxx", dt.ToString("yyyyMMddHHmmss"));
                xmlContent = xmlContent.Replace("time2xxxx", dt_temp2.ToString("yyyyMMddHHmmss"));

                xmlContent2 = xmlContent2.Replace("sequencexxxx", count.ToString());
                xmlContent2 = xmlContent2.Replace("time1xxxx", dt.ToString("yyyyMMddHHmmss"));
                xmlContent2 = xmlContent2.Replace("time2xxxx", dt_temp2.ToString("yyyyMMddHHmmss"));

                xmlContent3 = xmlContent3.Replace("sequencexxxx", count.ToString());
                xmlContent3 = xmlContent3.Replace("time1xxxx", dt.ToString("yyyyMMddHHmmss"));
                xmlContent3 = xmlContent3.Replace("time2xxxx", dt_temp2.ToString("yyyyMMddHHmmss"));

                xmlContent4 = xmlContent4.Replace("sequencexxxx", count.ToString());
                xmlContent4 = xmlContent4.Replace("time1xxxx", dt.ToString("yyyyMMddHHmmss"));
                xmlContent4 = xmlContent4.Replace("time2xxxx", dt_temp2.ToString("yyyyMMddHHmmss"));

                xmlContent5 = xmlContent5.Replace("sequencexxxx", count.ToString());
                xmlContent5 = xmlContent5.Replace("time1xxxx", dt.ToString("yyyyMMddHHmmss"));
                xmlContent5 = xmlContent5.Replace("time2xxxx", dt_temp2.ToString("yyyyMMddHHmmss"));


                xmlContent6 = xmlContent6.Replace("sequencexxxx", count.ToString());
                xmlContent6 = xmlContent6.Replace("time1xxxx", dt.ToString("yyyyMMddHHmmss"));
                xmlContent6 = xmlContent6.Replace("time2xxxx", dt_temp2.ToString("yyyyMMddHHmmss"));



                
                SetAppConfig("F_SDaqID", count.ToString());
                SetAppConfig("F_time", dt_temp.ToString("yyyyMMddHHmmss"));
                SqlCommand cmd2 = new SqlCommand("set identity_insert T_DaqDataForEnergy ON", connection);
                cmd2.ExecuteNonQuery();

                count = count + 1;
                string ss = processXML(xmlContent,count);
                if ( ss != "")
                {
                    InsertSampleData(count, "440300G079", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), ss, 0);
                }

                count = count + 1;
                ss = processXML(xmlContent2,count);
                if (ss != "")
                {
                    InsertSampleData(count, "440300G077", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), ss, 0);
                }

                count = count + 1;
                ss = processXML(xmlContent3, count-1);
                if (ss != "")
                {
                    InsertSampleData(count, "440300A004", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), ss, 0);
                }

                count = count + 1;
                ss = processXML(xmlContent4, count-2);
                if (ss != "")
                {
                    InsertSampleData(count, "440300G070", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), ss, 0);
                }
                count = count + 1;
                ss = processXML(xmlContent5, count-3);
                if (ss != "")
                {
                    InsertSampleData(count, "440300A202", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), ss, 0);
                }

                count = count + 1;
                ss = processXML(xmlContent6, count-4);
                if (ss != "")
                {
                    InsertSampleData(count, "440300G060", 0, dt_temp.ToString("yyyy-MM-dd HH:mm:ss"), ss, 0);
                }


                Console.WriteLine("下一次的的ID值为：" + count.ToString());
                if (count > end_count) {
                    break;
                }
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

        public static string processXML(string xmlContent,int count2) 
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xmlContent);
            XmlNode pXmlNode = xDoc.DocumentElement;
            Random ran = new Random();
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
                                        int RandKey = ran.Next(1, 100);
                                        p3.InnerText = Convert.ToString(Convert.ToDecimal(s) * (propostion + count2 * 0.005m + RandKey / 5000));
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
            return xDoc.OuterXml;
        }
        

    }
}
