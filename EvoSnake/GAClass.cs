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
      
        Direction move;
        Random Rgen = new Random();
        //the distance between the snake and food pre-move.
        double distanceToFoodBefore;
        int popSize = 100;
        int iterations = 1000;
        double crossOverRate = 0.5;
        double mutationChance = 0.4;
        double mutationMag = 2.0;
        int inputLayerSize = 6;
        int hiddenLayerSize = 4;
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
            int t = 0;
            while (t< iterations) {
                List<NeuralNetwork> newPop = new List<NeuralNetwork>();
                while(newPop.Count< population.Count)
                {             
                    SnakeGame temp = new SnakeGame((SnakeGame)snake.Clone());
                    NeuralNetwork bestNN1 = new NeuralNetwork();
                    NeuralNetwork bestNN2 = new NeuralNetwork();
                    int bestResult1 = -1;
                    int bestResult2 = -1;
                    for (int i = 0; i < 20; i++)
                    {
                        int nextnum = Rgen.Next(population.Count);
                        NeuralNetwork curNN = population[i];
                        
                        int curResult = getResult(curNN.calculateDirection(temp.outputBox()), new SnakeGame((SnakeGame)temp.Clone()));
                        if (curResult > bestResult1)
                        {
                            bestResult1 = curResult;
                            bestNN1 = curNN;
                        }
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        int nextnum = Rgen.Next(population.Count);
                        NeuralNetwork curNN = population[i];

                        int curResult = getResult(curNN.calculateDirection(temp.outputBox()), new SnakeGame((SnakeGame)temp.Clone()));
                        if (curResult > bestResult2)
                        {
                            bestResult2 = curResult;
                            bestNN2 = curNN;
                        }
                    }
                    NeuralNetwork crossedNN = crossGen(bestNN1, bestNN2);
                    crossedNN = Mutate(crossedNN);
                    newPop.Add(crossedNN);
                }
                population = newPop;
                t++;
            }
        }

        public NeuralNetwork crossGen(NeuralNetwork nn1 , NeuralNetwork nn2)
        {
            Double[] inArr = new Double[6];
            Double[] inputarr1 = nn1.vij;
            Double[] inputarr2 = nn2.vij;
            for (int i=0; i < inputLayerSize; i++)
            {
                if (Rgen.NextDouble()>crossOverRate)
                {
                    inArr[i] = inputarr1[i];
                }
                else
                {
                    inArr[i] = inputarr2[i];
                }
            }
            double[] hidArr = new double[4];
            Double[] hidArr1 = nn1.wij;
            Double[] hidArr2 = nn2.wij;
            for (int i = 0; i < hiddenLayerSize; i++)
            {
                if (Rgen.NextDouble() > crossOverRate)
                {
                    hidArr[i] = hidArr1[i];
                }
                else
                {
                    hidArr[i] = hidArr2[i];
                }
            }
            NeuralNetwork nn = new NeuralNetwork();
            nn.vij = inArr;
            nn.wij = hidArr;
            return nn;
        }

        public NeuralNetwork Mutate(NeuralNetwork nn1)
        {
            Double[] arr1 = nn1.vij;
            Double[] arr2 = nn1.wij;
            for (int i = 0; i < inputLayerSize; i++)
            {
                if (Rgen.NextDouble() <= mutationChance)
                {
                    arr1[i] = arr1[i] + Rgen.NextDouble() * mutationMag;
                }

            }
            nn1.vij = arr1;
            for (int i = 0; i < hiddenLayerSize; i++)
            {
                if (Rgen.NextDouble() <= mutationChance)
                {
                    arr2[i] = arr2[i] + Rgen.NextDouble() * mutationMag;
                }

            }
            nn1.wij = arr2;
            return nn1;
        }
            public int getResult(Direction move, SnakeGame temp)
        {
            int distanceBefore = temp.distanceToFood();          
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
