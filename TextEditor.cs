namespace XML_Serialization;

public class TextEditor {
  private List<string> _lines;
  private Stack<Memento> _history;
  private int _cursorX = 0, _cursorY = 0;
  private string _filePath;

  public TextEditor(string filePath) {
    _filePath = filePath;
    _lines = new List<string>(File.Exists(filePath) ? File.ReadAllLines(filePath) : new string[] { "" });
    _history = new Stack<Memento>();
    SaveState();
  }

  public void Run() {
    Console.Clear();
    DrawText();
    ConsoleKeyInfo key;

    while (true) {
      key = Console.ReadKey(true);

      if (key.Key == ConsoleKey.Escape) break;
      //ctrl+s or ctrl+shift+s has a conflict with win console, so it doesnt work(cool)
      if (key.Key == ConsoleKey.D && key.Modifiers == ConsoleModifiers.Control) { 
        SaveToFile();
        continue;
      }

      switch (key.Key) {
      case ConsoleKey.UpArrow: MoveCursor(0, -1); break;
      case ConsoleKey.DownArrow: MoveCursor(0, 1); break;
      case ConsoleKey.LeftArrow: MoveCursor(-1, 0); break;
      case ConsoleKey.RightArrow: MoveCursor(1, 0); break;
      case ConsoleKey.Backspace: DeleteCharacter(); break;
      case ConsoleKey.Delete: DeleteCharacter(true); break;
      case ConsoleKey.Enter: InsertNewLine(); break;
      /*doesn't work in rider console(wtf?), but in external works
     Cannot Undo
     Following files affected by this action have been already changed*/
      case ConsoleKey.Z when key.Modifiers == ConsoleModifiers.Control: Undo(); break;
      case ConsoleKey.Q when key.Modifiers == ConsoleModifiers.Control: return;
      default: InsertCharacter(key.KeyChar); break;
      }
    }
  }

  private void DrawText() {
    Console.Clear();
    foreach (string line in _lines)
      Console.WriteLine(line);
    SetCursor();
  }

  private void MoveCursor(int dx, int dy) {
    _cursorY = Math.Clamp(_cursorY + dy, 0, _lines.Count - 1);
    _cursorX = Math.Clamp(_cursorX + dx, 0, _lines[_cursorY].Length);
    SetCursor();
  }

  private void InsertCharacter(char ch) {
    if (!char.IsControl(ch)) {
      SaveState();
      _lines[_cursorY] = _lines[_cursorY].Insert(_cursorX, ch.ToString());
      _cursorX++;
      DrawText();
    }
  }

  private void DeleteCharacter(bool isDeleteKey = false) {
    if (_cursorX > 0 && !isDeleteKey) {
      SaveState();
      _lines[_cursorY] = _lines[_cursorY].Remove(_cursorX - 1, 1);
      _cursorX--;
    }
    else if (isDeleteKey && _cursorX < _lines[_cursorY].Length) {
      SaveState();
      _lines[_cursorY] = _lines[_cursorY].Remove(_cursorX, 1);
    }

    DrawText();
  }

  private void InsertNewLine() {
    SaveState();
    string remainingText = _lines[_cursorY].Substring(_cursorX);
    _lines[_cursorY] = _lines[_cursorY].Substring(0, _cursorX);
    _lines.Insert(_cursorY + 1, remainingText);
    _cursorX = 0;
    _cursorY++;
    DrawText();
  }

  private void Undo() {
    if (_history.Count > 1) {
      _history.Pop();
      _lines = new List<string>(_history.Peek().Content.Split('\n'));
      DrawText();
    }
  }

  private void SaveState() {
    _history.Push(new Memento(string.Join("\n", _lines)));
  }

  private void SaveToFile() {
    File.WriteAllLines(_filePath, _lines);
    Console.SetCursorPosition(0, _lines.Count);
    Console.WriteLine("\nSaved");
    Thread.Sleep(1000);
    DrawText();
  }

  private void SetCursor() {
    Console.SetCursorPosition(_cursorX, _cursorY);
  }
}