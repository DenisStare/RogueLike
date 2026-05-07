using System.Text;

namespace PathGen;

class Map
{
    char[,] grid;
    (int height, int width) screenSize;

    public Map(int height, int width)
    {
        grid = new char[width, height];
        screenSize.height = height;
        screenSize.width = width;

        grid.Initialize();
        FillMap(grid);
    }

    void FillMap(char[,] arr)
    {
        for (int row = 0; row < arr.GetLength(0); row++)
        {
            for (int colum = 0; colum < arr.GetLength(1); colum++)
            {
                arr[row, colum] = '#';
            }
        }
    }
    public StringBuilder GetDisplay()
    {
        var display = new StringBuilder();
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int colum = 0; colum < grid.GetLength(1); colum++)
            {
                display.Append(grid[row, colum]);
            }
            display.Append('\n');
        }
        return display;
    } 

    public void GenerateMap(bool dev_mode = false)
    {
        long iterations = grid.GetLongLength(0) * grid.GetLength(1) / 2;
        const int mapOffset = 2;
        const int wallOffset = 1;

        Random random = new Random();
        int randomX = random.Next(0, grid.GetLength(0) - mapOffset);
        int randomY = random.Next(0, grid.GetLength(1) - mapOffset);


        StringBuilder display = new StringBuilder();
        for (int i = 0; i < iterations; i++)
        {
            while (grid[randomX, randomY] == '.')
            {
                (randomX, randomY) = GenerateRandomDirection((randomX, randomY), random, mapOffset, wallOffset);
            }

            grid[randomX, randomY] = '.';

            (randomX, randomY) = GenerateRandomDirection((randomX, randomY), random, mapOffset, wallOffset);

            if (dev_mode) DisplayMap(1);
        }

        SecondTestForGeneratingMap(mapOffset);
    }

    (int, int) GenerateRandomDirection((int x, int y) randomPos, Random generator, int mapOffset, int wallOffset)
    {
        var random_direction = generator.Next(0, 4);
        switch (random_direction)
        {
            case 0: randomPos.y += 1; break;
            case 1: randomPos.y -= 1; break;
            case 2: randomPos.x += 1; break;
            case 3: randomPos.x -= 1; break;
        }

        randomPos.x = Math.Clamp(randomPos.x, wallOffset, grid.GetLength(0) - mapOffset);
        randomPos.y = Math.Clamp(randomPos.y, wallOffset, grid.GetLength(1) - mapOffset);

        return randomPos;
    }

    void SecondTestForGeneratingMap(int mapOffset)
    {
        char[,] newGrid = new char[screenSize.width, screenSize.height];
        FillMap(newGrid);

        const int minObject = 8;
        var count = 0;
        for (int row = 0; row < grid.GetLength(0) - mapOffset; row++)
        {
            for (int col = 0; col < grid.GetLength(1) - mapOffset; col++)
            {
                if (grid[row, col] == '#')
                {
                    count = GetSurroudingCount(row, col, '.');
                    if (count < minObject)
                    {
                        newGrid[row, col] = '.';
                    }
                }
                else if (grid[row, col] == '.')
                {
                    count = GetSurroudingCount(row, col, '#');
                    if (count < minObject)
                    {
                        newGrid[row, col] = '#';
                    }
                }
            }
        }
        Array.Copy(newGrid, grid, grid.Length);
    }

    int GetSurroudingCount(int x, int y, char what)
    {
        int count = 0;
        int xLength = grid.GetLength(0);
        int yLength = grid.GetLength(1);
        for (int offsetX = x - 1; offsetX <= x + 1; offsetX++)
        {
            for (int offsetY = y - 1; offsetY <= y + 1; offsetY++)
            {
                if (offsetX == x && offsetY == y) continue;

                if (offsetX < 0 || offsetX >= xLength) continue;
                if (offsetY < 0 || offsetY >= yLength) continue;

                if (grid[offsetX, offsetY] == what)
                {
                    count++;
                }
            }
        }
        return count; 
    }

    public void DisplayMap(int miliSeconds = 0)
    {
        var display = GetDisplay();
        Console.WriteLine(display);
        Console.SetCursorPosition(0, 0);
        Thread.Sleep(1);
    }
}