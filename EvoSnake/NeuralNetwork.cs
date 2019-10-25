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
        double[,] vij; //weights from inut variables into hidden layer
        double[,] wij; //weights from hidden layer into output layer
        double[] ok = new double[4]; //output neurons for snake direction
        double[,] zi; //multidimensional array of inputs taken from GetInputs() method
        double[] yi = new double[10]; //hidden layer neurons


        //initializing weights from input of GA
       
       
        //initialising hidden layer neurons
    }
}
