using System;

namespace Ukol1
{
    class Food
    {
        public int Xpos { get; private set; }
        public int Ypos { get; private set; }
        private int _gameWidth { get; }
        private int _gameHeight { get; }
        private Random _radnomGenerator { get; }

        public Food(int gameWidth, int gameHeight)
        {
            _gameWidth = gameWidth;
            _gameHeight = gameHeight;
            _radnomGenerator = new Random();
            Respawn();
        }

        public void Respawn()
        {
            Xpos = _radnomGenerator.Next(1, _gameWidth - 2);
            Ypos = _radnomGenerator.Next(1, _gameHeight - 2);
        }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Xpos, Ypos);
            Console.Write("■");
        }
    }
}
