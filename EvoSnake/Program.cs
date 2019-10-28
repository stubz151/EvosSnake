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
                moves move = moves.Forward;
                Stopwatch timer = new Stopwatch();
                timer.Start();
                while (timer.Elapsed.TotalMilliseconds < 100)
                {
                    if (Console.KeyAvailable)
                    {
                        input = Console.ReadKey().Key;
                        if (input == ConsoleKey.UpArrow)
                        {
                            move = moves.Forward;
                        }
                        
                        if (input == ConsoleKey.LeftArrow)
                        {
                            move = moves.Left;
                        }
                        if (input == ConsoleKey.RightArrow)
                        {
                            move = moves.Right;
                        }
                    }
                }
                timer.Stop();
                snakeyBoi.moveHead(move);
                snakeyBoi.DisplayBoard();
            }
            Console.WriteLine("Game OVER!!");

            Console.ReadLine();
            Console.Clear();
            sTest.DisplayBoard();
            Console.ReadLine();
            */         
            SnakeGame snakeyBoi = new SnakeGame(10, 10);
            snakeyBoi.curDirection = Direction.Right;
            while(snakeyBoi.gameOver==false)
            {

                double resultForward = snakeyBoi.resultOfMove(moves.Forward);
                double resultLeft= snakeyBoi.resultOfMove(moves.Left);
                double resultRight = snakeyBoi.resultOfMove(moves.Right);
                moves move = moves.Right;
                if (resultForward>resultLeft)
                {
                    if (resultForward > resultRight)
                    {
                        move = moves.Forward;
                    }
                    
                }
                else
                {
                    if (resultLeft > resultRight)
                    {
                        move = moves.Left;
                    }                 
                }
                snakeyBoi.moveHead(move);
                snakeyBoi.DisplayBoard();
                System.Threading.Thread.Sleep(1000);
            }
            snakeyBoi.DisplayBoard();
            Console.WriteLine("Game OVER!!");
            Console.ReadLine();

            /*
            GAClass ga = new GAClass(snakeyBoi);
            NeuralNetwork bestnn = ga.population[5];
            //NeuralNetwork bestnn = ga.bestNN();
            Stopwatch timer = new Stopwatch();        
            Console.Clear();
            while (snakeyBoi.gameOver == false)
            {
               
                moves move = bestnn.calculateDirection(snakeyBoi.getInputs());
                snakeyBoi.moveHead(move);              
                snakeyBoi.DisplayBoard();               
                System.Threading.Thread.Sleep(1000);
            
            }
            
            snakeyBoi.DisplayBoard();
            Console.WriteLine("Game OVER!!");
            Console.ReadLine();
            */
        }
       
        }
    }

