using System.Text;

var GAME_SIZE = (height: 20, width: 64);

var map = new Map(GAME_SIZE.height, GAME_SIZE.width);

while (true)
{
    var display = map.GetDisplay();
    Console.Write(display.ToString());
    Console.SetCursorPosition(0, 0);
}

class Map
{
    char[,] grid;

    public Map(int height, int width)
    {
        grid = new char[height, width];
        grid.Initialize();
    }

    public StringBuilder GetDisplay()
    {
        var display = new StringBuilder();
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int colum = 0; colum < grid.GetLength(1); colum++)
            {
                if (grid[row, colum] == '\0')
                {
                    display.Append("#");
                }
                else
                {
                    display.Append(grid[row, colum]);
                }
            }
            display.Append('\n');
        }
        return display;
    } 
}