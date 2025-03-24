namespace XML_Serialization;

public class TextEditor {
  private string _filePath;
  private string _content;
  private Stack<Memento> _history = new Stack<Memento>();

  public TextEditor(string path) {
    _filePath = path;
    _content = File.Exists(_filePath) ? File.ReadAllText(_filePath) : string.Empty;
  }

  public void Write(string newText) {
    _history.Push(new Memento(newText));
    _content = newText;
  }

  public void Undo() {
    if (_history.Count > 0) {
      _content = _history.Pop()._content;
    }
  }

  public void Save() => File.WriteAllText(_filePath, _content);
  public string GetContent() => _content;
}