using System.Text;

namespace TextEngine;

public enum TileType 
{
    Wall = '#',
    Floor = '.'
}


public class TileSet
{
    TileType[,] _tiles = new TileType[0, 0];

    int _height = 0;
    int _width = 0;

    private TileSet() {}
    
    /// <summary>
    ///  Sets the size of the tile grid 
    /// </summary>
    /// <param name="height">The new height</param>
    /// <param name="width">The new width</param>
    /// <returns>true if the height or width are positive else false</returns>
    public bool SetSize(int height, int width)
    {
        if (height < 0) return false;
        if (width < 0) return false;

        _height = height;
        _width = width;

        _tiles = new TileType[_height, _width];
        return true;
    }

    public (int height, int width) GetSize()
    {
        return (_height, _width);
    }

    /// <summary>
    /// Creates a new tile set  
    /// </summary>
    /// <param name="toFill"></param>
    /// <returns>a new tile set</returns>
    public static TileSet Create(int height, int width, TileType toFill)
    {
        var tileSet = new TileSet();
        tileSet.SetSize(height, width);
        tileSet.Fill(toFill);

        return tileSet;
    }

    /// <summary>
    /// Fils the tile set with tileType.
    /// </summary>
    /// <param name="tileType">To fill</param>
    public void Fill(TileType tileType)
    {
        if (_height <= 0 || _width <= 0) throw new ArgumentException("Use SetSize() to use this method.");

        for (int row = 0; row < _height - 1; row++)
        {
            for (int col = 0; col < _width - 1; col++) _tiles[row, col] = tileType;
        }
    }

    public void DisplayTiles()
    {
        if (_height <= 0 || _width <= 0) throw new ArgumentException("Use SetSize() to use this method.");

        var display = new StringBuilder();

        for (int row = 0; row < _height - 1; row++)
        {
            for (int col = 0; col < _width - 1; col++) display.Append((char)_tiles[row, col]);
            display.Append('\n');
        }

        Console.WriteLine(display);
    }
}

public class TileGenrator
{
    TileSet? tileSet;

    private TileGenrator() {}

    public void DisplayTileSet()
    {
        if (tileSet == null) return;

        tileSet.DisplayTiles();
    }

    public static TileGenrator Create(TileSet baseTileSet)
    {
        var tileGenerator = new TileGenrator
        {
            tileSet = baseTileSet
        };

        return tileGenerator;
    }

    public static TileGenrator Create(int height, int width)
    {
        var baseTileSet = TileSet.Create(height, width, TileType.Wall);

        var tileGenerator = new TileGenrator
        {
            tileSet = baseTileSet
        };

        return tileGenerator;
    }

    public void Generate(int iterations)
    {
        var tileSetSize = tileSet!.GetSize();

        var gen = new Random();

        int randomX = gen.Next(0, tileSetSize.width - 1);
        int randomY = gen.Next(0, tileSetSize.height - 1);

        for (int iteration = 0; iteration < iterations; iteration++)
        {
            
        }
    }
}