
using System.Text;

namespace TeXt;

public class Display
{
    private readonly int _height;
    private readonly int _width;
    private readonly char[,] _display;

    public Display(int height, int width)
    {
        _display = new char[height, width];
        _height = height;
        _width = width;
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
}