using TextEngine;

var tileSet = TileGenrator.Create(16, 32);
tileSet.DisplayTileSet();

// var display = new Display(16, 32, '#');
// var mapGenerator = new MapGenerator('.', '#');
// var player = Player.CreateAndInit(display);
// mapGenerator.Generate(display, 200);

// player.PlacePlayer(display, '.');

// ConsoleKeyInfo key;
// while (true)
// {
//     display.DisplayContents();
//     key = Console.ReadKey();

//     if (key.Key == ConsoleKey.Escape) break;
// }