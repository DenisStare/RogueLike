using System.Runtime.InteropServices;

namespace TeXt;

public class MapGenerator
{
    readonly char _floorTile;
    readonly char _wallTile;

    public MapGenerator(char floorTile, char wallTile) 
    { 
        _floorTile = floorTile; 
        _wallTile = wallTile;
    }

    public void Generate(Display display, int walks, int wallOffset = 2, bool dev_mode = false)
    {
        FirstStep(display, walks, wallOffset, dev_mode);
        // SecondStep(display, dev_mode, wallOffset);
    }

    void FirstStep(Display display, int walks, int wallOffset = 2, bool dev_mode = false)
    {
        int randomX = 0;
        int randomY = 0;

        var generator = new Random();
        var screenSize = display.GetDisplaySize();

        var genarateRandomPos = () => {
            randomX = generator.Next(wallOffset, screenSize.width - wallOffset - 1);
            randomY = generator.Next(wallOffset, screenSize.height - wallOffset - 1);
        };
        
        foreach (int _ in Enumerable.Range(0, walks))
        {
            List<(int x, int y)> locations = display.GetMatchingTile((randomX, randomY), wallOffset, _wallTile); 
            if (locations.Count == 0 || display.GetTile(locations[0].x, locations[0].y) == _floorTile)
            {
                genarateRandomPos();
                continue;
            }

            display.SetTile(randomX, randomY, _floorTile);
            
            generator.Shuffle(CollectionsMarshal.AsSpan(locations));

            var new_direction = locations[0];
        

            randomX = new_direction.x;
            randomY = new_direction.y;

          
        }
    }

    /// <summary>
    ///  Using Cellular Automata altgoritm
    /// </summary>
    /// <param name="display"></param>
    /// <param name="dev_mode"></param>
    /// <param name="wallOffset"></param>
    /// <remarks> Unused it makes ending paths </remarks>
    void SecondStep(Display display, bool dev_mode = false, int wallOffset = 2)
    {
        var display_size = display.GetDisplaySize();
        var tile = new char();
        const int min_floor_maches = 4;
        const int max_wall_maches = 8;

        foreach (int row in Enumerable.Range(wallOffset, display_size.width - wallOffset))
        {
            foreach (int col in Enumerable.Range(wallOffset, display_size.height - wallOffset))
            {
                tile = display.GetTile(row, col);
                if (tile == _wallTile)
                {
                    var mathingTiles = display.GetMatchingTile((row, col), wallOffset, _wallTile);
                    if (mathingTiles.Count >= max_wall_maches)
                    {
                        display.SetTile(row, col, _floorTile);
                    }
                }

                if (tile == _floorTile)
                {
                    var mathingTiles = display.GetMatchingTile((row, col), wallOffset, _floorTile);
                    if (mathingTiles.Count < min_floor_maches)
                    {
                        display.SetTile(row, col, _wallTile);
                    }
                }

                if (dev_mode)
                {
                    display.DisplayContents();
                    Thread.Sleep(5);
                }
            }
        }
    }
}