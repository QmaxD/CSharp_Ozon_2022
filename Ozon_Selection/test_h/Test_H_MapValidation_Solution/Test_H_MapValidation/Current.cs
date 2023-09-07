using System;


public static class Program
{
    public static void Main(string[] args)
    {
        string? inputLine;
        inputLine = Console.ReadLine();
        int numberOfTests = int.Parse(inputLine);
        do
        {
            inputLine = Console.ReadLine();
            int[] size = inputLine.Split(' ').Select(it => int.Parse(it)).ToArray();
            int row = size[0];
            int col = size[1];

            char[][] map = new char[row][];
            for (int i = 0; i < row; i++)
                map[i] = new char[(col + 1) / 2 + (row - 1) / 2];

            int numberRow = 0;
            int numberCol = (row - 1) / 2;
            int oddRow = 0;
            do
            {
                inputLine = Console.ReadLine();
                inputLine = inputLine.Trim('.');
                char[] rowColor = inputLine.Split('.').Select(it => char.Parse(it)).ToArray();
                int indexRowColor = 0;

                for (int width = 0; width < map[numberRow].Length; width++)
                {
                    if (width < numberCol || indexRowColor >= rowColor.Length)
                        map[numberRow][width] = '.';
                    else
                    {
                        map[numberRow][width] = rowColor[indexRowColor];
                        indexRowColor++;
                    }
                }
                oddRow++;
                if (oddRow % 2 == 0)
                    numberCol--;
                numberRow++;
                row--;
            } while (row > 0);

            // поиск границ цвета
            bool allTrue = true;
            List<char> colors = new();
            char color = '.';

            int h = 0, w = 0;
            while (h < map.Length && allTrue)
            {
                w = 0;
                while (w < map[h].Length && allTrue)
                {
                    if (map[h][w] != '.' && map[h][w] != 'x')
                    {
                        color = map[h][w];
                        map[h][w] = 'x';

                        if (colors.Contains(color))
                            allTrue = false;
                        else
                            SearchColorRegion(map, h, w, color);
                        colors.Add(color);// запишем цвет в массив
                    }
                    w++;
                }
                h++;
            }

            if (allTrue)
                Console.WriteLine("YES");
            else
                Console.WriteLine("NO");

            numberOfTests--;
        }
        while (numberOfTests > 0);


        void SearchColorRegion(char[][] map, int row, int col, char color)
        {
            char verified = 'x';

            // ---  проверка в ряду справа  ---
            if (col + 1 < map[row].Length)
            {
                if (map[row][col + 1] == color)
                {
                    map[row][col + 1] = verified;
                    SearchColorRegion(map, row, col + 1, color);
                }
            }

            // ---  проверка внизу справа  ---
            if (row + 1 < map.Length)
            {
                if (map[row + 1][col] == color)
                {
                    map[row + 1][col] = verified;
                    SearchColorRegion(map, row + 1, col, color);
                }
            }

            // ---  проверка внизу слева  ---
            if (row + 1 < map.Length && col - 1 >= 0)
            {
                if (map[row + 1][col - 1] == color)
                {
                    map[row + 1][col - 1] = verified;
                    SearchColorRegion(map, row + 1, col - 1, color);
                }
            }

            // ---  проверка в ряду слева  ---
            if (col - 1 >= 0)
            {
                if (map[row][col - 1] == color)
                {
                    map[row][col - 1] = verified;
                    SearchColorRegion(map, row, col - 1, color);
                }
            }

            // ---  проверка вверху слева  ---
            if (row - 1 >= 0)
            {
                if (map[row - 1][col] == color)
                {
                    map[row - 1][col] = verified;
                    SearchColorRegion(map, row - 1, col, color);
                }
            }

            // ---  проверка вверху справа  ---
            if (row - 1 >= 0 && col + 1 < map[row - 1].Length)
            {
                if (map[row - 1][col + 1] == color)
                {
                    map[row - 1][col + 1] = verified;
                    SearchColorRegion(map, row - 1, col + 1, color);
                }
            }
        }

    }
}
