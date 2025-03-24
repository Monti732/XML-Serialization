namespace XML_Serialization;

using System.Xml.Serialization;

[Serializable]
public class TextFile {
  public string FilePath { get; set; }
  public string Content { get; set; }

  public TextFile() { }

  public TextFile(string filePath, string content) {
    FilePath = filePath;
    Content = content;
  }

  // Сохранение в XML
  public void SaveToXml(string path) {
    XmlSerializer serializer = new XmlSerializer(typeof(TextFile));
    using (FileStream fileStream = new FileStream(path, FileMode.Create)) {
      serializer.Serialize(fileStream, this);
    }
  }

  // Загрузка из XML
  public static TextFile LoadFromXml(string path) {
    XmlSerializer serializer = new XmlSerializer(typeof(TextFile));
    using (FileStream fs = new FileStream(path, FileMode.Open)) {
      return (TextFile)serializer.Deserialize(fs);
    }
  }
}