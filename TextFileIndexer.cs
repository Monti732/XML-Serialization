namespace XML_Serialization;

public class TextFileIndexer {
  private Dictionary<string, List<string>> index = new Dictionary<string, List<string>>();

  public void IndexDirectory(string directory) {
    foreach (var file in Directory.GetFiles(directory, "*.txt")) {
      string content = File.ReadAllText(file);
      foreach (var word in content.Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
        string keyword = word.ToLower();
        if (!index.ContainsKey(keyword)) {
          index[keyword] = new List<string>();
        }

        if (!index[keyword].Contains(file)) {
          index[keyword].Add((file));
        }
      }
    }
  }

  public List<string> Search(string keyword) {
    keyword = keyword.ToLower();
    return index.ContainsKey(keyword) ? index[keyword] : new List<string>();
  }
}