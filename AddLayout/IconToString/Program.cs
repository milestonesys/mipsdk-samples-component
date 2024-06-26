using System;
using System.IO;
using System.Xml;

namespace IconToString
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                Console.WriteLine("Must have two parameters: #1 icon filename #2 layout xml filename");
                return;
            }

            string filename = args[0];
            string filename2 = args[1];
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found:"+filename);
                return;
            }
            if (!File.Exists(filename2))
            {
                Console.WriteLine("File not found:" + filename2);
                return;
            }

            byte[] content = File.ReadAllBytes(filename);
            string base64 = Convert.ToBase64String(content);

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(filename2);
            } catch (XmlException ex)
            {
                Console.WriteLine("Invalid xml:" + ex.Message);
                return;
            }

            XmlNode node = doc.SelectSingleNode("/ViewLayout");
            XmlNode iconNode;
            if (node["ViewLayoutIcon"] != null)
            {
                iconNode = node["ViewLayoutIcon"];
                iconNode.InnerText = base64;
            }
            else
            {
                iconNode = doc.CreateNode(XmlNodeType.Element, "ViewLayoutIcon", "");
                iconNode.InnerText = base64;
                node.AppendChild(iconNode);
            }

            string fileNew = filename2;
            if (filename2.EndsWith(".xml"))
            {
                fileNew = filename2.Substring(0, filename2.Length - 4);
            }
            fileNew += "NEW.xml";
            File.WriteAllText(fileNew, doc.OuterXml);
        }
    }
}
