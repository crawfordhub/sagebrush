using System;
using AForge.Neuro;
using AForge.Math;
using AForge.Genetic;
using AForge;
using AForge.Neuro.Learning;

namespace Example.Network.XOr
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize input and output values
            double[][] input = new double[4][] 
            {
                new double[] {0, 0}, new double[] {0, 1},
                new double[] {1, 0}, new double[] {1, 1}
            };
            double[][] output = new double[4][] 
            {
                new double[] {0}, new double[] {1},
                new double[] {1}, new double[] {0}
            };

            // create neural network
            ActivationNetwork network = new ActivationNetwork(
                new SigmoidFunction(2),
                2, // two inputs in the network
                2, // two neurons in the first layer
                1); // one neuron in the second layer
            
            // create teacher
            BackPropagationLearning teacher =
                new BackPropagationLearning(network);
            
            // loop
            int i = 0;
            while (i < 100)
            {
                // run epoch of learning procedure
                double error = teacher.RunEpoch(input, output);
                // check error value to see if we need to stop
                // ...
                Console.WriteLine("Error: {0}", error);
                i++;
            }
            Console.ReadLine();
        }
    }
}
