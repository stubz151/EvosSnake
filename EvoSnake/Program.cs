using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoSnake
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            ConsoleKey input;
            SnakeGame sTest = new SnakeGame(20, 20);
            
            SnakeGame snakeyBoi = new SnakeGame((SnakeGame)sTest.Clone());
            //snakeyBoi = (SnakeGame)sTest.Clone();
            snakeyBoi.DisplayBoard();
            snakeyBoi.curDirection = Direction.Right;

            while (snakeyBoi.gameOver == false)
            {
                Direction move = snakeyBoi.curDirection;
                Stopwatch timer = new Stopwatch();
                timer.Start();
                while (timer.Elapsed.TotalMilliseconds < 100)
                {
                    if (Console.KeyAvailable)
                    {
                        input = Console.ReadKey().Key;
                        if (input == ConsoleKey.UpArrow)
                        {
                            move = Direction.Up;
                        }
                        if (input == ConsoleKey.DownArrow)
                        {
                            move = Direction.Down;
                        }
                        if (input == ConsoleKey.LeftArrow)
                        {
                            move = Direction.Left;
                        }
                        if (input == ConsoleKey.RightArrow)
                        {
                            move = Direction.Right;
                        }
                    }
                }
                timer.Stop();
                snakeyBoi.MakeMove(move);
                snakeyBoi.DisplayBoard();
            }
            Console.WriteLine("Game OVER!!");

            Console.ReadLine();
            Console.Clear();
            sTest.DisplayBoard();
            Console.ReadLine();

    */

            SnakeGame snakeyBoi = new SnakeGame(10, 20);
            GAClass ga = new GAClass(snakeyBoi);
            NeuralNetwork bestnn = ga.bestNN();
            Stopwatch timer = new Stopwatch();
            Console.WriteLine(ga.population.Count);
            Console.ReadLine();
            while (snakeyBoi.gameOver == false)
            {
                snakeyBoi.MakeMove(bestnn.calculateDirection(snakeyBoi.outputBox()));
                snakeyBoi.DisplayBoard();
                timer.Start();
                while (timer.Elapsed.TotalMilliseconds < 100)
                {
                }
                timer.Stop();
            }
            Console.WriteLine("Game OVER!!");
            Console.ReadLine();

        }
    }
}
