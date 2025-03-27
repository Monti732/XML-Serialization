namespace XML_Serialization;

public class Memento {
  public string Content { get; set; }
  public Memento(string content) => Content = content;
}