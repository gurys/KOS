using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace KosMail
{
    public class Config
    {
        string _fileName;
        XmlDocument _doc = new XmlDocument();
        XmlNode _root;
        public class Server
        {
            public string Address { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
        }

        public Config(string appPath)
        {
            _fileName = appPath + "\\KosMail.xml";
            if (!File.Exists(_fileName))
            {
                _doc.LoadXml("<KosMail></KosMail>");
                _root = _doc.DocumentElement;
                XmlElement elem = _doc.CreateElement("pop3");
                XmlNode pop3 = _root.AppendChild(elem);
                XmlElement first = _doc.CreateElement("server");
                first.Attributes.Append(_doc.CreateAttribute("address"));
                first.Attributes["address"].Value = string.Empty;
                first.Attributes.Append(_doc.CreateAttribute("user"));
                first.Attributes["user"].Value = string.Empty;
                first.Attributes.Append(_doc.CreateAttribute("password"));
                first.Attributes["password"].Value = string.Empty;
                pop3.AppendChild(first);

                return;
            }
            XmlTextReader reader = new XmlTextReader(_fileName);
            _doc.Load(reader);
            _root = _doc["KosMail"];
            reader.Close();
        }

        public List<Server> GetPop3()
        {
            List<Server> list = new List<Server>();
            XmlElement elem = _root["pop3"];
            if (elem.HasChildNodes)
                for (int i=0; i<elem.ChildNodes.Count; i++)
                {
                    XmlNode node = elem.ChildNodes[i];
                    if (!string.IsNullOrEmpty(node.Attributes["address"].Value))
                        list.Add(new Server()
                        {
                            Address = node.Attributes["address"].Value,
                            User = node.Attributes["user"].Value,
                            Password = node.Attributes["password"].Value
                        });
                }
            return list;
        }

        public void SetPop3(List<Server> list)
        {
            XmlNode node = _root["pop3"];
            node.RemoveAll();
            foreach (Server srv in list)
            {
                XmlElement first = _doc.CreateElement("server");
                first.Attributes.Append(_doc.CreateAttribute("address"));
                first.Attributes["address"].Value = srv.Address;
                first.Attributes.Append(_doc.CreateAttribute("user"));
                first.Attributes["user"].Value = srv.User;
                first.Attributes.Append(_doc.CreateAttribute("password"));
                first.Attributes["password"].Value = srv.Password;
                node.AppendChild(first);
            }
        }

        public List<Server> GetSmtp()
        {
            List<Server> list = new List<Server>();
            XmlElement elem = _root["smtp"];
            if (elem!=null && elem.HasChildNodes)
                for (int i = 0; i < elem.ChildNodes.Count; i++)
                {
                    XmlNode node = elem.ChildNodes[i];
                    if (!string.IsNullOrEmpty(node.Attributes["address"].Value))
                        list.Add(new Server()
                        {
                            Address = node.Attributes["address"].Value,
                            User = node.Attributes["user"].Value,
                            Password = node.Attributes["password"].Value
                        });
                }
            return list;
        }

        public void SetSmtp(List<Server> list)
        {
            XmlNode node = _root["smtp"];
            if (node == null)
            {
                XmlElement elem = _doc.CreateElement("smtp");
                node = _root.AppendChild(elem);
            }
            node.RemoveAll();
            foreach (Server srv in list)
            {
                XmlElement first = _doc.CreateElement("server");
                first.Attributes.Append(_doc.CreateAttribute("address"));
                first.Attributes["address"].Value = srv.Address;
                first.Attributes.Append(_doc.CreateAttribute("user"));
                first.Attributes["user"].Value = srv.User;
                first.Attributes.Append(_doc.CreateAttribute("password"));
                first.Attributes["password"].Value = srv.Password;
                node.AppendChild(first);
            }
        }

        public void Save()
        {
            if (File.Exists(_fileName))
                File.Delete(_fileName);
            _doc.Save(_fileName);
        }
    }
}
