namespace XML_Serialization;

public class ConsoleFileManager {
  public static void ShowDirectory(string directory) {
    string currentDirectory = directory;
    while (true) {
      Console.Clear();
      Console.SetCursorPosition(Console.CursorTop, Console.CursorLeft);
      Console.WriteLine($"Current directory: {currentDirectory}\n");

      var directoryItems = new List<string>();
      directoryItems.Add("..");
      foreach (var dir in Directory.GetDirectories(currentDirectory)) {
        directoryItems.Add($"{Path.GetFileName(dir)}\\");
      }

      foreach (var file in Directory.GetFiles(currentDirectory)) {
        directoryItems.Add(Path.GetFileName(file));
      }

      directoryItems.Add("cancel");
      string[] directoryItemsArray = directoryItems.ToArray();
      int exitItemIndex = directoryItemsArray.Length - 1;
      var menu = new Menu(directoryItemsArray);
      menu.Show();
      int choice = menu.GetSelectedIndex();
      if (choice == 0) {
        currentDirectory = Directory.GetParent(currentDirectory)?.FullName ?? currentDirectory;
      }
      else if (choice == exitItemIndex) {
        return;
      }
      else {
        string targetDirectory = Path.Combine(currentDirectory, directoryItemsArray[choice].Substring(0, directoryItemsArray[choice].Length-1));
        currentDirectory = targetDirectory;
      }
    }
  }
}