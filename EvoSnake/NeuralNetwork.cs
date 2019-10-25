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
        double[,] vij;  //weights from inut variables into hidden layer
        double[,] wij;  //weights from hidden layer into output layer
        double[] ok = new double[4];    //output neurons for snake direction
        double[,] zi;   //multidimensional array of inputs taken from GetInputs() method
        double[] yi = new double[10];   //hidden layer neurons
        double[] inputPattern = new double[4];  //value of input pattern

        Random Rgen = new Random();
        SnakeGame snakeyBoi;

        public NeuralNetwork()
        {
            snakeyBoi = new SnakeGame(20, 20);  //initialing the size of the board in the neural network class
        }
        //initialising neural network
        public void makeNN()
        {
            //initialising weights array betwen input and hidden
            for (int i = 0; i < vij.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < vij.GetUpperBound(1) + 1; j++)
                {
                    vij[i, j] = Rgen.NextDouble() * 4;
                }
            }
            //initilse weights between hidden and output
            for (int i = 0; i < wij.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < wij.GetUpperBound(1) + 1; j++)
                {
                    wij[i, j] = Rgen.NextDouble() * 4;
                }
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
        public void updateWieghtsArrays(double[,] arr1, double[,] arr2)
        {
            for (int i = 0; i < arr1.GetUpperBound(0); i++)
            {
                for (int j = 0; j < arr1.GetUpperBound(1); j++)
                {
                    vij[i, j] = arr1[i, j];
                }
            }
            for (int i = 0; i < arr2.GetUpperBound(0); i++)
            {
                for (int j = 0; j < arr2.GetUpperBound(1); j++)
                {
                    wij[i, j] = arr2[i, j];
                }
            }
        }
        public void getInputs(double[,] arrInputs)      //arrInputs is the inputs array from for the genetic algorithm, called each generation of learning
        {
            for (int i = 0; i < arrInputs.GetUpperBound(0); i++)
            {
                for (int j = 0; j < arrInputs.GetUpperBound(1); j++)
                {
                    zi[i, j] = arrInputs[i, j];
                }
            }
        }
        public void learn()
        {
            
            while (snakeyBoi.gameOver == false)
            {
                //call genetic algorithm to get the new updated weights arrays btw input-hidden and hidden-output,as webight updates are not done by the NN, but rather by the GA
                updateWieghtsArrays(vij, wij);      //updating weights in order to use thme in the NN in order to get output

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

    }
}
