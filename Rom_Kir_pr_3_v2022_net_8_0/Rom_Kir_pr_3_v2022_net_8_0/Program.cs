using System;

class Program
{
    static void Main()
    {
        // Флаг для управления повторением выбора операций
        bool repeat = true;
        // Координаты ферзя
        string queenPos = string.Empty;
        // Координаты другой фигуры
        string piecePos = string.Empty;

        // Основной цикл программы, продолжается до тех пор, пока пользователь не выберет выход
        while (repeat)
        {
            // Вывод меню выбора действий
            Console.WriteLine("Выберите одно из действий:");
            Console.WriteLine("1. Разместить фигуры на шахматной доске");
            Console.WriteLine("2. Определить, бьет ли ферзь фигуру");
            Console.WriteLine("3. Выйти из программы");
            Console.Write("Ваш выбор: ");

            // Проверка на ввод числа
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                Console.WriteLine();
                continue;
            }

            // Выполнение выбранного действия
            switch (choice)
            {
                case 1:
                    // Размещение фигур на шахматной доске
                    SetupBoard(out queenPos, out piecePos);
                    break;
                case 2:
                    // Проверка возможности побить фигуру ферзём
                    if (queenPos != string.Empty && piecePos != string.Empty)
                    {
                        CheckCapture(queenPos, piecePos);
                    }
                    else
                    {
                        Console.WriteLine("Фигуры на шахматной доске не размещены");
                    }
                    break;
                case 3:
                    // Завершение программы
                    repeat = false;
                    break;
                default:
                    // Обработка неверного выбора
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }

            // Переход на новую строку для удобства чтения
            Console.WriteLine();
        }
    }

    // Метод для размещения фигур на доске
    static void SetupBoard(out string queenPos, out string piecePos)
    {
        // Инициализация доски
        char[,] board = new char[8, 8];
        InitializeBoard(board);

        // Запрос координат для размещения ферзя и фигуры
        Console.WriteLine("Введите координаты ферзя и фигуры (в формате x1y1 x2y2):");
        string input = Console.ReadLine();

        // Разделение введённых координат
        string[] coordinates = input.Split(' ');

        // Проверка корректности введённых координат
        if (coordinates.Length != 2 || coordinates[0] == coordinates[1] || !ValidateCoordinates(coordinates[0]) || !ValidateCoordinates(coordinates[1]))
        {
            Console.WriteLine("Введены некорректные координаты");
            queenPos = string.Empty;
            piecePos = string.Empty;
            return;
        }

        // Размещение фигур на доске
        queenPos = coordinates[0];
        piecePos = coordinates[1];
        PlacePieces(board, queenPos, piecePos);

        // Вывод доски
        DrawBoard(board);
    }

    // Метод для проверки, может ли ферзь побить фигуру
    static void CheckCapture(string queenPos, string piecePos)
    {
        // Вывод информации о проверке
        Console.WriteLine("Операция   2: Определение, бьет ли ферзь фигуру");
        Console.WriteLine();

        // Вычисление координат ферзя и фигуры
        int queenX = queenPos[0] - 'a';
        int queenY = queenPos[1] - '1';

        int pieceX = piecePos[0] - 'a';
        int pieceY = piecePos[1] - '1';

        // Проверка, может ли ладья побить фигуру
        if (queenX == pieceX || queenY == pieceY)
        {
            Console.WriteLine("Ферзь сможет побить фигуру за 1 ход");
        }
        else if (Math.Abs(queenX - pieceX) == Math.Abs(queenY - pieceY))
        {
            Console.WriteLine("Ферзь сможет побить фигуру за  1 ход");
        }
        else
        {
            Console.WriteLine("Ферзь не может побить фигуру за 1 ход");
        }
    }

    // Метод для инициализации доски
    static void InitializeBoard(char[,] board)
    {
        // Заполнение доски пустыми клетками
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                board[row, col] = '-';
            }
        }
    }

    // Метод для размещения фигур на доске
    static void PlacePieces(char[,] board, string queenPos, string piecePos)
    {
        // Вычисление координат ферзя и фигуры
        int queenX = queenPos[0] - 'a';
        int queenY = queenPos[1] - '1';

        int pieceX = piecePos[0] - 'a';
        int pieceY = piecePos[1] - '1';

        // Размещение ферзя и фигуры на доске
        MoveQueen(board, queenX, queenY);
        PlacePiece(board, pieceX, pieceY, 'F');
    }

    // Метод для размещения ферзя на доске
    static void MoveQueen(char[,] board, int x, int y)
    {
        // Проверка корректности координат
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            board[y, x] = 'Q'; // Размещаем ферзя на выбранных координатах
        }
    }

    // Метод для размещения фигуры на доске
    static void PlacePiece(char[,] board, int x, int y, char piece)
    {
        // Проверка корректности координат
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            board[y, x] = piece; // Размещаем фигуру на выбранных координатах
        }
    }

    // Метод для вывода доски
    static void DrawBoard(char[,] board)
    {
        // Вывод заголовка доски
        Console.WriteLine("   a b c d e f g h");

        // Вывод доски в обратном порядке (с верхней стороны)
        for (int row = 7; row >= 0; row--)
        {
            Console.Write($"{row + 1} ");

            for (int col = 0; col < 8; col++)
            {
                Console.Write(board[row, col] + " ");
            }

            Console.WriteLine();
        }
    }

    // Метод для проверки корректности введённых координат
    static bool ValidateCoordinates(string coordinate)
    {
        // Проверка длины строки координат
        if (coordinate.Length != 2)
        {
            return false;
        }

        // Проверка диапазона символов координат
        char file = coordinate[0];
        char rank = coordinate[1];

        if (file < 'a' || file > 'h' || rank < '1' || rank > '8')
        {
            return false;
        }

        return true;
    }
}
