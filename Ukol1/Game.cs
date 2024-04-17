using System;
using System.Threading;

namespace Ukol1
{
    class Game
    {
        private int _width { get; }
        private int _height { get; }
        private Snake _snake { get; }
        private Food _food { get; }
        private int _score { get; set; }
        private bool _gameIsOver { get; set; }

        public Game()
        {
            _width = 32;
            _height = 16;
            _snake = new Snake(_width / 2, _height / 2);
            _food = new Food(_width, _height);
            _gameIsOver = false;
            SetupConsole();
        }

        private void SetupConsole()
        {
            Console.WindowWidth = _width;
            Console.WindowHeight = _height;
        }

        private void ClearBoard()
        {
            Console.Clear();
        }

        private void DrawBorders()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < _width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, _height - 1);
                Console.Write("■");
            }
            for (int i = 0; i < _height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(_width - 1, i);
                Console.Write("■");
            }
        }

        private void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                _snake.ChangeDirection(key);
            }
        }

        private void UpdateGame()
        {
            _snake.Move();
            if (_snake.CheckCollision(_width, _height))
            {
                _gameIsOver = true;
            }
            else if (_snake.Eat(_food))
            {
                _score++;
                _food.Respawn();
            }
        }

        private void Render()
        {
            _snake.Draw();
            _food.Draw();
        }

        private void EndGame()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(_width / 5, _height / 2);
            Console.WriteLine("Game over, Score: " + _score);
            Console.SetCursorPosition(_width / 5, _height / 2 + 1);
        }

        public void Start()
        {
            while (!_gameIsOver)
            {
                ClearBoard();
                DrawBorders();
                ProcessInput();
                UpdateGame();
                Render();
                Thread.Sleep(500);
            }
            EndGame();
        }
    }
}
