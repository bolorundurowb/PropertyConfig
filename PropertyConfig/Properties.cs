using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace PropertyConfig;

/// <summary>
/// Class that holds non-nested configuration details
/// </summary>
public class Properties : NameValueCollection
{
    /// <summary>
    /// The file path to save the config to. It defaults to "config.xml"
    /// </summary>
    public string FilePath { get; set; } = "config.xml";

    /// <summary>
    /// Loads the config from the path stored in the FilePath property
    /// </summary>
    public void LoadFromXml() => LoadFromXml(FilePath);

    /// <summary>
    /// Loads the config from the specified filePath
    /// </summary>
    /// <param name="filePath">The path to the config xml file</param>
    /// <exception cref="FileNotFoundException">The file at the path specified does not exist</exception>
    public void LoadFromXml(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("The given file doesn't exist.");

        var xmlDocument = new XmlDocument();
        using var stream = new FileStream(filePath, FileMode.Open);
        xmlDocument.Load(stream);

        if (xmlDocument.DocumentElement == null)
            return;

        foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes[0])
            this[node.Name] = node.InnerText;
    }

    /// <summary>
    /// Writes the current config to the path stored in the "FilePath" property
    /// </summary>
    public void StoreToXml() => StoreToXml(FilePath);

    /// <summary>
    /// Writes the config to the supplied file path
    /// </summary>
    /// <param name="filePath">The specified path to save the config to</param>
    public void StoreToXml(string filePath)
    {
        var comment = $"Created by property config, version: {Constants.LibVersion}";
        StoreToXml(filePath, comment);
    }

    /// <summary>
    /// Writes the config to the supplied path
    /// </summary>
    /// <param name="filePath">The specified path to save the config to</param>
    /// <param name="comment">Any comment you want to insert into the config file</param>
    public void StoreToXml(string filePath, string comment)
    {
        var xmlDocument = new XmlDocument();
        var root = xmlDocument.CreateElement("config");

        // Insert Comment
        var xmlComment = xmlDocument.CreateComment(comment);
        root.AppendChild(xmlComment);

        var allKeys = AllKeys;
        foreach (var key in allKeys)
        {
            var configItem = xmlDocument.CreateElement(key);
            configItem.InnerText = this[key];
            root.AppendChild(configItem);
        }

        xmlDocument.AppendChild(root);

        // check for existence of file, delete if it exists
        if (File.Exists(filePath))
            File.Delete(filePath);

        // write the config
        using var stream = File.OpenWrite(filePath);
        xmlDocument.Save(stream);
    }

    /// <summary>
    /// Retrieve the value at a particular key
    /// </summary>
    /// <param name="key">A string key</param>
    /// <returns>The corresponding value</returns>
    public string GetProperty(string key) { return this[key]; }

    /// <summary>
    /// Retrieve the value at a particular key or return a default
    /// </summary>
    /// <param name="key">A string key</param>
    /// <param name="defaultValue">The value to be returned  if the value is null</param>
    /// <returns>The correspnding value or default</returns>
    public string GetProperty(string key, string defaultValue)
    {
        var value = this[key];
        return value ?? defaultValue;
    }

    /// <summary>
    /// Retrieve all the property keys
    /// </summary>
    /// <returns>An IEnumerable containing all the keys</returns>
    public IEnumerable<string> PropertyNames() => AllKeys;

    /// <summary>
    /// Writes a value to a particular key
    /// </summary>
    /// <param name="key">A string key</param>
    /// <param name="value">A string value</param>
    public void SetProperty(string key, string value) => this[key] = value;
}
