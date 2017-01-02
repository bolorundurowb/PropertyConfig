using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Xml;

namespace PropertyConfig
{
    /// <summary>
    /// Class that holds non-nested configuration details
    /// </summary>
    public class Configuration : NameValueCollection
    {
        /// <summary>
        /// The file path to save the config to. It defaults to "config.xml"
        /// </summary>
        public string FilePath { get; set; } = "config.xml";

        /// <summary>
        /// Loads the config from the path stored in the FilePath property
        /// </summary>
        public void LoadFromXml()
        {
            LoadFromXml(FilePath);
        }

        /// <summary>
        /// Loads the config from the specified filePath
        /// </summary>
        /// <param name="filePath">The path to the config xml file</param>
        /// <exception cref="FileNotFoundException">The file at the path specified does not exist</exception>
        public void LoadFromXml(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The given file doesn't exist.");
            }
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath);
            if (xmlDocument.DocumentElement == null) return;
            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes[0])
            {
                this[node.Name] = node.InnerText;
            }
        }

        /// <summary>
        /// Writes the current config to the path stored in the "FilePath" property
        /// </summary>
        public void StoreToXml()
        {
            StoreToXml(FilePath);
        }

        /// <summary>
        /// Writes the config to the supplied file path
        /// </summary>
        /// <param name="filePath">The specified path to save the config to</param>
        public void StoreToXml(string filePath)
        {
            const string comment = "Created by Property Config";
            StoreToXml(filePath, comment);
        }

        /// <summary>
        /// Writes the config to the supplied path
        /// </summary>
        /// <param name="filePath">The specified path to save the config to</param>
        /// <param name="comment">Any comment you want to insert into the config file</param>
        public void StoreToXml(string filePath, string comment)
        {
            XmlDocument xmlDocument = new XmlDocument();
            var root = xmlDocument.CreateElement("config");
            // Insert Comment
            var xmlComment = xmlDocument.CreateComment(comment);
            root.AppendChild(xmlComment);
            var allConfigs = AllKeys.Distinct();
            foreach (var pair in allConfigs)
            {
                var configItem = xmlDocument.CreateElement(pair);
				configItem.InnerText = this[pair];
                root.AppendChild(configItem);
            }
            xmlDocument.AppendChild(root);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            xmlDocument.Save(filePath);
        }
    }
}
