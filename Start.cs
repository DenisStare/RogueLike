using TeXt;

var display = new Display(16, 32, '#');
var mapGenerator = new MapGenerator('.', '#');
mapGenerator.Generate(display, 200);

ConsoleKeyInfo key;
while (true)
{
    display.DisplayContents();
    key = Console.ReadKey();

    if (key.Key == ConsoleKey.Escape) break;
}