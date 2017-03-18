using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetFramework;
using Networks.Example.XOr;
using NeuralNetCommon;

namespace XOrNetworkTrainer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World");

			var network = new Network();

			// Assign training data:

            for (int i = 0; i < 20; i++)
            {
                var selector = i % 4;
                network.Input1.Sum = network.TrainingFunction.ElementAt(selector).Input1;
                network.Input2.Sum = network.TrainingFunction.ElementAt(selector).Input2;
                var expectedOutput = network.TrainingFunction.ElementAt(selector).Output;

			    var output = ForwardPropagate(network);
                Console.WriteLine("Error: {0}", expectedOutput - output);
                Console.WriteLine("");
                BackwardPropagate(network, output, expectedOutput);
            }

            Console.WriteLine("Final weights:");
            foreach (var synapse in network.Synapses)
            {
                Console.WriteLine("{0}", synapse.Weight);
            }

			Console.ReadLine();
		}

		private static double ForwardPropagate(Network network)
		{
            Console.WriteLine("Input Value 1: {0}", network.Input1.Result);
            Console.WriteLine("Input Value 2: {0}", network.Input2.Result);
            
            var inputNodes = new List<Node> { network.Input1, network.Input2 };

			// Reset all values:
			network.Output.Sum = 0;
			foreach(var node in network.HiddenLayer)
			{
				node.Sum = 0;
			}

			// Forward propagate from input nodes to hidden layer.
			foreach (var synapse in network
				.Synapses
				.Where(s => inputNodes.Any(n => n.Equals(s.InputNode))))
			{
				synapse.OutputNode.Sum += synapse.InputNode.Result * synapse.Weight;
			}

			// Forward propagate fromo hidden layer to output.
			foreach(var synapse in network
				.Synapses
				.Where(s => network.HiddenLayer.Any(n => n.Equals(s.InputNode))))
			{
				synapse.OutputNode.Sum += synapse.InputNode.Result * synapse.Weight;
			}

            return network.Output.Result;
		}

		private static void BackwardPropagate(Network network, double calculatedResult, double target)
		{
            var inputNodes = new List<Node> { network.Input1, network.Input2 };
			var outputMarginOfError = target - calculatedResult;

			// ******* Hack - using linear activation methods so d/dx == 1
			var deltaOutputSum = ActivationMethods.ComputeSigmoidDerivative(network.Output.Sum) * outputMarginOfError;

            // Synapses connected to the output node:
            foreach (var synapse in network.Synapses.Where(s => network.HiddenLayer.Any(n => n.Equals(s.InputNode))))
            {
                // Calculate new weight:
                synapse.OldWeight = synapse.Weight;
                synapse.Weight += deltaOutputSum * synapse.InputNode.Result;
            }

            // Synapses connected to input nodes:
            // Delta hidden sum = delta output sum * hidden-to-outer weights * S'(hidden sum)
            foreach (var synapse in network
				.Synapses
				.Where(s => inputNodes.Any(n => n.Equals(s.InputNode))))
            {
                // Find the output synapse:
                var outputSynapse = network.Synapses.First(s => s.InputNode == synapse.OutputNode);

                synapse.OutputNode.DeltaHiddenSum = deltaOutputSum * outputSynapse.OldWeight * ActivationMethods.ComputeSigmoidDerivative(synapse.OutputNode.Sum);

                synapse.Weight += synapse.OutputNode.DeltaHiddenSum * network.Input1.Result;
            }
		}
	}
}
