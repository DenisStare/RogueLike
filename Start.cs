using System.Text;
using TeXt;

var display = new Display(16, 64);
display.Fill('#');

ConsoleKeyInfo key;
while (true)
{
    display.DisplayContents();
    key = Console.ReadKey();

    if (key.Key == ConsoleKey.Escape) break;
}