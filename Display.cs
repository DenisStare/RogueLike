
using System.Text;

using Vec2 = (int x, int y);

namespace TeXt;

public class Display
{
    private readonly int _height;
    private readonly int _width;
    private readonly char[,] _display;

    public Display(int height, int width, char fill = ' ')
    {
        _display = new char[height, width];
        _height = height;
        _width = width;

        Fill(fill);
    }

    public void DisplayContents(bool setCursorDefaultAtStart = false, int miliSecondsToWait = 0)
    {
        if (setCursorDefaultAtStart) Console.SetCursorPosition(0, 0);

        var displayBuilder = new StringBuilder();

        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++) displayBuilder.Append(_display[row, col]);
            displayBuilder.Append("\n");
        }

        Console.Write(displayBuilder);

        Console.SetCursorPosition(0, 0);
        Thread.Sleep(miliSecondsToWait);
    }

    public void Fill(char toFill)
    {
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++) _display[row, col] = toFill;
        }
    }

    public void SetTile(int x, int y, char to)
    {
        if (x < 0 || x > _width) 
            throw new IndexOutOfRangeException("The x position is negative or was exiced the display width."); 
        if (y < 0 || y > _height)
            throw new IndexOutOfRangeException("The y position is negative or was exiced the display height.");

        _display[y, x] = to;
    }

    public (int height, int width) GetDisplaySize()
    {
        return (_height, _width);
    }

    public char GetTile(int x, int y)
    {
        if (x < 0 || x > _width) 
            throw new IndexOutOfRangeException("The x position is negative or was exiced the display width."); 
        if (y < 0 || y > _height)
            throw new IndexOutOfRangeException("The y position is negative or was exiced the display height.");
            
        return _display[y, x];
    }

    public List<Vec2> GetMatchingTile(Vec2 pos, int offset, char tile)
    {
        var tiles = new List<Vec2>();

        Vec2 check_pos;
        var screenSize = GetDisplaySize();
        foreach (int row in Enumerable.Range(-1, 3))
        {
            foreach (int col in Enumerable.Range(-1, 3))
            {
                check_pos = new Vec2(pos.x + col, pos.y + row);

                if (check_pos.x < offset || check_pos.x > screenSize.width - offset - 1) continue;
                if (check_pos.y < offset || check_pos.y > screenSize.height - offset - 1) continue;
                
                char tileChar = GetTile(check_pos.x, check_pos.y);
                if (tileChar == tile)
                    tiles.Add((check_pos.x, check_pos.y));
            }
        }
        return tiles;
    }

}