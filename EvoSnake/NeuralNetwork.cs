using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EvoSnake
{
    class NeuralNetwork
    {
        double[] vij { get; set; } = new double[6] ;  //weights from inut variables into hidden layer
        double[] wij { get; set; } = new double[4];  //weights from hidden layer into output layer
        double[] ok { get; set; } = new double[4];    //output neurons for snake direction
        double[] zi { get; set; } = new double[6];   //multidimensional array of inp  
        double[] inputPattern = new double[6];  //value of input pattern
        double[] yi { get; set; }
        Random Rgen = new Random();
        SnakeGame snakeyBoi;

        public NeuralNetwork()
        {
            makeNN();
           // snakeyBoi = new SnakeGame(20, 20);  //initialing the size of the board in the neural network class
        }
        //initialising neural network
        public void makeNN()
        {
            //initialising weights array betwen input and hidden
            for (int i = 0; i < vij.Length; i++)
            {
                
                    vij[i] = Rgen.NextDouble() * 4;
                
            }
            //initilse weights between hidden and output
            for (int i = 0; i < wij.Length; i++)
            {
               
                    wij[i] = Rgen.NextDouble() * 4;
                
            }
            //initialing hidden layer neurons
            
            for (int i = 0; i < yi.Length; i++)
            {
                yi[i] = 0;
            }
           
            //initialing output layer neurons
            for (int i = 0; i < ok.Length; i++)
            {
                ok[i] = 0;
            }

        }

        //getting weights from GA, making them equal to arrays in NN class in order to use them
        public void updateWeightsArrays(double[] arr1, double[] arr2)
        {
            for (int i = 0; i < vij.Length; i++)
            {

                vij[i] = arr1[i];

            }
            //initilse weights between hidden and output
            for (int i = 0; i < wij.Length; i++)
            {

                wij[i] = arr2[i];

            }
        }
        public void setInputs(double[] arrInputs)      //arrInputs is the inputs array from for the genetic algorithm, called each generation of learning
        {
            for (int i = 0; i < arrInputs.Length; i++)
            {              
                    zi[i] = arrInputs[i];               
            }
        }
        public Direction calculateDirection()
        {
            double resultOfInputLayer=0;
            for (int i = 0; i < vij.Length; i++)
            {
                resultOfInputLayer = resultOfInputLayer + sig(vij[i], zi[i]);               
            }
            double resultsOfHiddenLayer = 0;
            for (int i = 0; i < wij.Length; i++)
            {
                ok[i] = resultOfInputLayer + sig(wij[i], resultOfInputLayer);
            }
            double bestValue=-1;
            int bestMove = -1;
            for (int i =0; i< ok.Length; i++)
            {
                if (ok[i]> bestValue)
                {
                    bestValue = ok[i];
                    bestMove = i;
                }
            }
            if (bestMove==0)
            {
                return Direction.Up;
            }
            if (bestMove == 1)
            {
                return Direction.Down;
            }
            if (bestMove == 2)
            {
                return Direction.Left;
            }
            if (bestMove == 3)
            {
                return Direction.Right;
            }
            return Direction.Up;
        }
        public double sig(double weight, double input)
        {
            double sum = weight * input;
            double sig = 1.0f / (1.0f + (float)Math.Exp(-sum);
            return sig;
        }
        /*
        public void learn()
        {
            
            while (snakeyBoi.gameOver == false)
            {
                //call genetic algorithm to get the new updated weights arrays btw input-hidden and hidden-output,as webight updates are not done by the NN, but rather by the GA
                updateWeightsArrays(vij, wij);      //updating weights in order to use thme in the NN in order to get output

                for (int p = 0; p < zi.GetUpperBound(0) - 1; p++)
                {
                    //getting inputs
                    for (int i = 0; i < zi.Length; i++)
                    {
                        inputPattern[i] = zi[p, i];
                    }
                    //getting hidden neurons through multiplynig the matrices
                   
                    for (int i = 0; i < yi.Length; i++) 
                    {
                        double temp2 = 0;
                        for (int j = 0; j < zi.Length; j++) 
                        {
                            temp2 = temp2 + (inputPattern[j] * vij[j, i]);
                        }
                        yi[i] = temp2;
                    }
                    
                    //get output neurons
                    for (int i = 0; i < ok.Length; i++) //24
                    {
                        double temp1 = 0;
                       
                        for (int j = 0; j < yi.Length; j++) //10
                        {
                            temp1 = temp1 + (yi[j] * wij[j, i]);
                        }
                       
                        ok[i] = temp1;//OK[] represents the outputs
                    }
                }
            }
            
        }
        */
    }
}
