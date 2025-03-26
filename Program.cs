namespace XML_Serialization;

class Program {
  static void Main() {
    var menuItems = new MenuItems();
    var mainMenu = new MainMenu(menuItems.mainMenuItems);
    mainMenu.Show();
  }
}