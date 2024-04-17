using System;
using System.Collections.Generic;

namespace Ukol1
{
    class Snake
    {
        private enum Direction
        {
            Up, Down, Right, Left
        }

        private Direction _direction;
        private readonly List<(int Xpos, int Ypos)> _body = new List<(int Xpos, int Ypos)>();
        private bool _growOnNextMove = false;

        public Snake(int initialX, int initialY)
        {
            _body.Add((initialX, initialY));
            _direction = Direction.Right;
        }

        public void ChangeDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _direction = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    _direction = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    _direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    _direction = Direction.Right;
                    break;
                default:
                    break;
            }
        }

        public void Move()
        {
            var head = _body[0];
            switch (_direction)
            {
                case Direction.Up:
                    head.Ypos--;
                    break;
                case Direction.Down:
                    head.Ypos++;
                    break;
                case Direction.Left:
                    head.Xpos--;
                    break;
                case Direction.Right:
                    head.Xpos++;
                    break;
            }

            _body.Insert(0, head);

            if (!_growOnNextMove)
            {
                _body.RemoveAt(_body.Count - 1);
            }
            else
            {
                _growOnNextMove = false;
            }
        }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var segment in _body)
            {
                Console.SetCursorPosition(segment.Xpos, segment.Ypos);
                Console.Write("■");
            }
        }

        public bool CheckCollision(int boundaryWidth, int boundaryHeight)
        {
            var head = _body[0];

            if (head.Xpos <= 0 || head.Xpos >= boundaryWidth - 1 || head.Ypos <= 0 || head.Ypos >= boundaryHeight - 1)
            {
                return true;
            }

            for (int i = 1; i < _body.Count; i++)
            {
                if (head.Xpos == _body[i].Xpos && head.Ypos == _body[i].Ypos)
                    return true;
            }

            return false;
        }

        public bool Eat(Food food)
        {
            var head = _body[0];

            if (head.Xpos == food.Xpos && head.Ypos == food.Ypos)
            {
                _growOnNextMove = true;
                return true;
            }
            return false;
        }
    }
}
