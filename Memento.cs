namespace XML_Serialization;

public class Memento {
  public string _content { get; set; }
  public Memento(string content) => _content = content;
}