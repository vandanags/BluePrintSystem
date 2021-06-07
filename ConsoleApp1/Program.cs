using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\\XML File\\organization xml.txt file Back end test.txt");
            Console.WriteLine("------------------------------Main XML------------------------");
            ReadXML(xmlDoc);
            Console.WriteLine("----------------------Swapped XML----------------------------");
            SwapUnits(xmlDoc);
        }

        public static void ReadXML(XmlDocument xmlDoc)
        {
            DataTable dtEmployee = new DataTable();
            dtEmployee.Columns.Add("Title");
            dtEmployee.Columns.Add("Name");
            dtEmployee.Columns.Add("Unit");
            string title = "";
            string unitName = string.Empty;

            XmlNodeList unitNodes = xmlDoc.GetElementsByTagName("Unit");

            foreach (XmlElement unit in unitNodes)
            {
                unitName = unit.Attributes["Name"].Value;
                XmlNodeList employeeNodes = xmlDoc.GetElementsByTagName("Employee");
                foreach (XmlElement employee in employeeNodes)
                {
                    string employeeName = string.Empty;

                    if (employee.ParentNode.Attributes["Name"].Value == unitName)
                    {
                        employeeName = employee.FirstChild.Value;
                        title = employee.Attributes["Title"].Value;
                        dtEmployee.Rows.Add(title, employeeName, unitName);
                    }
                }
            }
            Console.WriteLine("Title" + "|" + "Name" + "|" + "Unit");
            foreach (DataRow dr in dtEmployee.Rows)
            {                
                Console.WriteLine(dr["Title"] + "|" + dr["Name"].ToString() + "|" + dr["Unit"]);
            }


        }

        public static void SwapUnits(XmlDocument xmlDoc)
        {
            XmlNodeList unitNodes = xmlDoc.GetElementsByTagName("Unit");
            string unitName = string.Empty;
            var a = "";
            var b = "";
            foreach (XmlElement unit in unitNodes)
            {
                unitName = unit.Attributes["Name"].Value;

                a = "Platform Team";

                b = "Maintenance Team";
                //  }
                a = a + b;
                b = a.Remove(a.IndexOf(b));
                a = a.Substring(b.Length);

                if (unit.Attributes["Name"].Value == "Platform Team")
                {
                    unit.Attributes["Name"].Value = a;
                }
                if (unit.Attributes["Name"].Value == "Maintenance Team")
                {
                    unit.Attributes["Name"].Value = b;
                }




            }
            xmlDoc.Save(@"C:\\XML File\\organization xml.txt file Back end test.txt");

            ReadXML(xmlDoc);
        }
    }
}
