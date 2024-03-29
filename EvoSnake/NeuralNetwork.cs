﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EvoSnake
{
    public class NeuralNetwork
    {
        public double[,] vij { get; set; }   //weights from inut variables into hidden layer
        public double[,] wij { get; set; }   //weights from hidden layer into output layer
        public double[] ok { get; set; }    //output neurons for snake direction
        public double[] zi { get; set; }   //multidimensional array of inp  
        public double[] inputPattern = new double[6];  //value of input pattern
        public double[] yi { get; set; } = new double[6];
        Random Rgen = new Random();


        public NeuralNetwork()
        {
            vij = new double[6, 10];  //6 by 10 as there are 10 hidden neurons
            wij = new double[10, 3]; //10 by 4 as there are 10 hidden neurons and 3 output neurons
            ok = new double[3];
            zi = new double[6];
            makeNN();
            // snakeyBoi = new SnakeGame(20, 20);  //initialing the size of the board in the neural network class
        }
        //initialising neural network
        public void makeNN()
        {
            //initialising weights array betwen input and hidden
            //initialising weights array betwen input and hidden
            for (int i = 0; i < vij.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < vij.GetUpperBound(1) + 1; j++)
                {
                    vij[i, j] = Rgen.NextDouble() * 4.00;
                }
            }
            //initilse weights between hidden and output
            for (int i = 0; i < wij.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < wij.GetUpperBound(1) + 1; j++)
                {
                    wij[i, j] = Rgen.NextDouble() * 4.00;
                }
            }
            //initialing hidden layer neurons
           


        }

        //getting weights from GA, making them equal to arrays in NN class in order to use them
        public void updateWeightsArrays(double[,] arr1, double[,] arr2)
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
        public void setInputs(double[] arrInputs)      //arrInputs is the inputs array from for the genetic algorithm, called each generation of learning
        {
            for (int i = 0; i < arrInputs.Length; i++)
            {
                zi[i] = arrInputs[i];
            }
        }
        public moves calculateDirection(double[] arrInputs)
        {
            setInputs(arrInputs);
            double resultOfInputLayer = 0;
            //getting hidden neurons
            for (int i = 0; i < yi.Length; i++)
            {
                double temp2 = 0;
                for (int j = 0; j < zi.Length; j++)
                {
                    temp2 = temp2 + (zi[j] * vij[j, i]);
                }
                yi[i] = temp2;
            }
            //getting output neurons
            for (int i = 0; i < ok.Length; i++)
            {
                double temp1 = 0;
                for (int j = 0; j < yi.Length; j++)
                {
                    temp1 = temp1 + (yi[j] * wij[j, i]);
                }
                ok[i] = temp1;
            }
            //for (int i = 0; i < vij.Length; i++)
            //{
            //    resultOfInputLayer = resultOfInputLayer + sig(vij[i], zi[i]);               
            //}



            double resultsOfHiddenLayer = 0;
            //for (int i = 0; i < wij.Length; i++)
            //{
            //    ok[i] = resultOfInputLayer + wij[i]* resultOfInputLayer;
            //}
            double bestValue = 0.00;
            int bestMove = -1;
            for (int i = 0; i < ok.Length; i++)
            {
                
                if (ok[i] > bestValue)
                {
                    bestValue = ok[i];
                    bestMove = i;
                }
            }
            if (bestMove == 0)
            {
                return moves.Forward;
            }
            if (bestMove == 1)
            {
                return moves.Left;
            }
            if (bestMove == 2)
            {
                return moves.Right;
            }

            return moves.Forward;
        }
        public double sig(double weight, double input)
        {
            double sum = weight * input;
            double sig = 1.0f / (1.0f + (float)Math.Exp(-sum));
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
