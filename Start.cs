using System.Text;
using CoconutEngine;

var GAME_SIZE = (height: 64, width: 20);

var map = new Map(GAME_SIZE.height, GAME_SIZE.width);
map.GenerateMap();

while (true)
{
    map.DisplayMap();
}

