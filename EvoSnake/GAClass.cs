using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoSnake
{
    class GAClass
    {
        //population contains a list of units
        List<NeuralNetwork> population = new List<NeuralNetwork>();
        SnakeGame snake = new SnakeGame(20, 20);
        //4 possible moves with different double values, will pick the highest one from the nn
        double[] moveResult = new double[4];
        int iterations = 1000;
        Direction move;
        //the distance between the snake and food pre-move.
        double distanceToFoodBefore;
        int popSize = 100;
        
        public void genPop()
        {
            for (int i = 0; i < popSize; i++)
            {
                NeuralNetwork nn = new NeuralNetwork();
                population.Add(nn);
            }
        }
        public void Train()
        {
            for (int i = 0; i < iterations; i++)
            {
                while (snake.gameOver == false)
                {
                    SnakeGame temp = (SnakeGame)snake.Clone();

                }
            }
        }
        public int result(Direction move)
        {
            int distanceBefore = snake.distanceToFood();
            SnakeGame temp = (SnakeGame)snake.Clone();
            temp.MakeMove(move);
            int result = 0;
            if (temp.gameOver == true)
            {
                
            }
            else
            {
                result++;
                if (temp.distanceToFood() < distanceBefore)
                {
                    result++;
                }

            }
            return result;
        }
    }
}
